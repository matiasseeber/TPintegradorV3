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
    public class facturasDatos
    {
        public facturasDatos()
        {

        }

        public bool generarFactura(facturasEntidades facturas)
        {
            AccesoDatos acceso = new AccesoDatos();
            SqlCommand command = new SqlCommand();
            SqlParameter parameter = new SqlParameter();
            parameter = command.Parameters.Add("@dniUsuario", SqlDbType.VarChar);
            parameter.Value = facturas.Dni_Usuario;
            parameter = command.Parameters.Add("@montoFinal", SqlDbType.Decimal);
            parameter.Value = facturas.Monto_final;
            return Convert.ToBoolean(acceso.ejecutarSP(command, "agregarFactura"));
        }

        public bool generarDetalleFactura(detalleFacturaEntidades detalleFactura)
        {
            AccesoDatos acceso = new AccesoDatos();
            SqlCommand command = new SqlCommand();
            armarParametrosGenrarDetalleFactura(ref command, detalleFactura);
            return Convert.ToBoolean(acceso.ejecutarSP(command,"AgregarDetalleFacturas"));
        }

        public void armarParametrosGenrarDetalleFactura(ref SqlCommand command, detalleFacturaEntidades detalleFactura)
        {
            SqlParameter parameter = new SqlParameter();
            parameter = command.Parameters.Add("@id_Factura", SqlDbType.Int);
            parameter.Value = detalleFactura.Id_factura;
            parameter = command.Parameters.Add("@id_Articulo", SqlDbType.Int);
            parameter.Value = detalleFactura.Id_articulo;
            parameter = command.Parameters.Add("@PrecioUnitario", SqlDbType.Decimal);
            parameter.Value = detalleFactura.Precio_unitario;
            parameter = command.Parameters.Add("@descripcionProducto", SqlDbType.VarChar);
            parameter.Value = detalleFactura.DescripcionProducto;
            parameter = command.Parameters.Add("@cantidad", SqlDbType.Int);
            parameter.Value = detalleFactura.Cantidad;
        }
    }
}
