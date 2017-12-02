using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Web.Models.Context;
using Microsoft.AspNetCore.Authorization;

namespace Proyecto_Web.Controllers
{
    public class NoticiaController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            NoticiaContext context = HttpContext.RequestServices.GetService(typeof(NoticiaContext)) as NoticiaContext;
            var noticias = context.getNoticias();
            return View(noticias);            
        }

        [Authorize]
        [HttpGet]
        public IActionResult Nuevo(string result)
        {
            if (result != null)
            {
                ViewBag.result = result;
            }
            EventoContext eventos = HttpContext.RequestServices.GetService(typeof(EventoContext)) as EventoContext;
            ViewBag.eventos = eventos.getEventos();            
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Nuevo(string inputNombre, string inputFecha, int selectEvento, string inputNoticia)
        {
            NoticiaContext context = HttpContext.RequestServices.GetService(typeof(NoticiaContext)) as NoticiaContext;
            bool result = context.nuevaNoticia(inputNombre, inputFecha, inputNoticia, selectEvento);
            if (result)
            {
                return RedirectToAction("Nuevo", "Noticia", new { result = "Success" });
            }
            return RedirectToAction("Nuevo", "Noticia", new { result = "Failure" });
        }

        [Authorize]
        [HttpGet]
        public IActionResult Edit(int id, string result)
        {
            if (result != null)
            {
                ViewBag.result = result;
            }
            EventoContext eventos = HttpContext.RequestServices.GetService(typeof(EventoContext)) as EventoContext;
            NoticiaContext noticia = HttpContext.RequestServices.GetService(typeof(NoticiaContext)) as NoticiaContext;
            ViewBag.noticia = noticia.getNoticia(id);
            ViewBag.eventos = eventos.getEventos();
            return View();
        }
    }
}