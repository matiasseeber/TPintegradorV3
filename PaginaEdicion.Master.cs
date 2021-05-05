using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShopMarket
{
    public partial class Home : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["nombre"] != null)
            {
                lblNombreUsuario.Text = Session["nombre"].ToString();
                btnCerrarSesion.Visible = true;
            }
        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            lblNombreUsuario.Text = "";
            btnCerrarSesion.Visible = false;
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }
    }
}