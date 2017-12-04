using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Proyecto_Web.Models.Context;
using System.IO;
using Microsoft.AspNetCore.Http;
using Proyecto_Web.Models;

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
        public async Task<IActionResult> NuevoAsync(string inputNombre, string inputInicio, string inputFinal, IFormFile inputImagen)
        {
            EventoContext context = HttpContext.RequestServices.GetService(typeof(EventoContext)) as EventoContext;
            if (inputImagen == null || inputImagen.Length == 0)
                return Content("file not selected");            
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\", inputImagen.FileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await inputImagen.CopyToAsync(stream);
            }
            string filePath = inputImagen.FileName;


            bool result = context.Add(inputNombre, inputInicio, inputFinal, filePath);
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

        [Authorize]        
        public IActionResult Eliminar(int id)
        {
            EventoContext context = HttpContext.RequestServices.GetService(typeof(EventoContext)) as EventoContext;
            bool result = context.Eliminar(id);
            if (result)
            {
                return RedirectToAction("Index", "Evento", new { id = id, result = "Success" });
            }
            return RedirectToAction("Index", "Evento", new { id = id, result = "Failure" });
        }
        public IActionResult Evento(int id)
        {
            EventoContext context = HttpContext.RequestServices.GetService(typeof(EventoContext)) as EventoContext;
            CompetenciaContext context1 = HttpContext.RequestServices.GetService(typeof(CompetenciaContext)) as CompetenciaContext;
           PartidoContext  context2= HttpContext.RequestServices.GetService(typeof(PartidoContext)) as PartidoContext;
            List < Partido > partidos= context2.getPartidos();
            partidos = partidos.Where(x => x.evento.id == id).ToList();
            List<Competencia> competencias = context1.getCompetencias();
            competencias = competencias.Where(x => x.evento.id == id).ToList();
            ViewBag.Evento = context.detallesEvento(id);
            ViewBag.Partidos = partidos;
            ViewBag.Competencias = competencias;
            return View();
        }
    }
    
}