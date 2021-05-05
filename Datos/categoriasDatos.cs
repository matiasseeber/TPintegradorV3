using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using System.Data.SqlClient;
using System.Data;

namespace Datos
{
    public class categoriasDatos
    {
        public bool agregarCategoria(string descripcion)
        {
            AccesoDatos datos = new AccesoDatos();
            SqlCommand comando = new SqlCommand();
            SqlParameter parameter = new SqlParameter();
            parameter = comando.Parameters.Add("@Descripcion", SqlDbType.VarChar);
            parameter.Value = descripcion;
            return Convert.ToBoolean(datos.ejecutarSP(comando, "AgregarCategoria"));
        }

        public bool eliminarCategoria(int id)
        {
            AccesoDatos accesoDatos = new AccesoDatos();
            SqlCommand command = new SqlCommand();
            SqlParameter parameter = new SqlParameter();
            parameter = command.Parameters.Add("@IdCat", SqlDbType.Int);
            parameter.Value = id;
            return Convert.ToBoolean(accesoDatos.ejecutarSP(command, "EliminarCategoria"));
        }
    }
}
