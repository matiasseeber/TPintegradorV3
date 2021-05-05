using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using Datos;
using System.Data;
using System.Data.SqlClient;

namespace Negocios
{
    public class UsuariosNegocios
    {
        public bool agregarUsuario(UsuariosEntidades usuario)
        {
            usuariosDatos usuariosDatos = new usuariosDatos();
            return usuariosDatos.agregarUsuario(usuario);
        }

        public bool verificarDni(UsuariosEntidades usuario)
        {
            string consulta = "select * from Usuarios where dniUsuario = '" + usuario.DniUsuario + "'";
            AccesoDatos accesoDatos = new AccesoDatos();
            return accesoDatos.existe(consulta);
        }

        public bool verificarEmail(UsuariosEntidades usuario)
        {
            string consulta = "select * from Usuarios where emailUsuario = '" + usuario.EmailUsuario + "'";
            AccesoDatos accesoDatos = new AccesoDatos();
            return accesoDatos.existe(consulta);
        }

        public DataTable cargarGrv(string consulta)
        {
            AccesoDatos datos = new AccesoDatos();
            return datos.ObtenerTabla("usuarios",consulta);
        }

        public bool borrarUsuario(string id)
        {
            usuariosDatos usuarios = new usuariosDatos();
            return usuarios.eliminarUsuario(id);
        }
        public bool editarUsuario(string id, Boolean admin)
        {
            usuariosDatos usuarios = new usuariosDatos();
            return usuarios.editarUsuario(id, admin);
        }

        public bool logearse(UsuariosEntidades usuarios)
        {
            string consulta = "select * from Usuarios where emailUsuario = '"+usuarios.EmailUsuario+"' and contraseña='"+usuarios.Contra+"'";
            AccesoDatos accesoDatos = new AccesoDatos();
            return accesoDatos.existe(consulta);
        }

        public DataTable obtenerUsuario(UsuariosEntidades usuarios)
        {
            string consulta = "select * from Usuarios where emailUsuario = '" + usuarios.EmailUsuario + "' and contraseña='" + usuarios.Contra + "'";
            AccesoDatos datos = new AccesoDatos();
            return datos.ObtenerTabla("Usuarios", consulta);
        }

        public bool editarMiCuenta(UsuariosEntidades usuarios, bool x)
        {
            usuariosDatos usuariosDatos = new usuariosDatos();
            return usuariosDatos.editarUsuarioMiCuenta(usuarios, x);
        }
    }
}
