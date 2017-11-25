using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Web.Models.Context;
using Microsoft.AspNetCore.Authorization;
using Proyecto_Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Proyecto_Web.Controllers
{
    public class PartidoController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            PartidoContext context= HttpContext.RequestServices.GetService(typeof(PartidoContext)) as PartidoContext;
            var partidos = context.getPartidos();
            return View(partidos);
        }
        [Authorize]
        [HttpGet]
        public IActionResult Nuevo(string result)
        {
            if (!String.IsNullOrEmpty(result))
            {
                ViewBag.result = result;
            }
            EquipoContext equipos= HttpContext.RequestServices.GetService(typeof(EquipoContext)) as EquipoContext;
            DisciplinaContext disciplinas = HttpContext.RequestServices.GetService(typeof(DisciplinaContext)) as DisciplinaContext;
            UbicacionContext ubicaciones = HttpContext.RequestServices.GetService(typeof(UbicacionContext)) as UbicacionContext;
            EventoContext eventos = HttpContext.RequestServices.GetService(typeof(EventoContext)) as EventoContext;
            ViewBag.equipos = equipos.GetAllEquipos();
            ViewBag.disciplinas = disciplinas.getDisciplinas();
            ViewBag.eventos = eventos.getEventos();
            ViewBag.ubicaciones = ubicaciones.getUbicaciones();
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Nuevo(string inputFecha,string inputHora, int selectDisciplina, int selectEquipo1, int selectEquipo2, int selectUbicacion, int selectEvento)
        {
            PartidoContext context = HttpContext.RequestServices.GetService(typeof(PartidoContext)) as PartidoContext;
            bool result = context.nuevoPartido(inputFecha,inputHora, selectDisciplina, selectEquipo1, selectEquipo2, selectUbicacion, selectEvento);
            if (result)
            {
                return RedirectToAction("Nuevo", "Partido", new { result = "Success" });
            }
            return RedirectToAction("Nuevo", "Partido", new { result = "Failure" });
        }

        [HttpGet]
        public JsonResult equipoJson(int id)
        {
            EquipoContext context = HttpContext.RequestServices.GetService(typeof(EquipoContext)) as EquipoContext;
            List<Equipo> equipos = context.GetAllEquipos();
            equipos = equipos.Where(equipo => equipo.disciplina.id.Equals(id)).ToList();
            return Json(new SelectList(equipos, "id", "nombre"));
        }

        [HttpGet]
        public JsonResult equipoJson2(int id, int id2)
        {
            EquipoContext context = HttpContext.RequestServices.GetService(typeof(EquipoContext)) as EquipoContext;
            List<Equipo> equipos = context.GetAllEquipos();
            equipos = equipos.Where(equipo => equipo.disciplina.id.Equals(id)).ToList();
            equipos = equipos.Where(equipo => equipo.id.Equals(id2)==false).ToList();
            return Json(new SelectList(equipos, "id", "nombre"));
        }
    }
}