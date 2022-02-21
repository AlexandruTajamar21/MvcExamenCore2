using MvcExamenCore2.Models;
using ProyectoTiendaMagic.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcExamenCore2.Repositories
{
    public class RepositoryUsuarios
    {
        private UserContext con;

        public RepositoryUsuarios(UserContext con)
        {
            this.con = con;
        }

        public Usuario ConfirmarUsuario(string correo, string contraseña)
        {
            var consulta = from datos in this.con.Usuarios
                           where datos.Email == correo && datos.Pass == contraseña
                           select datos;
            return consulta.SingleOrDefault();
        }

        internal Usuario getUsuario(int userId)
        {
            var consulta = from datos in this.con.Usuarios
                           where datos.IdUsuario == userId
                           select datos;
            return consulta.FirstOrDefault();
        }
    }
}
