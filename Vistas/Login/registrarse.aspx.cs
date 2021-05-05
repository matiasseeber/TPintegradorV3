using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades;
using Negocios;

namespace TPintegrador
{
    public partial class registrarse : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Form.Attributes.Add("autocomplete", "off");
            this.UnobtrusiveValidationMode = System.Web.UI.UnobtrusiveValidationMode.None;
        }

        public UsuariosEntidades cargarUsuario()
        {
            UsuariosEntidades usuario = new UsuariosEntidades();
            usuario.NombreUsuario = txtNombreReg.Text;
            usuario.ApellidoUsuario = txtApellidoReg.Text;
            usuario.Admin = false;
            usuario.Contra = txtContraseñaReg.Text;
            usuario.DniUsuario = Convert.ToInt32(txtDniReg.Text);
            usuario.DireccionUsuario = txtDireccionReg.Text;
            usuario.EmailUsuario = txtEmailReg.Text;
            usuario.NumeroTarjetaCredito = Convert.ToInt32(txtNumTarjetaReg.Text);
            usuario.CodigoSeguridad = Convert.ToInt32(txtNumSegTarjReg.Text);
            return usuario;
        }

        protected void btnRegistrarse_Click(object sender, EventArgs e)
        {
            lblMsjres.Text = "";
            UsuariosEntidades usuario = new UsuariosEntidades();
            usuario = cargarUsuario();
            UsuariosNegocios usuariosNegocios = new UsuariosNegocios();
            bool existe = true;
            if (usuariosNegocios.verificarDni(usuario))
            {
                lblMsjres.Text = "Ese numero de dni ya esta asociado a una cuenta." + Environment.NewLine;
                existe = false;
            }

            if (usuariosNegocios.verificarEmail(usuario))
            {
                lblMsjres.Text += "Ese email ya esta asociado a una cuenta.";
                existe = false;
            }

            if (!existe)
                return;

            if (usuariosNegocios.agregarUsuario(usuario))
            {
                lblMsjres2.ForeColor = Color.Green;
                lblMsjres2.Text = "EL USUARIO SE PUDO AÑADIR EXITOSAMENTE. \n";
                lblMsjres2.Text += "SE LO REDIRECCIONARA A LA PAGINA DEL LOGIN EN 5 SEG...";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "myscript", "setTimeout(function(){location.href='Login.aspx';},5000);", true);
            }
        }
    }
}