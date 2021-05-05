using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Entidades;
using Negocios;

namespace ShopMarket.Vistas.Mi_cuenta
{
    public partial class Mi_cuenta : System.Web.UI.Page
    {

        public void cargarLabels()
        {
            lblNombreUsuario.Text = Session["nombre"].ToString();
            lblApellidoUsuario.Text = Session["apellido"].ToString();
            lblDniUsuario.Text = Session["dni"].ToString();
            txtDireccion.Text = Session["direccion"].ToString();
            lblEmailUsuario.Text = Session["email"].ToString();
            txtNumTarjetaCred.Text = Session["numTarjeta"].ToString();
            txtCodSeguridad.Text = Session["codSeguridad"].ToString();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Form.Attributes.Add("autocomplete", "off");
            this.UnobtrusiveValidationMode = System.Web.UI.UnobtrusiveValidationMode.None;
            if (Session["nombre"] != null)
            {
                lblAvisoUsuario.Visible = false;
                btnModificar.Enabled = true;
                btnModificarDireccion.Enabled = true;
                if (!IsPostBack)
                {
                    cargarGrvFacturasUsuario();
                    cargarLabels();
                }
            }
            else
            {
                btnModificar.Enabled = false;
                btnModificarDireccion.Enabled = false;
                btnAdministrar.Enabled = false;
                btnAdministrar.Visible = false;
                lblAvisoUsuario.Visible = true;
            }

           if (Convert.ToInt32(Session["admin"]) == 1){
                btnAdministrar.Visible = true;
           }
        }

        public bool validacion()
        {
            if (txtDireccion.Text == "")
                return false;
            return true;
        }

        public bool validacion2()
        {
            if (txtNumTarjetaCred.Text == "" || txtCodSeguridad.Text == "")
                return false;
            return true;
        }

        protected void btnModificarDireccion_Click(object sender, EventArgs e)
        {
            if (!validacion())
            {
                lblValidacion.Text = "NO PUDE MODIFICAR LA INFORMACION SI LA CAJA DE TEXTO ESTA VACIA";
                return;
            }

            UsuariosEntidades usuarios = new UsuariosEntidades();
            usuarios.DireccionUsuario = txtDireccion.Text;
            usuarios.DniUsuario = Convert.ToInt32(lblDniUsuario.Text);
            UsuariosNegocios usuariosNegocios = new UsuariosNegocios();
            if (usuariosNegocios.editarMiCuenta(usuarios, true))
            {
                Session["direccion"] = txtDireccion.Text;
                lblValidacion.Text = "EL REGISTRO SE MODIFICO EXITOSAMENTE";
            }
            else
                lblValidacion.Text = "NO SE PUDO MODIFICAR EL REGISTRO";
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            if (!validacion2())
            {
                lblValidacion.Text = "NO PUDE MODIFICAR LA INFORMACION SI ALGUNA DE LAS CAJAS DE TEXTO ESTA VACIA";
                return;
            }

            UsuariosEntidades usuarios = new UsuariosEntidades();
            usuarios.NumeroTarjetaCredito = Convert.ToInt32(txtNumTarjetaCred.Text);
            usuarios.DniUsuario = Convert.ToInt32(lblDniUsuario.Text);
            usuarios.CodigoSeguridad = Convert.ToInt32(txtCodSeguridad.Text);
            UsuariosNegocios usuariosNegocios = new UsuariosNegocios();
            if (usuariosNegocios.editarMiCuenta(usuarios, false))
            {
                Session["numTarjeta"] = usuarios.NumeroTarjetaCredito.ToString();
                Session["codSeguridad"] = usuarios.CodigoSeguridad.ToString();
                lblValidacion.Text = "EL REGISTRO SE MODIFICO EXITOSAMENTE";
            }
            else
                lblValidacion.Text = "NO SE PUDO MODIFICAR EL REGISTRO";
        }

        public void cargarGrvFacturasUsuario()
        {
            facturasNegocios facturasNegocios = new facturasNegocios();
            string consulta = "select * from facturas where dni_Usuario =" + Session["dni"].ToString();
            grvFactuas.DataSource = facturasNegocios.cargarGrv(consulta);
            grvFactuas.DataBind();
        }

        protected void grvFactuas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvFactuas.PageIndex = e.NewPageIndex;
            cargarGrvFacturasUsuario();
        }

        protected void grvFactuas_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            string idFacturas = ((Label)grvFactuas.Rows[e.NewSelectedIndex].FindControl("lblNumeroFactura")).Text;
            facturasNegocios facturasNegocios = new facturasNegocios();
            string consulta = "select descripcionProducto as Producto, precio_unitario as Precio, cantidad as Cantidad, (precio_unitario * cantidad) as Total from detalleFacturas where id_factura ="+idFacturas;
            grvDetalleFacturas.DataSource = facturasNegocios.cargarGrv(consulta);
            grvDetalleFacturas.DataBind();
        }
    }
    
}