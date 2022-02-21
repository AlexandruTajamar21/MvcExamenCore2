using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MvcExamenCore2.Models;
using MvcExamenCore2.Repositories;
using ProyectoTiendaMagic.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MvcExamenCore2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private RepositoryInventario repolibros;

        public HomeController(ILogger<HomeController> logger, RepositoryInventario repolibros)
        {
            _logger = logger;
            this.repolibros = repolibros;
        }

        public IActionResult Index()
        {
            List<Libro> libros = this.repolibros.getAllLibros();
            ViewData["GENEROS"] = this.repolibros.getGeneros();
            return View(libros);
        }

        public IActionResult DetallesLibro(int idLibro)
        {
            Libro libro = this.repolibros.getLibro(idLibro);
            return View(libro);
        }

        public IActionResult InsertarCarrito(int idLibro)
        {
            Libro libro = this.repolibros.getLibro(idLibro);
            List<int> items;
            if (HttpContext.Session.GetString("CARRITO") == null)
            {
                items = new List<int>();
            }
            else
            {
                items = HttpContext.Session.GetObject<List<int>>("CARRITO");
            }
            items.Add(libro.IdLibro);
            HttpContext.Session.SetObject("CARRITO", items);
            return RedirectToAction("Index");
        }

        public IActionResult Carrito()
        {
            List<Libro> Libros = new List<Libro>();
            if (HttpContext.Session.GetString("CARRITO") != null)
            {
                List<int> items = HttpContext.Session.GetObject<List<int>>("CARRITO");
                foreach(var it in items)
                {
                    Libro libro = this.repolibros.getLibro(it);
                    Libros.Add(libro);
                }
                return View(Libros);
            }
            else
            {
                return View();
            }

        }
        public IActionResult EliminarElemento(int idLibro)
        {
            if (HttpContext.Session.GetString("CARRITO") != null)
            {
                List<int> items = HttpContext.Session.GetObject<List<int>>("CARRITO");
                items.Remove(idLibro);
                HttpContext.Session.SetObject("CARRITO", items);
            }
            return RedirectToAction("Carrito");
        }

        public IActionResult ComprarCarrito()
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            List<Libro> Libros = new List<Libro>();
            if (HttpContext.Session.GetString("CARRITO") != null)
            {
                List<int> items = HttpContext.Session.GetObject<List<int>>("CARRITO");
                foreach (var it in items)
                {
                    Libro libro = this.repolibros.getLibro(it);
                    this.repolibros.CompraLibro(libro,userId);
                }
            }
            HttpContext.Session.Clear();
            return RedirectToAction("Pedidos","User");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
