using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Proyecto_Web.Models.Context;

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

        /*[Authorize]
        public IActionResult SeeCompetencias()
        {
            CompetenciaContext context = HttpContext.RequestServices.GetService(typeof(CompetenciaContext)) as CompetenciaContext;
            var competencias = context.getCompetencias();            
            return PartialView(competencias);
        }*/
    }
}