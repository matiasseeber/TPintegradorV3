using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class UsuariosEntidades
    {
        private string nombreUsuario;
        private string apellidoUsuario;
        private int dniUsuario;
        private string emailUsuario;
        private string direccionUsuario;
        private Boolean admin = false;
        private int numeroTarjetaCredito;
        private int codigoSeguridad;
        private string contra;

        public UsuariosEntidades()
        {

        }

        public UsuariosEntidades(string nombreUsuario, string apellidoUsuario, int dniUsuario, string emailUsuario, string direccionUsuario, bool admin, int numeroTarjetaCredito, int codigoSeguridad)
        {
            this.nombreUsuario = nombreUsuario;
            this.apellidoUsuario = apellidoUsuario;
            this.dniUsuario = dniUsuario;
            this.emailUsuario = emailUsuario;
            this.direccionUsuario = direccionUsuario;
            this.admin = admin;
            this.numeroTarjetaCredito = numeroTarjetaCredito;
            this.codigoSeguridad = codigoSeguridad;
        }

        public string NombreUsuario { get => nombreUsuario; set => nombreUsuario = value; }
        public string ApellidoUsuario { get => apellidoUsuario; set => apellidoUsuario = value; }
        public int DniUsuario { get => dniUsuario; set => dniUsuario = value; }
        public string EmailUsuario { get => emailUsuario; set => emailUsuario = value; }
        public string DireccionUsuario { get => direccionUsuario; set => direccionUsuario = value; }
        public bool Admin { get => admin; set => admin = value; }
        public int NumeroTarjetaCredito { get => numeroTarjetaCredito; set => numeroTarjetaCredito = value; }
        public int CodigoSeguridad { get => codigoSeguridad; set => codigoSeguridad = value; }
        public string Contra { get => contra; set => contra = value; }
    }
}
