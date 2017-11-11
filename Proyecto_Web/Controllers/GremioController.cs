using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Web.Models.Context;

namespace Proyecto_Web.Controllers
{
    public class GremioController : Controller
    {        
        public IActionResult Index()
        {
            GremioContext context = HttpContext.RequestServices.GetService(typeof(GremioContext)) as GremioContext;
            var gremios = context.getGremios();
            return View(gremios);
        }
        [HttpGet]
        public IActionResult Nuevo(String result)
        {
            if (!String.IsNullOrEmpty(result))
            {
                ViewBag.result = result;
            }
            EstadoContext context = HttpContext.RequestServices.GetService(typeof(EstadoContext)) as EstadoContext;
            ViewBag.estados = context.getEstados();
            return View();
        }


        [HttpPost]
        public IActionResult Nuevo(int inputnGremio, string inputLocalidad, int selectEstado)
        {
            GremioContext context = HttpContext.RequestServices.GetService(typeof(GremioContext)) as GremioContext;
            bool result = context.nuevoGremio(inputnGremio, inputLocalidad, selectEstado);
            if (result)
            {
                return RedirectToAction("Nuevo", "Gremio", new { result = "Success" });
            }
            return RedirectToAction("Nuevo", "Gremio", new { result = "Fail" });            
        }
    }
}
