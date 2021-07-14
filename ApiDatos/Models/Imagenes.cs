using System;
using System.Collections.Generic;

#nullable disable

namespace ApiDatos.Models
{
    public partial class Imagenes
    {
        public int Id { get; set; }
        public int? ProductosId { get; set; }
        public int? TiposImagenId { get; set; }
        public string Ruta { get; set; }
        public string Nombre { get; set; }

        public virtual Productos Productos { get; set; }
        public virtual TiposImagen TiposImagen { get; set; }
    }
}
