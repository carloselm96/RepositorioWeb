using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Web.Models;
using Proyecto_Web.Models.Context;

namespace Proyecto_Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            EventoContext context = HttpContext.RequestServices.GetService(typeof(EventoContext)) as EventoContext;
            ViewBag.eventos = context.getEventos();
            ViewBag.eventoact = context.EventoActual();
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Resultados()
        {
            return View();
        }

        public IActionResult Index2()
        {
            return View();
        }
    }
}
