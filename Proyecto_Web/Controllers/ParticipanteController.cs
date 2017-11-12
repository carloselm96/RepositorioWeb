using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Web.Models.Context;
using Proyecto_Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace Proyecto_Web.Controllers
{
    public class ParticipanteController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            ParticipantesContext context = HttpContext.RequestServices.GetService(typeof(ParticipantesContext)) as ParticipantesContext;
            var participantes = context.getParticipantes();
            return View(participantes);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Nuevo(string result)
        {
            //Nombre, apellidop apellidom fecha nac disciplina equipo
            if (!String.IsNullOrEmpty(result))
            {
                ViewBag.result = result;
            }
            DisciplinaContext disciplinas = HttpContext.RequestServices.GetService(typeof(DisciplinaContext)) as DisciplinaContext;
            EquipoContext equipos = HttpContext.RequestServices.GetService(typeof(EquipoContext)) as EquipoContext;
            ViewBag.equipos = equipos.GetAllEquipos();
            ViewBag.disciplinas = disciplinas.getDisciplinas();
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Nuevo(int selectEquipo, string inputFNac, string inputAM, string inputAP, string inputNombre, int selectDisciplina)
        {
            ParticipantesContext context = HttpContext.RequestServices.GetService(typeof(ParticipantesContext)) as ParticipantesContext;
            bool result=context.nuevoParticipante(inputNombre, inputAP, inputAM, inputFNac,selectDisciplina, selectEquipo);
            if (result)
            {
                return RedirectToAction("Nuevo", "Participante", new { result = "Success" });
            }
            return RedirectToAction("Nuevo", "Participante", new { result = "Failure" });
        }
    }
}