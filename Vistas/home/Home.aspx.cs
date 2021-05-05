using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocios;
using Entidades;
using System.Data;
using System.Data.SqlClient;

namespace ShopMarket
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Form.Attributes.Add("autocomplete", "off");
            this.UnobtrusiveValidationMode = System.Web.UI.UnobtrusiveValidationMode.None;
            if (!IsPostBack)
            {
                cargarListView();
                cargarCategoriasDDL();
            }
        }

        public void cargarCategoriasDDL()
        {
            categoriasNegocios categoriasNegocios = new categoriasNegocios();
            SqlDataReader sqlData = categoriasNegocios.cargarDDL();
            ddlCategorias.DataSource = sqlData;
            ddlCategorias.DataTextField = "descripcionCategoria";
            ddlCategorias.DataValueField = "idCategoria";
            ddlCategorias.DataBind();
        }

        public void cargarListView()
        {

            string consulta = "";

            bool nombre = Convert.ToBoolean(Session["filtrarArticuloNombre"]);
            bool categorias = Convert.ToBoolean(Session["categorias"]);
            bool precio = Convert.ToBoolean(Session["precioArticulo"]);

            if (nombre)
            {
                consulta = "select * from Articulos where estado = 1 and stockDisponibleArticulo > 0 and DescripcionArticulo like '%" + txtNombreArticulo.Text + "%' ";
            }

            if (categorias)
            {
                if(consulta == "")
                    consulta = "select * from articulos where estado = 1 and stockDisponibleArticulo > 0 and idCategoria = '" + ddlCategorias.SelectedValue.ToString() + "' ";
                else
                    consulta += "and idCategoria = '" + ddlCategorias.SelectedValue.ToString() + "' ";
            }

            if (precio)
            {
                if (consulta == "")
                   consulta = "select * from articulos where estado = 1 and stockDisponibleArticulo > 0 and precioUnitarioArticulo between '" + txtPrecioMin.Text + "' and '"+ txtPrecioMaximo.Text+"'";
                else
                   consulta += "and precioUnitarioArticulo between '" + txtPrecioMin.Text + "' and '"+ txtPrecioMaximo.Text+"'";
            }

            if (consulta != "")
            {
                cargarListViewGenerico(consulta);
                return;
            }

            cargarListViewGenerico("select * from articulos where estado = 1 and stockDisponibleArticulo > 0");
        }

        public void cargarListViewGenerico(string consulta)
        {
            articulosNegocios articulos = new articulosNegocios();
            listViewArticulos.DataSource = articulos.cargarListView(consulta);
            listViewArticulos.DataBind();
        }

        public bool verificacionTxt()
        {

            lblPreguntaConfirmacion.Text = "";

            bool estado = true;     

            if(txtPrecioMin.Text == "" || txtPrecioMaximo.Text == "")
            {
                estado = false;
                Session["precioArticulo"] = false;
                lblPreguntaConfirmacion.Visible = true;
                lblPreguntaConfirmacion.Text = "AMBAS CAJAS DE TEXTO DE PRECIO DEBEN CONTENER NUMEROS";
                return estado;
            }

            if (Convert.ToInt32(txtPrecioMin.Text) > Convert.ToInt32(txtPrecioMaximo.Text))
            {
                estado = false;
                lblPreguntaConfirmacion.Visible = true;
                lblPreguntaConfirmacion.Text = "EL PRECIO MINIMO NO PUEDE SER MAYOR QUE EL PRECIO MAXIMO \n";
            }

            return estado;
        }

        protected void btnBuscarNombre_Click(object sender, EventArgs e)
        {
            Session["filtrarArticuloNombre"] = true;
            cargarListView();
        }

        protected void listViewArticulos_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            (listViewArticulos.FindControl("DataPager1") as DataPager).SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            this.cargarListView();
        }

        protected void btnQuitarFiltros_Click(object sender, EventArgs e)
        {
            Session["filtrarArticuloNombre"] = false;
            Session["precioArticulo"] = false;
            Session["categorias"] = false;
            cargarListView();
            txtPrecioMaximo.Text = "";
            txtPrecioMin.Text = "";
            txtNombreArticulo.Text = "";
            ddlCategorias.SelectedIndex = 0;
        }

        protected void ddlCategorias_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["categorias"] = true;
            cargarListView();
        }

        protected void btnFiltrarPrecio_Click(object sender, EventArgs e)
        {
            if (!verificacionTxt())
                return;
            Session["precioArticulo"] = true;
            cargarListView();
        }

        protected void btnSeleccionar_Command1(object sender, CommandEventArgs e)
        {
            if (Session["nombre"] == null)
            {
                lblPreguntaConfirmacion.Text = "DEBE INICIAR SESION PRIMERO PARA PODER ENVIAR UN PRODUCTO AL CARRITO";
                return;
            }
            carritoEntidades carrito = new carritoEntidades();
            carrito.Id_articulo = Convert.ToInt32(e.CommandArgument.ToString().Split('-')[0]);

            lblPreguntaConfirmacion.Text = "";
            carrito.Dni = Session["dni"].ToString();
            carritoNegocios carritoNegocios1 = new carritoNegocios();
            if (carritoNegocios1.verificarSeleccionArticulo(carrito))
            {
                lblPreguntaConfirmacion.Visible = true;
                lblPreguntaConfirmacion.Text = "ESTE PRODUCTO YA ESTA EN SU CARRITO, SI DESEA MODIFICAR SU CANTIDAD DIRIJASE A ESA MISMA PAGINA";
                lblCarrito.Text = "";
                return;
            }

            if (e.CommandName == "Seleccionar")
            {
                carrito.DescripcionArticulo = e.CommandArgument.ToString().Split('-')[1];
                carritoNegocios carritoNegocios = new carritoNegocios();
                carritoNegocios.agregarArticuloCarrito(carrito);
                lblCarrito.Visible = true;
                lblCarrito.Text = "PRODUCTO AÑADIDO AL CARRITO :" + carrito.DescripcionArticulo;
            }
        }
    }
}