using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ApiNegocio.Entidades
{
    public class ViewModelProducto
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Descripcion { get; set; }
        [Required]
        public decimal? Precio { get; set; }
        public decimal? PrecioDtc { get; set; }
        [Required]
        public decimal? Descuento { get; set; }
        [Required]
        public int? PaisId { get; set; }
        public int? visualizaciones { get; set; }

        public List<ViewModelImagenes> ListImagenes { get; set; }
    }
}
