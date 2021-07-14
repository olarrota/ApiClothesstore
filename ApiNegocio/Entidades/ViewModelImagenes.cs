using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiNegocio.Entidades
{
    public class ViewModelImagenes
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Ruta { get; set; }
        public int? ProductosId { get; set; }
        public int? TiposImagenId { get; set; }
        //public IFormFile Img { get; set; }
    }
}
