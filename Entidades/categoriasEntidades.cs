using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class categoriasEntidades
    {
        private string idCategoria;
        private string descripcionCategoria;

        public categoriasEntidades()
        {

        }

        public string IdCategoria { get => idCategoria; set => idCategoria = value; }
        public string DescripcionCategoria { get => descripcionCategoria; set => descripcionCategoria = value; }
    }
}
