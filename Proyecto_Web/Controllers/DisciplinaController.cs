using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Web.Models.Context;
using Microsoft.AspNetCore.Http;

namespace Proyecto_Web.Controllers
{
    public class DisciplinaController : Controller
    {
        public IActionResult Index()   
        {
            DisciplinaContext context= HttpContext.RequestServices.GetService(typeof(DisciplinaContext)) as DisciplinaContext;
            var disciplinas=context.getDisciplinas();
            return View(disciplinas);
        }
    }
}