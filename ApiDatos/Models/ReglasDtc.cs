using System;
using System.Collections.Generic;

#nullable disable

namespace ApiDatos.Models
{
    public partial class ReglasDtc
    {
        public int Id { get; set; }
        public decimal? ValorDtc { get; set; }
        public int? PaisId { get; set; }

        public virtual Paises Pais { get; set; }
    }
}
