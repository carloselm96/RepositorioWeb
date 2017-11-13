using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Proyecto_Web.Models.Context;

namespace Proyecto_Web.Controllers
{
    public class EventoController : Controller
    {
        [Authorize]        
        public IActionResult Index()
        {
            EventoContext context = HttpContext.RequestServices.GetService(typeof(EventoContext)) as EventoContext;
            var eventos = context.getEventos();
            return View(eventos);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Nuevo(string result)
        {
            if (!String.IsNullOrEmpty(result))
            {
                ViewBag.result = result;
            }                        
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Nuevo(string inputNombre, string inputInicio, string inputFinal)
        {
            EventoContext context = HttpContext.RequestServices.GetService(typeof(EventoContext)) as EventoContext;
            bool result = context.Add(inputNombre, inputInicio, inputFinal);
            if (result)
            {
                return RedirectToAction("Nuevo", "Evento", new { result = "Success" });
            }
            return RedirectToAction("Nuevo", "Evento", new { result = "Failure" });
        }

        [Authorize]
        [HttpGet]
        public IActionResult Editar(int id,string result)
        {
            if (!String.IsNullOrEmpty(result))
            {
                ViewBag.result = result;
            }
            EventoContext context = HttpContext.RequestServices.GetService(typeof(EventoContext)) as EventoContext;
            var evento=context.detallesEvento(id);
            return View(evento);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Editar(int id, string inputNombre, string inputInicio, string inputFinal)
        {
            EventoContext context = HttpContext.RequestServices.GetService(typeof(EventoContext)) as EventoContext;
            bool result = context.Edit(id,inputNombre, inputInicio, inputFinal);
            if (result)
            {
                return RedirectToAction("Editar", "Evento", new {id=id, result = "Success" });
            }
            return RedirectToAction("Editar", "Evento", new {id=id, result = "Failure" });
        }
    }
}