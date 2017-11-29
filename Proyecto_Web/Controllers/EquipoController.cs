using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Web.Models.Context;
using Microsoft.AspNetCore.Authorization;
using Proyecto_Web.Models;

namespace Proyecto_Web.Controllers
{
    public class EquipoController : Controller
    {
        [Authorize]       
        public IActionResult Index(string sortOrder, string bus)
        {   
           EquipoContext context = HttpContext.RequestServices.GetService(typeof(EquipoContext)) as EquipoContext;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            var equipos = context.GetAllEquipos();            
            return View(equipos);
        }        
        
        [Authorize]
        [HttpGet]
       public IActionResult Nuevo(string result)
        {
            if (result != null)
            {
                ViewBag.result = result;
            }
            DisciplinaContext disciplina = HttpContext.RequestServices.GetService(typeof(DisciplinaContext)) as DisciplinaContext;
            GremioContext gremio= HttpContext.RequestServices.GetService(typeof(GremioContext)) as GremioContext;
            ViewBag.disciplinas = disciplina.getDisciplinas();
            ViewBag.gremios = gremio.getGremios();            
            return View();
        }
        
        [Authorize]
        [HttpPost]
        public IActionResult Nuevo(String inputNombre, int selectGremio, int selectDisciplina)
        {
            EquipoContext context = HttpContext.RequestServices.GetService(typeof(EquipoContext)) as EquipoContext;
            var result = context.Add(inputNombre,selectGremio,selectDisciplina);                       
            if (result == true)
            {                                
                return RedirectToAction("Nuevo", "Equipo", new { result = "Success" });
            }
            return RedirectToAction("Nuevo", "Equipo", new { result = "Fail" });

        }

        [Authorize]
        [HttpGet]
        public IActionResult detallesEquipo(string id)
        {
            EquipoContext context = HttpContext.RequestServices.GetService(typeof(EquipoContext)) as EquipoContext;
            var Equipo = context.detallesEquipo(id);
            return View(Equipo);
        }

        [Authorize]
        [HttpGet]
        public IActionResult editar(string id)
        {
            DisciplinaContext disciplina = HttpContext.RequestServices.GetService(typeof(DisciplinaContext)) as DisciplinaContext;
            GremioContext gremio = HttpContext.RequestServices.GetService(typeof(GremioContext)) as GremioContext;
            ViewBag.disciplinas = disciplina.getDisciplinas();
            ViewBag.gremios = gremio.getGremios();
            EquipoContext context = HttpContext.RequestServices.GetService(typeof(EquipoContext)) as EquipoContext;
            ViewBag.equipo = context.detallesEquipo(id);
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult editar(int id, String inputNombre, int selectGremio, int selectDisciplina)
        {
            EquipoContext context = HttpContext.RequestServices.GetService(typeof(EquipoContext)) as EquipoContext;
            bool result = context.Editar(id, inputNombre, selectGremio, selectDisciplina);
            if (result == true)
            {
                return RedirectToAction("detallesEquipo", "Equipo", new { id = id });
            }
            return RedirectToAction("detallesEquipo", "Equipo", new { id = id });            
        }


        [Authorize]    
        [HttpPost]
        public IActionResult eliminar(int id)
        {
            EquipoContext context = HttpContext.RequestServices.GetService(typeof(EquipoContext)) as EquipoContext;
            var result = context.eliminar(id);
            if (result == true)
            {
                return RedirectToAction("Index", "Equipo", new { result = "Success" });
            }
            return RedirectToAction("Index", "Equipo", new { result = "Fail" });
        }
    }

    
}