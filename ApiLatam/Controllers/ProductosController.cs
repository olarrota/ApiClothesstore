using ApiNegocio.Entidades;
using ApiNegocio.NegocioClothesstore;
using ImageMagick;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace ApiLatam.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private IWebHostEnvironment _hostingEnvironment;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="environment"></param>
        public ProductosController(IWebHostEnvironment environment)
        {
            _hostingEnvironment = environment;
        }


        /// <summary>
        /// Consulta los productos existentes en la tienda
        /// </summary>
        /// <returns>Los productos con su información y imagenes</returns>
        [HttpGet]
        [Route("/[controller]/[action]")]
        public ActionResult GetProductos()
        {
            try
            {
                NegocioProducto negocio = new NegocioProducto();
                List<ViewModelProducto> lst = new List<ViewModelProducto>();
                lst = negocio.getProductos();
                return Ok(lst);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        /// <summary>
        /// Consulta los productos más destacados, utiliza el campo visualizaciones de la tabla productos
        /// para Ordenar la consuta por los mas buscados.
        /// </summary>
        /// <returns>La información de los productos  con sus imagenes indicando si es frontal,trasera, etc</returns>
        [HttpGet]
        [Route("/[controller]/[action]")]
        public ActionResult GetProductosDestacados()
        {
            try
            {
                NegocioProducto negocio = new NegocioProducto();
                List<ViewModelProducto> lst = new List<ViewModelProducto>();
                lst = negocio.getProductosDestacados();
                return Ok(lst);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        /// <summary>
        /// Crea un nuevo producto, con su respectivo listado de imagenes
        /// </summary>
        /// <param name="files"></param>
        /// <param name="product"></param>
        /// <returns>Devuelve un listado de productos actulizados</returns>
        [HttpPost]
        [Route("/[controller]/[action]")]
      
        public ActionResult AddProductos([FromForm] List<IFormFile> files, [FromForm] ViewModelCrearProducto product)
        //public ActionResult AddProductos([FromForm] List<ViewModelCrearImagenes> files, [FromForm] ViewModelCrearProducto product)
        {
            NegocioProducto negocio = new NegocioProducto();
            List<ViewModelImagenes> ListImgSave = new List<ViewModelImagenes>();
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                
                if (!negocio.validarDescuento(product.PaisId, product.Descuento))
                {
                    return BadRequest("El descuento no es valido");
                }
                ViewModelImagenes imgSave;
                if (files.Count() > 0)
                {
                    int maxSize = 1000000;
                    string filePath = Path.Combine(_hostingEnvironment.ContentRootPath, "Images");
                    string ruta = "";
                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }
                    string nameFile = "";
                    
                    int contador = 0;
                    foreach (var item in files)
                    {
                        ruta = "";
                        nameFile = "";
                        imgSave = new ViewModelImagenes();
                        
                        if (!Regex.IsMatch(item.FileName.ToLower(), @"^.*\.(jpg|png|jpeg)$"))
                        {
                           return BadRequest("No es un formato valido para la imagen");
                        }
                        nameFile = DateTime.Now.ToString("yyyyMMddHHmmssfff") + "_" + item.FileName;
                        ruta = Path.Combine(filePath, nameFile);

                        if (item.Length > maxSize)
                        {
                            byte[] fileBytes;
                            using (var ms = new MemoryStream())
                            {
                                item.CopyTo(ms);
                                 fileBytes = ms.ToArray();
                            }
                            using (MagickImage oMagickImage = new MagickImage(fileBytes))
                            {
                                oMagickImage.Resize(900, 0);
                                oMagickImage.Write(ruta);
                            }
                        }
                        else {
                            using (var stream = System.IO.File.Create(ruta))
                            {
                                item.CopyToAsync(stream);
                            }
                        }


                       
                        imgSave.Nombre = nameFile;
                        imgSave.Ruta = ruta;
                        if (contador == 0)
                        {
                            imgSave.TiposImagenId = 1;
                        }
                        else if (contador == 1)
                        {
                            imgSave.TiposImagenId = 2;
                        }
                        else {
                            imgSave.TiposImagenId = 3;
                        }
                       
                        ListImgSave.Add(imgSave);
                        contador++;
                    }
                }

                List<ViewModelProducto> lst = new List<ViewModelProducto>();
                lst = negocio.addProducto(product, ListImgSave);
                return Ok(lst);
            }
            catch (Exception ex)
            {
                negocio.deletefiles(ListImgSave);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

    }
}
