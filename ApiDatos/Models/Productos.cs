using System;
using System.Collections.Generic;

#nullable disable

namespace ApiDatos.Models
{
    public partial class Productos
    {
        public Productos()
        {
            Imagenes = new HashSet<Imagenes>();
            InversePais = new HashSet<Productos>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal? Precio { get; set; }
        public decimal? Descuento { get; set; }
        public int? PaisId { get; set; }
        public int? Visualizaciones { get; set; }

        public virtual Productos Pais { get; set; }
        public virtual ICollection<Imagenes> Imagenes { get; set; }
        public virtual ICollection<Productos> InversePais { get; set; }
    }
}
