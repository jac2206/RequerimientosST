using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Newtonsoft.Json;
using RequerimientosST.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RequerimientosST.Logic;

namespace RequerimientosST.Controllers
{
    public class APIRequerimientoController : Controller
    {
        // GET: APIRequerimiento
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ObtenerRequerimientosAPI([DataSourceRequest] DataSourceRequest request)
        {
            try
            {

                string responseBody = APILogic.ObtenerRequerimientoAPI("http://localhost/RequerimientoSTApi/api/Requerimientos/ObtenerRequerimientos");
                List<uspObtenerRequerimientos_Result> ObjOrderList = JsonConvert.DeserializeObject<List<uspObtenerRequerimientos_Result>>(responseBody);
                var ObjOrderList2 = JsonConvert.DeserializeObject<List<uspObtenerRequerimientos_Result>>(responseBody);
                DataSourceResult result = ObjOrderList2.ToList().ToDataSourceResult(request);
                return Json(result);


            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(550, "Some error" + ex.Message);
            }

        }

    }
}