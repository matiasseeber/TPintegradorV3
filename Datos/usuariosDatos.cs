using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace Datos
{
    public class usuariosDatos
    {
        public bool agregarUsuario(UsuariosEntidades usuario)
        {
            AccesoDatos acceso = new AccesoDatos();
            SqlCommand command = new SqlCommand();
            armarParametrosAgregar(ref command,usuario);
            if (acceso.ejecutarSP(command, "agregarUsuario") == 1)
                return true;
            return false;
        }

        public bool eliminarUsuario(string id)
        {
            AccesoDatos datos = new AccesoDatos();
            SqlCommand comando = new SqlCommand();
            SqlParameter parameter = new SqlParameter();
            parameter = comando.Parameters.Add("@dni", SqlDbType.VarChar);
            parameter.Value = id;
            return Convert.ToBoolean(datos.ejecutarSP(comando, "eliminarUsuario"));
        }

        public bool editarUsuario(string id, Boolean admin)
        {
            AccesoDatos datos = new AccesoDatos();
            SqlCommand comando = new SqlCommand();
            SqlParameter parameter = new SqlParameter();
            parameter = comando.Parameters.Add("@dni", SqlDbType.VarChar);
            parameter.Value = id;
            parameter = comando.Parameters.Add("@admin", SqlDbType.Bit);
            parameter.Value = admin;
            return Convert.ToBoolean(datos.ejecutarSP(comando, "modificarEstadoAdminUsuario"));
        }

        public void armarParametrosAgregar(ref SqlCommand command, UsuariosEntidades usuarios)
        {
            SqlParameter parametro = new SqlParameter();
            parametro = command.Parameters.Add("@dni",SqlDbType.VarChar);
            parametro.Value = usuarios.DniUsuario;
            parametro = command.Parameters.Add("@nombre",SqlDbType.VarChar);
            parametro.Value = usuarios.NombreUsuario;
            parametro = command.Parameters.Add("@apellido", SqlDbType.VarChar);
            parametro.Value = usuarios.ApellidoUsuario;
            parametro = command.Parameters.Add("@mail", SqlDbType.VarChar);
            parametro.Value = usuarios.EmailUsuario;
            parametro = command.Parameters.Add("@direccion", SqlDbType.VarChar);
            parametro.Value = usuarios.DireccionUsuario;
            parametro = command.Parameters.Add("@numTarjetaCredito", SqlDbType.VarChar);
            parametro.Value = usuarios.NumeroTarjetaCredito;
            parametro = command.Parameters.Add("@codigoSeguridad", SqlDbType.VarChar);
            parametro.Value = usuarios.CodigoSeguridad;
            parametro = command.Parameters.Add("@contra", SqlDbType.VarChar);
            parametro.Value = usuarios.Contra;
        }

        public bool editarUsuarioMiCuenta(UsuariosEntidades usuarios,bool x)
        {
            AccesoDatos datos = new AccesoDatos();
            SqlCommand comando = new SqlCommand();
            SqlParameter parameter = new SqlParameter();
            parameter = comando.Parameters.Add("@dni", SqlDbType.VarChar);
            parameter.Value = usuarios.DniUsuario;
            if (x)
            {
                parameter = comando.Parameters.Add("@direccion", SqlDbType.VarChar);
                parameter.Value = usuarios.DireccionUsuario;
                return Convert.ToBoolean(datos.ejecutarSP(comando, "modificarDireccionUsuario"));
            }

                parameter = comando.Parameters.Add("@numTarjetaCredito", SqlDbType.VarChar);
                parameter.Value = usuarios.NumeroTarjetaCredito;
                parameter = comando.Parameters.Add("@Cod", SqlDbType.VarChar);
                parameter.Value = usuarios.CodigoSeguridad;
                return Convert.ToBoolean(datos.ejecutarSP(comando, "modificarTarjetaUsuario"));
        }
    }
}
