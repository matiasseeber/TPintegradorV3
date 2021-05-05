using Negocios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades;
using System.Drawing;
using System.Data.SqlClient;
using System.Data;
using ShopMarket;

namespace TPintegrador
{
    public partial class Inicio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.UnobtrusiveValidationMode = System.Web.UI.UnobtrusiveValidationMode.None;
            if (!IsPostBack)
            {
                this.Form.Attributes.Add("autocomplete", "off");
            }

            if (Session["nombre"] == null)
                btnCerrarSession.Visible = false;
            else
                btnCerrarSession.Visible = true;
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (Session["dni"] != null)
            {
                lblMsjAclaratorio.ForeColor = Color.Red;
                lblMsjAclaratorio.Text = "DEBE CERRAR SESION ANTES DE PODER INIICAR SESION CON OTRA CUENTA";
                return;
            }

            UsuariosNegocios usuario = new UsuariosNegocios();
            UsuariosEntidades usuariosEntidades = new UsuariosEntidades();
            usuariosEntidades.EmailUsuario = txtNomUsuario.Text;
            usuariosEntidades.Contra = txtContra.Text;
            
            if (!usuario.verificarEmail(usuariosEntidades))
            {
                lblMsjAclaratorio.ForeColor = Color.Red;
                lblMsjAclaratorio.Text = "EL EMAIL O LA CONTRASEÑA SON INCORRECTOS";
                return;
            }
            
            if (!usuario.logearse(usuariosEntidades))
            {
                lblMsjAclaratorio.ForeColor = Color.Red;
                lblMsjAclaratorio.Text = "EL EMAIL O LA CONTRASEÑA SON INCORRECTOS";
                return;
            }
            
            DataTable dt = (DataTable)usuario.obtenerUsuario(usuariosEntidades);
            Session["nombre"] = /*nombre*/dt.Rows[0][1].ToString() +" "+/*apellido*/dt.Rows[0][2].ToString();
            Session["apellido"] = dt.Rows[0][2].ToString();
            Session["dni"] = dt.Rows[0][0].ToString();
            Session["email"] = dt.Rows[0][3].ToString();
            Session["direccion"] = dt.Rows[0][4].ToString();
            Session["admin"] = Convert.ToInt32(dt.Rows[0][7]);
            Session["master"] = Convert.ToBoolean(dt.Rows[0][8]);
            Session["numTarjeta"] = dt.Rows[0][5].ToString();
            Session["codSeguridad"] = dt.Rows[0][6].ToString();
            Server.Transfer("Carga.aspx");
        }

        protected void btnCerrarSession_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            lblMsjAclaratorio.ForeColor = Color.Green;
            lblMsjAclaratorio.Text = "SESION CERRADA";
            btnCerrarSession.Visible = false;
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }
    }
}