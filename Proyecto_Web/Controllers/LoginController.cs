using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Proyecto_Web.Models.Context;
using Proyecto_Web.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace Proyecto_Web.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(Usuario user)
        {
            UsuarioContext context = HttpContext.RequestServices.GetService(typeof(UsuarioContext)) as UsuarioContext;
            if (context.VerificarUsuario(user))
            {
                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Name, user.correo));

                var userIdentity = new ClaimsIdentity(claims, "login");

                var principal = new ClaimsPrincipal(userIdentity);

                await HttpContext.SignInAsync("PKAT", principal);

                return RedirectToRoute(new { controller = "Administracion", action = "Index" });

            }


            //return RedirectToRoute(new { controller = "Home", action = "Login" });
            ViewBag.msg = "Contraseña o Usuario incorrectos";
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            return Redirect("/Login");
        }
    }
}