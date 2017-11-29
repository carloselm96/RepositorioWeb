using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Web.Models.Context;
using Proyecto_Web.Models;
using Microsoft.AspNetCore.Authorization;

namespace Proyecto_Web.Controllers
{
    public class GremioController : Controller
    {        
        [Authorize]
        public IActionResult Index()
        {
            GremioContext context = HttpContext.RequestServices.GetService(typeof(GremioContext)) as GremioContext;
            var gremios = context.getGremios();
            return View(gremios);
        }

        [Authorize]
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

        [Authorize]
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

        [HttpGet]
        public IActionResult Eliminar(int id)
        {
            GremioContext context = HttpContext.RequestServices.GetService(typeof(GremioContext)) as GremioContext;
            bool result = context.eliminar(id);
            if (result)
            {
                return RedirectToAction("Index", "Gremio");
            }
            return RedirectToAction("Index", "Gremio");
        }
    }
}
