using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using Datos;
using System.Data.SqlClient;

namespace Negocios
{
    public class categoriasNegocios
    {
        public DataTable cargarCategorias()
        {
            AccesoDatos accesoDatos = new AccesoDatos();
            string consulta = "select * from Categorias";
            return accesoDatos.ObtenerTabla("Categorias", consulta);
        }

        public DataTable cargarCategorias(string consulta)
        {
            AccesoDatos accesoDatos = new AccesoDatos();
            return accesoDatos.ObtenerTabla("Categorias", consulta);
        }

        public bool borrarCategoria(int id)
        {
            categoriasDatos categoriasDatos = new categoriasDatos();
            return categoriasDatos.eliminarCategoria(id);
        }

        public bool editarCategoria(string id, string nuevaDescripcion)
        {
            AccesoDatos accesoDatos = new AccesoDatos();
            SqlCommand command = new SqlCommand();
            SqlParameter parameter = new SqlParameter();
            parameter = command.Parameters.Add("@IdCat", SqlDbType.Int);
            parameter.Value = id;
            parameter = command.Parameters.Add("@DescripcionCat", SqlDbType.VarChar);
            parameter.Value = nuevaDescripcion;
            return Convert.ToBoolean(accesoDatos.ejecutarSP(command, "ModificarCateogoria"));
        }

        public bool agregarCategoria(string descripcion)
        {
            categoriasDatos categorias = new categoriasDatos();
            return categorias.agregarCategoria(descripcion);
        }

        public SqlDataReader cargarDDL()
        {
            AccesoDatos accesoDatos = new AccesoDatos();
            SqlCommand command = new SqlCommand("select idCategoria,descripcionCategoria from Categorias",accesoDatos.getConexion());
            SqlDataReader dr = command.ExecuteReader();
            return dr;
        }

        public bool verificarUsoCategoria(int id)
        {
            AccesoDatos acceso = new AccesoDatos();
            string consulta = "select * from Articulos where idCategoria = '" + id.ToString()+"'";
            return acceso.existe(consulta);
        }

    }
}
