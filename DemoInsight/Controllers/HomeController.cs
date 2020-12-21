using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DemoInsight.Models;
using static ServiceReferenceTipoCambio.TipoCambioSoapClient;
using ServiceReferenceTipoCambio;
using Microsoft.EntityFrameworkCore;

namespace DemoInsight.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }



        public async Task<IActionResult> Index()
       {

            TipoCambioSoapClient client = new TipoCambioSoapClient(EndpointConfiguration.TipoCambioSoap);
            //var result = await client.AddAsync(5, 8);
            var result = await client.TipoCambioDiaAsync();

            var ResultOfTipoCambioBank = result.Body.TipoCambioDiaResult.CambioDolar[0];

            ViewBag.ResultOfBank = ResultOfTipoCambioBank;



            var contextOptions = new DbContextOptionsBuilder<Models.DBContext>()
            .UseSqlServer("Server=localhost;Database=DemoAzureInsight;User Id=elvis;Password=*Disdel100;")
            .Options;

            using var context = new Models.DBContext(contextOptions);
            Models.TipoCambio NewValueTipoCambios = new TipoCambio();
            NewValueTipoCambios.Fecha = Convert.ToDateTime(ResultOfTipoCambioBank.fecha);
            NewValueTipoCambios.Referencia =Convert.ToDecimal(ResultOfTipoCambioBank.referencia);

            context.Add<Models.TipoCambio>(NewValueTipoCambios);
            context.SaveChanges();

            List<Models.TipoCambio> AllData = context.TipoCambios.ToList();
            ViewBag.AllData = AllData;


            return View();

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}
