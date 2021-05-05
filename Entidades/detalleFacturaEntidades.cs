using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class detalleFacturaEntidades
    {
        private int id_factura;
        private int numeroDeOrden;
        private int id_articulo;
        private decimal precio_unitario;
        private string descripcionProducto;
        private int cantidad;

        public detalleFacturaEntidades()
        {

        }

        public int Id_factura { get => id_factura; set => id_factura = value; }
        public int NumeroDeOrden { get => numeroDeOrden; set => numeroDeOrden = value; }
        public int Id_articulo { get => id_articulo; set => id_articulo = value; }
        public decimal Precio_unitario { get => precio_unitario; set => precio_unitario = value; }
        public string DescripcionProducto { get => descripcionProducto; set => descripcionProducto = value; }
        public int Cantidad { get => cantidad; set => cantidad = value; }
    }
}
