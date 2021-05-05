using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using Datos;
using System.Data;

namespace Negocios
{
    public class articulosNegocios
    {
        public bool agregarArticulo(articulosEntidades articulos)
        {
            articulosDatos Datos = new articulosDatos();
            return Datos.agregarArticulo(articulos);
        }

        public DataTable cargarGrvArticulo(string consulta)
        {
            AccesoDatos acceso = new AccesoDatos();
            return acceso.ObtenerTabla("Articulos", consulta);
        }

        public DataSet cargarListView(string consulta)
        {
            articulosDatos articulos = new articulosDatos();
            return articulos.devolverDataSet(consulta);
        }

        public bool borrarArticulo(int id)
        {
            articulosDatos articulosDatos = new articulosDatos();
            return articulosDatos.borrarArticulo(id);
        }

        public bool bajaLogica(int id)
        {
            articulosDatos articulos = new articulosDatos();
            return Convert.ToBoolean(articulos.bajaLogica(id));
        }

        public bool verificarUsoArticulo(int id)
        {
            AccesoDatos acceso = new AccesoDatos();
            return acceso.existe("select * from detalleFacturas where id_articulo = '"+id.ToString()+"'");
        }

        public bool editarArticulo(articulosEntidades articuloEditar)
        {
            articulosDatos articulos = new articulosDatos();
            return articulos.editarArticulo(articuloEditar);
        }

    }
}
