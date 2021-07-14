using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiNegocio.Entidades
{
    public class ViewModelCrearImagenes
    {
        public int TiposImagenId { get; set; }
        public IFormFile Imagen { get; set; }
    }
}
