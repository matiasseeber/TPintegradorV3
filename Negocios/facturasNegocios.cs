using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using System.Data.SqlClient;
using System.Data;
using Datos;

namespace Negocios
{
    public class facturasNegocios
    {
        public DataTable cargarGrv(string consulta)
        {
            AccesoDatos acceso = new AccesoDatos();
            return acceso.ObtenerTabla("facturas", consulta);
        }

        public bool generarFactura(facturasEntidades facturas)
        {
            facturasDatos facturasDatos = new facturasDatos();
            return facturasDatos.generarFactura(facturas);
        }

        public bool generarDetalleFactura(detalleFacturaEntidades detalleFactura)
        {
            facturasDatos facturasDatos = new facturasDatos();
            return facturasDatos.generarDetalleFactura(detalleFactura);
        }
    }
}
