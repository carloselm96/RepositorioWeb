using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Proyecto_Web.Models.Context;

namespace Proyecto_Web.Controllers
{
    public class AdministracionController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Participantes()
        {
            ParticipantesContext context = HttpContext.RequestServices.GetService(typeof(ParticipantesContext)) as ParticipantesContext;
            var participantes = context.getParticipantes();
            return View(participantes);
        }

        

        [Authorize]
        public IActionResult Partidos()
        {
            PartidoContext context = HttpContext.RequestServices.GetService(typeof(PartidoContext)) as PartidoContext;
            var partidos = context.getPartidos();
            return View(partidos);
        }

        [Authorize]
        public IActionResult Equipos()
        {
            EquipoContext context = HttpContext.RequestServices.GetService(typeof(EquipoContext)) as EquipoContext;
            var equipos = context.GetAllEquipos();
            return View(equipos);
        }

        [Authorize]
        public IActionResult Competencias()
        {
            CompetenciaContext context = HttpContext.RequestServices.GetService(typeof(CompetenciaContext)) as CompetenciaContext;
            var competencias = context.getCompetencias();            
            //List < Competencia > competenciaList= context.getCompetencias();
            return View(competencias);            
        }
    }
}