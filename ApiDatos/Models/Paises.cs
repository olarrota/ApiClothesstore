using System;
using System.Collections.Generic;

#nullable disable

namespace ApiDatos.Models
{
    public partial class Paises
    {
        public Paises()
        {
            ReglasDtcs = new HashSet<ReglasDtc>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<ReglasDtc> ReglasDtcs { get; set; }
    }
}
