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
    public class articulosDatos
    {
        public bool agregarArticulo(articulosEntidades articulos)
        {
            AccesoDatos datos = new AccesoDatos();
            SqlCommand command = new SqlCommand();
            armarParametros(ref command, articulos, true);
            if (datos.ejecutarSP(command, "AgregarArticulo") == 1)
                return true;
            return false;
        }

        public DataSet devolverDataSet(string consulta)
        {
            AccesoDatos acceso = new AccesoDatos();
            return acceso.devolverDataSet(consulta, "Articulos");
        }

        public void armarParametros(ref SqlCommand comando, articulosEntidades articulos, bool url)
        {
            SqlParameter parameter = new SqlParameter();
            parameter = comando.Parameters.Add("@idCat", SqlDbType.Int);
            parameter.Value = articulos.IdCategoria;
            parameter = comando.Parameters.Add("@Descripcion", SqlDbType.VarChar);
            parameter.Value = articulos.DescripcionArticulo1;
            parameter = comando.Parameters.Add("@precio", SqlDbType.Decimal);
            parameter.Value = articulos.PrecioUnitarioArticulo;
            parameter = comando.Parameters.Add("@stock", SqlDbType.Int);
            parameter.Value = articulos.StockDisponibleArticulo;
            if (url)
            {
                parameter = comando.Parameters.Add("@urlImg", SqlDbType.VarChar);
                parameter.Value = articulos.Url_articulo_img;
            }
            else
            {
                parameter = comando.Parameters.Add("@id", SqlDbType.VarChar);
                parameter.Value = articulos.IdArticulo;
            }
        }

        public bool borrarArticulo(int id)
        {
            AccesoDatos accesoDatos = new AccesoDatos();
            SqlCommand command = new SqlCommand();
            command.Connection = accesoDatos.getConexion();
            armarParamtetrosEliminar(ref command, id);
            return Convert.ToBoolean(accesoDatos.ejecutarSP(command, "eliminarArticulo"));
        }

        public void armarParamtetrosEliminar(ref SqlCommand comando, int id)
        {
            SqlParameter parameter = new SqlParameter();
            parameter = comando.Parameters.Add("@id", SqlDbType.Int);
            parameter.Value = id;
        }

        public bool bajaLogica(int id)
        {
            AccesoDatos accesoDatos = new AccesoDatos();
            SqlCommand command = new SqlCommand();
            command.Connection = accesoDatos.getConexion();
            armarParamtetrosEliminar(ref command, id);
            return Convert.ToBoolean(accesoDatos.ejecutarSP(command, "bajaLogicaArticulo"));
        }

        public bool editarArticulo(articulosEntidades articulos)
        {
            AccesoDatos accesoDatos = new AccesoDatos();
            SqlCommand command = new SqlCommand();
            command.Connection = accesoDatos.getConexion();
            armarParametros(ref command, articulos, false);
            return Convert.ToBoolean(accesoDatos.ejecutarSP(command, "modificarArticulo"));
        }
    }
}
