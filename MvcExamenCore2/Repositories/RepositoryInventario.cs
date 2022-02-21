using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MvcExamenCore2.Models;
using ProyectoTiendaMagic.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

#region Procedures

//create procedure SP_COMPRA_LIBRO
//(@IdPedido int, @IdFactura int, @Fecha date, @IdLibro int, @IdUsuario int, @Cantidad int)
//AS
//	insert into PEDIDOS
//	values(@IdPedido,@IdFactura,@Fecha, @IdLibro, @IdUsuario, @Cantidad)
//GO
#endregion

namespace MvcExamenCore2.Repositories
{
    public class RepositoryInventario
    {
        private UserContext context;

        public RepositoryInventario(UserContext context)
        {
            this.context = context;
        }

        public List<Genero> getGeneros()
        {
            var consulta = from datos in this.context.Generos
                           select datos;
            return consulta.ToList();
        }

        public List<Libro> getLisbrosGenero(int idGenero)
        {
            var consulta = from datos in this.context.Libros
                           where datos.IdGenero == idGenero
                           select datos;
            return consulta.ToList();
        }

        public List<Libro> getAllLibros()
        {
            var consulta = from datos in this.context.Libros
                           select datos;

            return consulta.ToList();
        }

        internal List<VistaProductos> getPedidosUser(int userId)
        {
            var consulta = from datos in this.context.VistaProductos
                           where datos.IdUsuario == userId
                           select datos;

            return consulta.ToList();
        }

        public Libro getLibro(int idLibro)
        {
            var consulta = from datos in this.context.Libros
                           where datos.IdLibro == idLibro
                           select datos;
            return consulta.FirstOrDefault();
        }

        internal void CompraLibro(Libro libro, int userId)
        {
            DateTime date = new DateTime();
            string fecha = date.ToString("dd/M/yyyy");
            string sql = "SP_COMPRA_LIBRO @IdPedido, @IdFactura, @Fecha, @IdLibro, @IdUsuario, @Cantidad";
            SqlParameter pamidPedido = new SqlParameter("@IdPedido", this.GetMaxIdPedido());
            SqlParameter pamIdFactura = new SqlParameter("@IdFactura", this.GetMaxIdFactura());
            SqlParameter pamFecha = new SqlParameter("@Fecha",fecha);
            SqlParameter pamIdLibro = new SqlParameter("@IdLibro", libro.IdLibro);
            SqlParameter pamIdUsuario = new SqlParameter("@IdUsuario", userId);
            SqlParameter pamCantidad = new SqlParameter("@Cantidad", 1);

            this.context.Database.ExecuteSqlRaw(sql, pamidPedido,pamIdFactura, pamFecha, pamIdLibro,pamIdUsuario,pamCantidad);
        }

        public int GetMaxIdPedido()
        {
            if (this.context.Pedidos.Count() == 0)
            {
                return 1;
            }
            else
            {
                return this.context.Pedidos.Max(z => z.IdPedido) + 1;
            }
        }
        public int GetMaxIdFactura()
        {
            if (this.context.Pedidos.Count() == 0)
            {
                return 1;
            }
            else
            {
                return this.context.Pedidos.Max(z => z.IdFactura) + 1;
            }
        }
    }
}
