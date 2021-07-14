using ApiLatam.Controllers;
using ApiNegocio.Entidades;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Xunit;

namespace ProductosTest
{
    public class UnitTest1
    {

        [Fact]
        public void Test_AddProductos_Descuento_invalido()
        {
            // Arrange
            ViewModelCrearProducto productoNew = new ViewModelCrearProducto
            {
                Nombre = "Camisa",
                Descripcion = "Camisa roja",
                Precio = 50000,
                Descuento = 60,
                PaisId = 1//Colombia
            };
            List<IFormFile> files = new List<IFormFile>();
            IWebHostEnvironment environment = null;
            var controller = new ProductosController(environment);
     
            // Act
            IActionResult result =  controller.AddProductos(files, productoNew);
            BadRequestObjectResult okResult = result as BadRequestObjectResult;
            // Assert

            Assert.Equal(400, okResult.StatusCode);
        }
        [Fact]
        public void Test_AddProductos_Descuento_valido()
        {
            // Arrange
            ViewModelCrearProducto productoNew = new ViewModelCrearProducto
            {
                Nombre = "Camisa",
                Descripcion = "Camisa roja",
                Precio = 50000,
                Descuento = 30,
                PaisId = 1//Colombia
            };
            List<IFormFile> files = new List<IFormFile>();

            IWebHostEnvironment environment = null;
            var controller = new ProductosController(environment);

            // Act
            IActionResult result = controller.AddProductos(files, productoNew);
            OkObjectResult okResult = result as OkObjectResult;
            // Assert
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public void Test_GetProductos_Consulta_validaProductos()
        {
            // Arrange
            IWebHostEnvironment environment = null;
            var controller = new ProductosController(environment);

            // Act
            IActionResult result = controller.GetProductos();
            OkObjectResult okResult = result as OkObjectResult;
            // Assert
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public void Test_GetProductos_Consulta_validaProductosDestacados()
        {
            // Arrange
            IWebHostEnvironment environment = null;
            var controller = new ProductosController(environment);

            // Act
            IActionResult result = controller.GetProductosDestacados();
            OkObjectResult okResult = result as OkObjectResult;
            // Assert
            Assert.Equal(200, okResult.StatusCode);
        }
    }
}
