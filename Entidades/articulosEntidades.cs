using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class articulosEntidades
    {
        private int idArticulo;
        private int idCategoria;
        private string DescripcionArticulo;
        private float precioUnitarioArticulo;
        private int stockDisponibleArticulo;
        private string url_articulo_img;

        public articulosEntidades()
        {

        }

        public string getDescripcion()
        {
            return this.DescripcionArticulo;
        }

        public int IdArticulo { get => idArticulo; set => idArticulo = value; }
        public int IdCategoria { get => idCategoria; set => idCategoria = value; }
        public float PrecioUnitarioArticulo { get => precioUnitarioArticulo; set => precioUnitarioArticulo = value; }
        public int StockDisponibleArticulo { get => stockDisponibleArticulo; set => stockDisponibleArticulo = value; }
        public string Url_articulo_img { get => url_articulo_img; set => url_articulo_img = value; }
        public string DescripcionArticulo1 { get => DescripcionArticulo; set => DescripcionArticulo = value; }
    }
}
