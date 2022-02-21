using Microsoft.EntityFrameworkCore;
using MvcExamenCore2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoTiendaMagic.Data
{
    
    public class UserContext :DbContext
    {
        public UserContext(DbContextOptions<UserContext> context) : base(context) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Genero> Generos { get; set; }
        public DbSet<Libro> Libros { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<VistaProductos> VistaProductos { get; set; }
    }
}
