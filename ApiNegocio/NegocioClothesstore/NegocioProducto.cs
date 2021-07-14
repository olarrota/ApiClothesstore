
using ApiDatos.Models;
using ApiNegocio.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace ApiNegocio.NegocioClothesstore
{
    public class NegocioProducto
    {

        #region Producto

        public List<ViewModelProducto> getProductos()
        {
            try
            {
                BDLATAMContext context = new BDLATAMContext();
                List<ViewModelProducto> product = new List<ViewModelProducto>();
                //List<ViewModelImagenes> listImg = new List<ViewModelImagenes>();
                return (from produc in context.Productos
                        select new ViewModelProducto
                        {
                            Id = produc.Id,
                            Nombre = produc.Nombre,
                            Descripcion = produc.Descripcion,
                            Precio = produc.Precio,
                            Descuento = produc.Descuento,
                            PrecioDtc = (produc.Precio == null ? 0 : produc.Precio - ((produc.Precio * produc.Descuento) / 100)),
                            PaisId = produc.PaisId,
                            visualizaciones = produc.Visualizaciones,
                            ListImagenes = (from ima in context.Imagenes
                                            where ima.ProductosId == produc.Id
                                            select new ViewModelImagenes
                                            {
                                                Id = ima.Id,
                                                Nombre = ima.Nombre,
                                                Ruta = ima.Ruta,
                                                ProductosId = ima.ProductosId,
                                                TiposImagenId = ima.TiposImagenId,
                                            }).ToList()
                        }).ToList();

                //foreach (var item in product)
                //{
                //    listImg=(from ima in context.Imagenes
                //     select new ViewModelImagenes
                //     {
                //         Id = ima.Id,
                //         Nombre = ima.Nombre,
                //         ProductosId = ima.ProductosId,
                //         TiposImagenId = ima.TiposImagenId,
                //         Img = ima.Img
                //     }).ToList();
                //    if (listImg.Count()>0)
                //    {
                //        item.ListImagenes = listImg;
                //    }  
                //}
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<ViewModelProducto> getProductosDestacados()
        {
            try
            {
                BDLATAMContext context = new BDLATAMContext();

                return (from produc in context.Productos
                        where produc.Visualizaciones >= 1
                        select new ViewModelProducto
                        {
                            Id = produc.Id,
                            Nombre = produc.Nombre,
                            Descripcion = produc.Descripcion,
                            Precio = produc.Precio,
                            Descuento = produc.Descuento,
                            PrecioDtc = (produc.Precio == null ? 0 : produc.Precio - ((produc.Precio * produc.Descuento) / 100)),
                            PaisId = produc.PaisId,
                            visualizaciones = produc.Visualizaciones,
                            ListImagenes = (from ima in context.Imagenes
                                            where ima.ProductosId == produc.Id
                                            select new ViewModelImagenes
                                            {
                                                Id = ima.Id,
                                                Nombre = ima.Nombre,
                                                Ruta = ima.Ruta,
                                                ProductosId = ima.ProductosId,
                                                TiposImagenId = ima.TiposImagenId,
                                            }).ToList()

                        }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<ViewModelProducto> addProducto(ViewModelCrearProducto prod, List<ViewModelImagenes> ListImgSave)
        {
            BDLATAMContext context = new BDLATAMContext();
            using (var transaction = context.Database.BeginTransaction())
            //using (DbContextTransaction transaction = (DbContextTransaction)context.Database.BeginTransaction())
            {
                try
                {
                    Productos productoNew = new Productos
                    {
                        Nombre = prod.Nombre,
                        Descripcion = prod.Descripcion,
                        Precio = prod.Precio,
                        Descuento = prod.Descuento,
                        PaisId = prod.PaisId,
                        Visualizaciones = 0
                    };
                    var model = context.Productos;
                    model.Add(productoNew);
                    context.SaveChanges();

                    if (ListImgSave.Count() > 0)
                    {
                        Imagenes img;
                        var modelImg = context.Imagenes;
                        foreach (var item in ListImgSave)
                        {

                            img = new Imagenes
                            {
                                Nombre = item.Nombre,
                                Ruta = item.Ruta,
                                ProductosId = productoNew.Id,
                                TiposImagenId = item.TiposImagenId,
                            };
                            modelImg.Add(img);
                            context.SaveChanges();
                        }
                    }

                    transaction.Commit();
                    return getProductos();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw new Exception(e.Message);
                }
            }
        }

        public bool validarDescuento(int? PaisId, decimal? descuento)
        {
            try
            {
                bool respuesta = true;
                BDLATAMContext context = new BDLATAMContext();
                ReglasDtc reglas = new ReglasDtc();
                reglas = context.ReglasDtcs.Where(x => x.PaisId == PaisId).SingleOrDefault();
                if (reglas!=null)
                {
                    if (descuento>reglas.ValorDtc )
                    {
                        respuesta = false;
                    }
                }
                
                return respuesta;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void deletefiles(List<ViewModelImagenes> ListImgSave)
        {
            foreach (var item in ListImgSave)
            {
                if (System.IO.File.Exists(item.Ruta))
                {
                    System.IO.File.Delete(item.Ruta);
                }
            }
        }

        #endregion
    }
}
