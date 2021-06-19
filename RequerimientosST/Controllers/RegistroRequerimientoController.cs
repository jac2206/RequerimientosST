using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RequerimientosST.Models;
using System.Threading;
using System.Globalization;
using Kendo.Mvc.Extensions;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace RequerimientosST.Controllers
{
    public class RegistroRequerimientoController : Controller
    {
        // GET: RegistroRequerimiento
        public ActionResult Index()
        {
            ////var url = "http://localhost/RequerimientoSTApi/api/Requerimientos/ObtenerRequerimientos";
            ////var request = (HttpWebRequest)WebRequest.Create(url);
            ////request.Method = "GET";
            ////request.ContentType = "application/json";
            ////request.Accept = "application/json";

            ////try
            ////{
            ////    using (WebResponse response = request.GetResponse())
            ////    {
            ////        using (Stream strReader = response.GetResponseStream())
            ////        {
            ////            if (strReader == null) ;
            ////            using (StreamReader objReader = new StreamReader(strReader))
            ////            {
            ////                string responseBody = objReader.ReadToEnd();
            ////                // Do something with responseBody
            ////                //Console.WriteLine(responseBody);
            ////                List<uspObtenerRequerimientos_Result> ObjOrderList = JsonConvert.DeserializeObject<List<uspObtenerRequerimientos_Result>>(responseBody);

            ////                var ObjOrderList2 = JsonConvert.DeserializeObject<List<uspObtenerRequerimientos_Result>>(responseBody);

            ////            }
            ////        }
            ////    }

            ////}

            ////catch(Exception ex)
            ////{

            ////}
            return View();

        }

        public JsonResult ObtenerAreaLista()
        {
            try
            {
                using (var DB = new PruebaTecnicaJulianArangoEntities())
                {

                    var resultado = DB.uspObtenerMaestroArea().ToList();
                    List<Object> viewModel = new List<object>();
                    foreach (var resultadoItem in resultado)
                    {
                        viewModel.Add(new
                        {
                            Id = resultadoItem.Id,
                            Nombre = resultadoItem.Nombre
                        });
                    }

                    // DataSourceResult result = viewModel.ToDataSourceResult(request);

                    return Json(viewModel, JsonRequestBehavior.AllowGet);

                }

            }

            catch (Exception ex)
            {
                //return new HttpStatusCodeResult(550, "Some error" + ex.Message);
            }
            return Json(100);
        }

        public JsonResult ObtenerPrioridadLista()
        {
            try
            {
                using (var DB = new PruebaTecnicaJulianArangoEntities())
                {

                    var resultado = DB.uspObtenerMaestroPrioridad().ToList();
                    List<Object> viewModel = new List<object>();
                    foreach (var resultadoItem in resultado)
                    {
                        viewModel.Add(new
                        {
                            Id = resultadoItem.Id,
                            Prioridad = resultadoItem.Prioridad
                        });
                    }


                    return Json(viewModel, JsonRequestBehavior.AllowGet);

                }

            }

            catch (Exception ex)
            {
                //return new HttpStatusCodeResult(550, "Some error" + ex.Message);
            }
            return Json(100);
        }

        public JsonResult ObtenerIngenieroLista()
        {
            try
            {
                using (var DB = new PruebaTecnicaJulianArangoEntities())
                {

                    var resultado = DB.uspObtenerMaestroIngeniero().ToList();
                    List<Object> viewModel = new List<object>();
                    foreach (var resultadoItem in resultado)
                    {
                        viewModel.Add(new
                        {
                            Id = resultadoItem.Id,
                            Ingeniero = resultadoItem.Ingeniero
                        });
                    }


                    return Json(viewModel, JsonRequestBehavior.AllowGet);

                }

            }

            catch (Exception ex)
            {
                //return new HttpStatusCodeResult(550, "Some error" + ex.Message);
            }
            return Json(100);
        }


        public ActionResult ObtenerRequerimientos([DataSourceRequest] DataSourceRequest request)
        {
            try
            {

                using (var DB = new PruebaTecnicaJulianArangoEntities())
                {

                    Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

                    IQueryable<uspObtenerRequerimientos_Result> obtenerRequerimientosLista = (IQueryable<uspObtenerRequerimientos_Result>)DB.uspObtenerRequerimientos().AsQueryable();
                    DataSourceResult result = obtenerRequerimientosLista.ToList().ToDataSourceResult(request);
                    return Json(result);

                }

            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(550, "Some error" + ex.Message);
            }

        }

      

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CrearRegistroRequerimiento([DataSourceRequest] DataSourceRequest request, uspObtenerRequerimientos_Result requerimientoEdit)
        {
            try
            {
                using (var DB = new PruebaTecnicaJulianArangoEntities())
                {


                    if (requerimientoEdit != null && ModelState.IsValid)
                    {
                        DB.uspGuardarRequerimientos(
                            requerimientoEdit.Nombre,
                            requerimientoEdit.Aplicativo,
                            requerimientoEdit.Alcance,
                            requerimientoEdit.FechaSolicitud,
                            requerimientoEdit.Prioridad,
                            requerimientoEdit.Ingeniero,
                            requerimientoEdit.DiasDesarrollo,
                            requerimientoEdit.FechaDesarrollo);
                        DB.SaveChanges();
                    }

                    return Json(new[] { requerimientoEdit }.ToDataSourceResult(request, ModelState));



                }

            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }

        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ActualizarRequerimiento([DataSourceRequest] DataSourceRequest request, uspObtenerRequerimientos_Result requerimientoEdit)
        {
            try
            {
                using (var DB = new PruebaTecnicaJulianArangoEntities())
                {

                    if (requerimientoEdit != null && ModelState.IsValid)
                    {
                        DB.uspActualizarRequerimientos(
                            requerimientoEdit.Id,
                            requerimientoEdit.Nombre,
                            requerimientoEdit.Aplicativo,
                            requerimientoEdit.Alcance,
                            requerimientoEdit.FechaSolicitud,
                            requerimientoEdit.Prioridad,
                            requerimientoEdit.Ingeniero,
                            requerimientoEdit.DiasDesarrollo,
                            requerimientoEdit.FechaDesarrollo);
                        DB.SaveChanges();
                    }

                    return Json(new[] { requerimientoEdit }.ToDataSourceResult(request, ModelState));


                }

            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(550, "Some error" + ex.Message);
            }

        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EliminarRequerimiento([DataSourceRequest] DataSourceRequest request, uspObtenerRequerimientos_Result referenciasEdit)
        {
            try
            {
                using (var DB = new PruebaTecnicaJulianArangoEntities())
                {


                    if (referenciasEdit != null && ModelState.IsValid)
                    {
                        DB.uspEliminarRequerimientos(referenciasEdit.Id);
                        DB.SaveChanges();
                    }

                    return Json(new[] { referenciasEdit }.ToDataSourceResult(request, ModelState));



                }

            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(550, "Some error" + ex.Message);
            }

        }


    }
}