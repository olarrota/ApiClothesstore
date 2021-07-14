using System;
using System.Collections.Generic;

#nullable disable

namespace ApiDatos.Models
{
    public partial class TiposImagen
    {
        public TiposImagen()
        {
            Imagenes = new HashSet<Imagenes>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Imagenes> Imagenes { get; set; }
    }
}
