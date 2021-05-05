using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using System.Data.SqlClient;
using System.Data;

namespace Datos
{
    public class AccesoDatos
    {
        public string ruta = "Data Source=DESKTOP-VNG912H\\SQLEXPRESS;Initial Catalog=TpIntegradorProgramacion;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"; 

        public SqlConnection getConexion()
        {
            SqlConnection conexion = new SqlConnection(ruta);
            try
            {
                conexion.Open();
                return conexion;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public SqlDataAdapter GetSqlDataAdapter(String consultaSql, SqlConnection conexion)
        { 
            SqlDataAdapter adapter = new SqlDataAdapter(consultaSql,conexion);
            try
            {
                return adapter;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable ObtenerTabla(String NombreTabla, String Sql)
        {
            DataSet ds = new DataSet();
            SqlConnection Conexion = getConexion();
            SqlDataAdapter adp = GetSqlDataAdapter(Sql, Conexion);
            adp.Fill(ds, NombreTabla);
            Conexion.Close();
            return ds.Tables[NombreTabla];
        }

        public DataSet devolverDataSet(string consulta, string nombre)
        {
            DataSet ds = new DataSet();
            SqlConnection conexion = getConexion();
            SqlDataAdapter adapter = new SqlDataAdapter(consulta, conexion);
            adapter.Fill(ds, nombre);
            conexion.Close();
            return ds;
        }

        public bool existe(String consulta)
        {
            bool estado = false;
            SqlConnection Conexion = getConexion();
            SqlCommand cmd = new SqlCommand(consulta, Conexion);
            SqlDataReader datos = cmd.ExecuteReader();
            if (datos.Read())
            {
                estado = true;
            }
            return estado;
        }

        public int ejecutarSP(SqlCommand command, string nombreProcedimiento)
        {
            SqlConnection connection = getConexion();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = nombreProcedimiento;
            int res = command.ExecuteNonQuery();
            connection.Close();
            return res;
        }
    }
}
