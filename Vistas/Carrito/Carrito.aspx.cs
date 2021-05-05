using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocios;
using Entidades;
using System.Data.SqlClient;
using System.Data;

namespace ShopMarket
{
    public partial class Carrito : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Form.Attributes.Add("autocomplete", "off");
            this.UnobtrusiveValidationMode = System.Web.UI.UnobtrusiveValidationMode.None;
            if (!IsPostBack)
            {
                cargarGrvUsuarioXcarrito();
                lblGrv.Text = "";
            }
        }

        public void totalCompra()
        {
            decimal total = 0;

            foreach(GridViewRow row in grvCarrito.Rows)
            {
                total += Convert.ToDecimal(((Label)row.FindControl("lblPrecioFila")).Text);
            }

            Session["total"] = total;
        }

        public void cargarGrvUsuarioXcarrito()
        {
            if(Session["dni"] == null)
            {
                lblAviso.Visible = true;
                btnFinalizarCompra.Visible = false;
                return;
            }

            if (lblAviso.Visible)
            {
                lblAviso.Visible = false;
                btnFinalizarCompra.Visible = true;
            }

            carritoNegocios carritoNegocios = new carritoNegocios();
            DataTable tablaCarrito = carritoNegocios.cargarGrv(Session["dni"].ToString());
            grvCarrito.DataSource = tablaCarrito;
            grvCarrito.DataBind();
            totalCompra();
        }

        protected void grvCarrito_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvCarrito.PageIndex = e.NewPageIndex;
            cargarGrvUsuarioXcarrito();
            lblGrv.Text = "";
        }

        protected void grvCarrito_RowEditing(object sender, GridViewEditEventArgs e)
        {
            lblGrv.Text = "";
            grvCarrito.EditIndex = e.NewEditIndex;
            cargarGrvUsuarioXcarrito();

        }

        protected void grvCarrito_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grvCarrito.EditIndex = -1;
            cargarGrvUsuarioXcarrito();
            lblGrv.Text = "HA SIDO CANCELADA LA MODIFICACION DEL REGISTRO";
        }

        public void mostrarPreguntaDeConfirmacion()
        {
            lblGrv.Text = "";

            if (lblPreguntaConfirmacion.Visible == true)
            {
                lblPreguntaConfirmacion.Visible = false;
                btnAceptar.Visible = false;
                btnCancelar.Visible = false;
                return;
            }

            lblPreguntaConfirmacion.Visible = true;
            btnAceptar.Visible = true;
            btnCancelar.Visible = true;
        }

        public void borrarProductoCarrito()
        {
            carritoNegocios carrito = new carritoNegocios();
            lblGrv.Text = "";
            if (carrito.borrarArticulo(Session["dniIdBorrar"].ToString(), Session["idBorrar"].ToString()))
            {
                lblGrv.Visible = true;
                lblGrv.Text = "EL PRODUCTO SE ELIMINO CORRECTAMENTE DE SU CARRITO";
            }
            else
            {
                lblGrv.Visible = true;
                lblGrv.Text = "EL PRODUCTO NO SE ELIMINO CORRECTAMENTE DE SU CARRITO";
            }
            cargarGrvUsuarioXcarrito();
        }

        protected void grvCarrito_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string dni = Session["dni"].ToString();
            string idArticulo = ((Label)grvCarrito.Rows[e.RowIndex].FindControl("lblIdArt")).Text;
            Session["dniIdBorrar"] = dni;
            Session["idBorrar"] = idArticulo;
            mostrarPreguntaDeConfirmacion();
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            borrarProductoCarrito();
            mostrarPreguntaDeConfirmacion();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Session["idBorrar"] = null;
            mostrarPreguntaDeConfirmacion();
            lblGrv.Text = "HA SIDO CANCELADA LA ELIMINACION DEL REGISTRO";
        }

        public bool verificarStock(string idArticulo,ref int cant)
        {
            articulosNegocios articulos = new articulosNegocios();
            string consulta = "select stockDisponibleArticulo from Articulos where idArticulo ="+idArticulo;
            DataTable dt = articulos.cargarGrvArticulo(consulta);
            if (cant > Convert.ToInt32(dt.Rows[0][0]))
            {
                cant = Convert.ToInt32(dt.Rows[0][0]);
                return false;
            }
            return true;
        }

        protected void grvCarrito_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string idArticulo = ((Label)grvCarrito.Rows[e.RowIndex].FindControl("lblIdArtET")).Text;
            int cant = Convert.ToInt32(((TextBox)grvCarrito.Rows[e.RowIndex].FindControl("txtCantidad")).Text);

            if (!verificarStock(idArticulo,ref cant))
            {
                lblGrv.Visible = true;
                lblGrv.Text = "NO PUEDE INGRESAR UNA CANTIDAD MAYOR A LA DEL STOCK ACTUAL ("+cant+")";
                return;
            }

            carritoNegocios carrito = new carritoNegocios();
            carritoEntidades carritoEntidades = new carritoEntidades();
            carritoEntidades.Dni = Session["dni"].ToString();
            carritoEntidades.Cantidad = cant;
            carritoEntidades.Id_articulo = Convert.ToInt32(idArticulo);

            if (carrito.modificarCantidad(carritoEntidades))
            {
                lblGrv.Text = "EL REGISTRO SE MODIFICO EXITOSAMENTE";
                grvCarrito.EditIndex = -1;
                cargarGrvUsuarioXcarrito();
                return;
            }
                grvCarrito.EditIndex = -1;
                cargarGrvUsuarioXcarrito();
                lblGrv.Text = "NO SE PUDO MODIFICAR EL REGISTRO EXITOSAMENTE";
        }

        public void esconderBotonesCompra()
        {
            if (lblPreguntaFinalizar.Visible)
            {
                lblPreguntaFinalizar.Visible = false;
                btnConfirmar.Visible = false;
                btnCancelarCompra.Visible = false;
                return;
            }
            lblPreguntaFinalizar.Visible = true;
            btnConfirmar.Visible = true;
            btnCancelarCompra.Visible = true;
        }

        protected void btnFinalizarCompra_Click(object sender, EventArgs e)
        {
            esconderBotonesCompra();
        }

        protected void btnCancelarCompra_Click(object sender, EventArgs e)
        {
            esconderBotonesCompra();
            lblGrv.Text = "COMPRA CANCELADA";
        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            esconderBotonesCompra();
            facturasNegocios facturas = new facturasNegocios();
            facturasEntidades facturasEntidades = new facturasEntidades();
            facturasEntidades.Dni_Usuario = Session["dni"].ToString();
            facturasEntidades.Monto_final = Convert.ToDecimal(Session["total"]);
            
            if (!facturas.generarFactura(facturasEntidades))
            {
                lblGrv.Text = "LA COMPRA NO PUDO SER CONFIRMADA";
                return;
            }

            lblGrv.Text = "COMPRA CONFIRMADA";

            DataTable dt = new DataTable();

            string consulta = "select id_Factura from facturas where dni_Usuario ="+Session["dni"].ToString();

            dt = facturas.cargarGrv(consulta);

            detalleFacturaEntidades detalleFacturaEntidades = new detalleFacturaEntidades();

            foreach (DataRow row in dt.Rows)
            {
                detalleFacturaEntidades.Id_factura = Convert.ToInt32(row[0]);
            }

            carritoNegocios carrito = new carritoNegocios();

            foreach (GridViewRow row in grvCarrito.Rows)
            {
                detalleFacturaEntidades.Id_articulo = Convert.ToInt32(((Label)row.FindControl("lblIdArt")).Text);
                detalleFacturaEntidades.DescripcionProducto = ((Label)row.FindControl("lblNomProducto")).Text;
                detalleFacturaEntidades.Precio_unitario = Convert.ToDecimal(((Label)row.FindControl("lblPrecio")).Text);
                detalleFacturaEntidades.Cantidad = Convert.ToInt32(((Label)row.FindControl("lblCantidad")).Text);
                facturas.generarDetalleFactura(detalleFacturaEntidades);
                carrito.borrarArticulo(Session["dni"].ToString(), detalleFacturaEntidades.Id_articulo.ToString());
            }
            cargarGrvUsuarioXcarrito();
        }

        protected void grvCarrito_DataBound(object sender, EventArgs e)
        {
                int rowCount = grvCarrito.Rows.Count;

                if (rowCount == 0)
                {
                btnFinalizarCompra.Visible = false;
                return;
                }

            btnFinalizarCompra.Visible = true;

        }
    }
}