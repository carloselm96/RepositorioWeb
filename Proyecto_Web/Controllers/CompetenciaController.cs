using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Proyecto_Web.Models.Context;
using Proyecto_Web.Models;

namespace Proyecto_Web.Controllers
{
    public class CompetenciaController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            CompetenciaContext context = HttpContext.RequestServices.GetService(typeof(CompetenciaContext)) as CompetenciaContext;
            var competencias = context.getCompetencias();
            return View(competencias);
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
            UbicacionContext ubicaciones = HttpContext.RequestServices.GetService(typeof(UbicacionContext)) as UbicacionContext;
            ParticipantesContext participantes = HttpContext.RequestServices.GetService(typeof(ParticipantesContext)) as ParticipantesContext;
            List<Participante> lista_p= participantes.getParticipantes();
            lista_p = lista_p.Where(x => x.disciplina.id.Equals(5)).ToList();
            ViewBag.eventos = eventos.getEventos();
            ViewBag.ubicaciones = ubicaciones.getUbicaciones();
            ViewBag.participantes = lista_p;
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Nuevo(int selectEvento, string inputNombre, string inputDescripcion, int selectUbicacion, string inputFecha, string inputHora, List<int> selectParticipantes)
        {
            CompetenciaContext context = HttpContext.RequestServices.GetService(typeof(CompetenciaContext)) as CompetenciaContext;
            bool result = context.Add1(selectEvento,inputNombre,inputDescripcion,selectUbicacion,inputFecha,inputHora);
            if (result == true)
            {
                int id = context.lastInserted();
                result=context.Add2(id, selectParticipantes);
                return RedirectToAction("Nuevo", "Competencia", new { result = "Success" });
            }
            return RedirectToAction("Nuevo", "Competencia", new { result = "Fail" });
        }

        [Authorize]
        public IActionResult Detalles(int id)
        {
            CompetenciaContext context = HttpContext.RequestServices.GetService(typeof(CompetenciaContext)) as CompetenciaContext;
            var competencia = context.GetCompetencia(id);
            return View(competencia);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Registrar(int idc, int idp, float puntaje)
        {
            CompetenciaContext context = HttpContext.RequestServices.GetService(typeof(CompetenciaContext)) as CompetenciaContext;
            var competencia = context.RegistrarResultados(idc, idp, puntaje);
            return RedirectToAction("Detalles", "Competencia", new { id = idc });
        }

        [Authorize]        
        public IActionResult Delete(int id)
        {
            CompetenciaContext context = HttpContext.RequestServices.GetService(typeof(CompetenciaContext)) as CompetenciaContext;
            var competencia = context.Eliminar(id);
            return RedirectToAction("Index", "Competencia", new { id = id });
        }
    }
}