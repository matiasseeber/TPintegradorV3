using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using System.Data;
using System.Data.SqlClient;

namespace Datos
{
    public class carritoDatos
    {
        public carritoDatos()
        {

        }

        public bool agregarArticuloCarrito(carritoEntidades carrito)
        {
            AccesoDatos acceso = new AccesoDatos();
            SqlCommand command = new SqlCommand();
            armarParametrosAgregar(ref command, carrito);
            if (acceso.ejecutarSP(command, "agregarArticuloCarrito") == 1)
                return true;
            return false;
        }

        public bool borrarArticulo(string dni, string idArt)
        {
            AccesoDatos acceso = new AccesoDatos();
            SqlCommand command = new SqlCommand();
            SqlParameter sqlParameter = new SqlParameter();
            sqlParameter = command.Parameters.Add("@dni", SqlDbType.VarChar);
            sqlParameter.Value = dni;
            sqlParameter = command.Parameters.Add("@idArticulo", SqlDbType.Int);
            sqlParameter.Value = idArt;
            if (acceso.ejecutarSP(command, "eliminarArticuloXcarrito") == 1)
                return true;
            return false;
        }

        public bool modificarArticulo(carritoEntidades carrito)
        {
            AccesoDatos acceso = new AccesoDatos();
            SqlCommand command = new SqlCommand();
            SqlParameter sqlParameter = new SqlParameter();
            sqlParameter = command.Parameters.Add("@dni", SqlDbType.VarChar);
            sqlParameter.Value = carrito.Dni;
            sqlParameter = command.Parameters.Add("@idArticulo", SqlDbType.Int);
            sqlParameter.Value = carrito.Id_articulo;
            sqlParameter = command.Parameters.Add("@cantidad", SqlDbType.Int);
            sqlParameter.Value = carrito.Cantidad;
            if (acceso.ejecutarSP(command, "modificarCantidadCarrito") == 1)
                return true;
            return false;
        }

        public void armarParametrosAgregar(ref SqlCommand command, carritoEntidades carrito)
        {
            SqlParameter sqlParameter = new SqlParameter();
            sqlParameter = command.Parameters.Add("@dni", SqlDbType.VarChar);
            sqlParameter.Value = carrito.Dni;
            sqlParameter = command.Parameters.Add("@idArticulo", SqlDbType.Int);
            sqlParameter.Value = carrito.Id_articulo;
            sqlParameter = command.Parameters.Add("@descripcion", SqlDbType.VarChar);
            sqlParameter.Value = carrito.DescripcionArticulo;
        }
    }
}
