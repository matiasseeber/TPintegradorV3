using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using Datos;
using System.Data.SqlClient;
using System.Data;

namespace Negocios
{
    public class carritoNegocios
    {
        public carritoNegocios()
        {

        }

        public bool agregarArticuloCarrito(carritoEntidades carrito)
        {
            carritoDatos carritoDatos = new carritoDatos();
            return carritoDatos.agregarArticuloCarrito(carrito);
        }

        public DataTable cargarGrv(string dni)
        {
            AccesoDatos accesoDatos = new AccesoDatos();
            string consulta = "select usuariosXcarrito.dni_Usuario,usuariosXcarrito.id_articulo,usuariosXcarrito.descripcionArticulo,usuariosXcarrito.cantidad ,precioUnitarioArticulo,(Articulos.precioUnitarioArticulo * cantidad) as Total from usuariosXcarrito inner join Articulos on usuariosXcarrito.id_articulo = Articulos.idArticulo where usuariosXcarrito.dni_Usuario ="+dni;
            return accesoDatos.ObtenerTabla("usuariosXcarrito",consulta);
        }

        public bool borrarArticulo(string dni, string idArt)
        {
            carritoDatos carrito = new carritoDatos();
            return carrito.borrarArticulo(dni, idArt);
        }

        public bool verificarSeleccionArticulo(carritoEntidades carrito)
        {
            AccesoDatos acceso = new AccesoDatos();
            string consulta = "select * from usuariosXcarrito where id_articulo='"+carrito.Id_articulo+ "' and dni_Usuario='" + carrito.Dni+"'";
            return acceso.existe(consulta);
        }

        public bool modificarCantidad(carritoEntidades carrito)
        {
            carritoDatos carritoDatos = new carritoDatos();
            return carritoDatos.modificarArticulo(carrito);
        }

    }
}
