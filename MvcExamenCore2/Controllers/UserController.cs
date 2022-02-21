using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using MvcExamenCore2.Models;
using MvcExamenCore2.Repositories;
using ProyectoTiendaMagic.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MvcExamenCore2.Controllers
{
    public class UserController : Controller
    {
        private RepositoryUsuarios repo;
        private RepositoryInventario repoinv;

        public UserController(RepositoryUsuarios repo,RepositoryInventario repoinv)
        {
            this.repo = repo;
            this.repoinv = repoinv;
        }
        [AuthorizeUsers]
        public IActionResult LogInUsuario()
        {
            return RedirectToAction("Index","Home");
        }
        [AuthorizeUsers]
        public IActionResult PerfilUsuario()
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            Usuario user = this.repo.getUsuario(userId);
            return View(user);
        }

        public IActionResult Pedidos()
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            List<VistaProductos> pedidos = this.repoinv.getPedidosUser(userId);
            return View(pedidos);
        }
    }
}
