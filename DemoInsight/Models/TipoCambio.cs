using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace DemoInsight.Models
{
    //[Table("TipoCambio")]
    public partial class TipoCambio
    {
     
        public int Id { get; set; }
        public DateTime FechaSys { get; set; }
        public DateTime Fecha { get; set; }
        public Decimal Referencia { get; set; }

        public TipoCambio()
        {
            FechaSys = DateTime.Now;
        }
    }


}
