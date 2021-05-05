using Entidades;
using Negocios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI.WebControls;
namespace ShopMarket.Vistas.Administrador
{
    public partial class Administrador : System.Web.UI.Page
    {
        public int devolverCantidad(DataTable dt)
        {

            int cont = 0;

            foreach(DataRow row in dt.Rows)
            {
                cont++;
            }

            return cont;
        }

        public string mejorCliente(DataTable factura)
        {

            string mejorCliente = "";
            decimal totalMejor = 0;

            foreach(DataRow dataRow in factura.Rows)
            {

                string dni = dataRow[1].ToString(); ;
                decimal total = 0;

                foreach (DataRow Row in factura.Rows)
                {
                    if (dataRow[1].ToString() == Row[1].ToString())
                        total += Convert.ToDecimal(Row[3]);
                }

                if(total > totalMejor || totalMejor == 0)
                {
                    mejorCliente = dni;
                    totalMejor = total;
                }

            }

            return mejorCliente;

        }

        public void reportesTotales(DataTable dt)
        {
            
            int cont = 0;
            decimal aux, suma = 0;
            foreach (DataRow row in dt.Rows)
            {
                suma += Convert.ToDecimal(dt.Rows[cont][3]);
                cont++;
            }

            aux = suma;
            lblRecaudado.Text ="Monto total recaudado en este periodo: "+ aux.ToString();

            categoriasNegocios categoriasNegocios = new categoriasNegocios();
            DataTable dataTableCategorias = categoriasNegocios.cargarCategorias();//TODO
            articulosNegocios articulosNegocios = new articulosNegocios();
            DataTable dataTableArticulos = articulosNegocios.cargarGrvArticulo("select * from Articulos");
            facturasNegocios facturas = new facturasNegocios();
            DataTable detalleFacturas = facturas.cargarGrv("select * from detalleFacturas");

            int cantCategorias = devolverCantidad(dataTableCategorias);
            int cantArticulos = devolverCantidad(dataTableArticulos);

            int[] categorias;
            categorias = new int[cantCategorias];

            int[] articulosVendidos;
            articulosVendidos = new int[cantArticulos];

            string consulta = "";

            foreach (DataRow dataRow in detalleFacturas.Rows)
            {
                articulosVendidos[Convert.ToInt32(dataRow[2]) - Convert.ToInt32(dataTableArticulos.Rows[0][0])] += Convert.ToInt32(dataRow[5]);
                consulta = "select idCategoria from Articulos where idArticulo="+dataRow[2].ToString();
                DataTable aux2 = articulosNegocios.cargarGrvArticulo(consulta);
                categorias[Convert.ToInt32(aux2.Rows[0][0]) - Convert.ToInt32(dataTableCategorias.Rows[0][0])] += Convert.ToInt32(dataRow[5]);
            }

            int mayor = 0, cant = 0;

            for (int i=0;i<cantArticulos;i++)
            {
                if(cant < articulosVendidos[i] || i == 0)
                {
                    mayor = i + Convert.ToInt32(dataTableArticulos.Rows[0][0]);
                    cant = articulosVendidos[i];
                }
            }
            consulta = "select * from Articulos where idArticulo = " + mayor.ToString();
            dataTableArticulos = articulosNegocios.cargarGrvArticulo(consulta);
            lblProductoMasVendido.Text = "El articulo mas vendido fue "+dataTableArticulos.Rows[0][2].ToString();

            for (int i = 0; i < cantCategorias; i++)
            {
                if (cant < categorias[i] || i == 0)
                {
                    mayor = i + Convert.ToInt32(dataTableCategorias.Rows[0][0]);
                    cant = categorias[i];
                }
            }

            for(int i = 0; i < cantCategorias; i++)
            {
                if (mayor == Convert.ToInt32(dataTableCategorias.Rows[i][0]))
                {
                    lblCategoriaMasVendida.Text = "La categoria mas vendida fue "+ dataTableCategorias.Rows[i][1].ToString();
                }
            }

            facturasNegocios facturasNegocios = new facturasNegocios();
            UsuariosNegocios usuarios = new UsuariosNegocios();
            string MejorCliente = mejorCliente(facturasNegocios.cargarGrv("select * from facturas"));
            DataTable usuariosDt = usuarios.cargarGrv("select * from usuarios where dniUsuario = " + MejorCliente);
            lblMejorCliente.Text = "El mejor cliente fue " + usuariosDt.Rows[0][1].ToString()+" "+ usuariosDt.Rows[0][2].ToString();
        }

        public void reportesPorFecha(DataTable dt, string fechaInicio, string fechaFin)
        {

            int cont = 0;
            decimal aux, suma = 0;
            foreach (DataRow row in dt.Rows)
            {
                suma += Convert.ToDecimal(dt.Rows[cont][3]);
                cont++;
            }

            aux = suma;
            lblRecaudado.Text = "Monto total recaudado en este periodo: " + aux.ToString();

            if (aux == 0)
            {
                lblCategoriaMasVendida.Visible = false;
                lblProductoMasVendido.Visible = false;
                lblMejorCliente.Visible = false;
                lblRecaudado.Visible = false;
                return;
            }
            categoriasNegocios categoriasNegocios = new categoriasNegocios();
            DataTable dataTableCategorias = categoriasNegocios.cargarCategorias();//TODO
            articulosNegocios articulosNegocios = new articulosNegocios();
            DataTable dataTableArticulos = articulosNegocios.cargarGrvArticulo("select * from Articulos");
            facturasNegocios facturas = new facturasNegocios();
            DataTable detalleFacturas = facturas.cargarGrv("select detalleFacturas.id_factura,numeroDeOrden,id_articulo,precio_unitario,descripcionProducto,cantidad from detalleFacturas inner join facturas on detalleFacturas.id_factura = facturas.id_Factura where fecha_venta between '" + fechaInicio + "' and '" + fechaFin + "'");

            int cantCategorias = devolverCantidad(dataTableCategorias);
            int cantArticulos = devolverCantidad(dataTableArticulos);

            int[] categorias;
            categorias = new int[cantCategorias];

            int[] articulosVendidos;
            articulosVendidos = new int[cantArticulos];

            string consulta = "";

            foreach (DataRow dataRow in detalleFacturas.Rows)
            {
                articulosVendidos[Convert.ToInt32(dataRow[2]) - Convert.ToInt32(dataTableArticulos.Rows[0][0])] += Convert.ToInt32(dataRow[5]);
                consulta = "select idCategoria from Articulos where idArticulo=" + dataRow[2].ToString();
                DataTable aux2 = articulosNegocios.cargarGrvArticulo(consulta);
                categorias[Convert.ToInt32(aux2.Rows[0][0]) - Convert.ToInt32(dataTableCategorias.Rows[0][0])] += Convert.ToInt32(dataRow[5]);
            }

            int mayor = 0, cant = 0;

            for (int i = 0; i < cantArticulos; i++)
            {
                if (cant < articulosVendidos[i] || i == 0)
                {
                    mayor = i + Convert.ToInt32(dataTableArticulos.Rows[0][0]);
                    cant = articulosVendidos[i];
                }
            }
            consulta = "select * from Articulos where idArticulo = " + mayor.ToString();
            dataTableArticulos = articulosNegocios.cargarGrvArticulo(consulta);
            lblProductoMasVendido.Text = "El articulo mas vendido fue " + dataTableArticulos.Rows[0][2].ToString();

            for (int i = 0; i < cantCategorias; i++)
            {
                if (cant < categorias[i] || i == 0)
                {
                    mayor = i + Convert.ToInt32(dataTableCategorias.Rows[0][0]);
                    cant = categorias[i];
                }
            }

            for (int i = 0; i < cantCategorias; i++)
            {
                if (mayor == Convert.ToInt32(dataTableCategorias.Rows[i][0]))
                {
                    lblCategoriaMasVendida.Text = "La categoria mas vendida fue " + dataTableCategorias.Rows[i][1].ToString();
                }
            }

            UsuariosNegocios usuarios = new UsuariosNegocios();
            string MejorCliente = mejorCliente(dt);
            DataTable usuariosDt = usuarios.cargarGrv("select * from usuarios where dniUsuario = '" + MejorCliente +"'");
            lblMejorCliente.Text = "El mejor cliente fue " + usuariosDt.Rows[0][1].ToString() + " " + usuariosDt.Rows[0][2].ToString();
        }

        public void esconderControlesProductos()
        {
            txtDescripcionArticulo.Visible = false;
            txtPrecioUnitario.Visible = false;
            txtStock.Visible = false;
            txtUrlImagen.Visible = false;
            btnAgregarArticulo.Visible = false;
            ddlCategoriasArt.Visible = false;
            txtNombreProducto.Visible = false;
            btnFiltrarNombreProducto.Visible = false;
            btnQuitarFiltros.Visible = false;
            ddlCategoriasArticulos.Visible = false;
        }

        public void esconderControlesFacturas()
        {
            lblDesde.Visible = false;
            lblHasta.Visible = false;
            Calendar1.Visible = false;
            Calendar2.Visible = false;
            lblCategoriaMasVendida.Visible = false;
            lblProductoMasVendido.Visible = false;
            btnFiltrar.Visible = false;
            btnQuitarFiltro.Visible = false;
            lblRecaudado.Visible = false;
            lblProductoMasVendido.Visible = false;
            lblCategoriaMasVendida.Visible = false;
            lblCategoriaMasVendida.Visible = false;
            btnMostrarReporte.Visible = false;
            grvFacturas.Visible = false;
            lblProductoMasVendido.Visible = false;
            lblCategoriaMasVendida.Visible = false;
            Session["mostrarReportes"] = false;
            Session["filtrarFecha"] = false;
        }

        public void esconderControles()
        {
            btnQuitarFiltros.Visible = false;
            if (ddlAdmin.SelectedValue != "Categorias")
            {
                txtDescripcionCategoria.Visible = false;
                btnAgregarCategoria.Visible = false;
            }

            if (ddlAdmin.SelectedValue != "Productos")
            {
                esconderControlesProductos();
            }

            if (ddlAdmin.SelectedValue != "facturas")
            {
                esconderControlesFacturas();
            }

            if(ddlAdmin.SelectedValue != "Usuarios")
            {
                txtNomUsuario.Visible = false;
                btnNombreUsuarioFiltrar.Visible = false;
                BtnQuitarFiltroUsuario.Visible = false;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Form.Attributes.Add("autocomplete", "off");
            this.UnobtrusiveValidationMode = System.Web.UI.UnobtrusiveValidationMode.None;
            if(ddlAdmin.SelectedIndex == 0)
            {
                esconderControles();
            }

            if (!IsPostBack)
            {
                cargarCategoriasDDL();
            }
            if (Session["nombre"] == null)
            {
                Response.Redirect("../Home/Home.aspx");
            }

            if (lblAgregar.Text != "")
            {
                lblAgregar.Text = "";
            }

            if (Convert.ToBoolean(Session["master"])==false)
            {
                grvAdmin.AutoGenerateEditButton = false;
            }
            else
            {
                grvAdmin.AutoGenerateEditButton = true;
            }
        }

        public void cargarCategoriasDDL()
        {
            categoriasNegocios categoriasNegocios = new categoriasNegocios();
            SqlDataReader sqlData = categoriasNegocios.cargarDDL();
            ddlCategoriasArt.DataSource = sqlData;
            ddlCategoriasArt.DataTextField = "descripcionCategoria";
            ddlCategoriasArt.DataValueField = "idCategoria";
            ddlCategoriasArt.DataBind();
        }

        public void cargarCategoriasDDLproductos()
        {
            categoriasNegocios categoriasNegocios = new categoriasNegocios();
            SqlDataReader sqlData = categoriasNegocios.cargarDDL();
            ddlCategoriasArticulos.DataSource = sqlData;
            ddlCategoriasArticulos.DataTextField = "descripcionCategoria";
            ddlCategoriasArticulos.DataValueField = "idCategoria";
            ddlCategoriasArticulos.DataBind();
        }

        public void categoriaselec()
        {
            grvAdmin.Visible = false;
            grvFacturas.Visible = false;
            grvProductos.Visible = false;
            grvCategorias.Visible = true;
            txtDescripcionCategoria.Visible = true;
            btnAgregarCategoria.Visible = true;
        }

        public void productosSelec()
        {
            grvAdmin.Visible = false;
            grvCategorias.Visible = false;
            grvFacturas.Visible = false;
            txtDescripcionArticulo.Visible = true;
            txtPrecioUnitario.Visible = true;
            txtStock.Visible = true;
            txtUrlImagen.Visible = true;
            btnAgregarArticulo.Visible = true;
            ddlCategoriasArt.Visible = true;
            txtNombreProducto.Visible = true;
            btnFiltrarNombreProducto.Visible = true;
            btnQuitarFiltros.Visible = true;
            ddlCategoriasArticulos.Visible = true;
            grvProductos.Visible = true;
        }

        public void facturasSelec()
        {
            lblDesde.Visible = true;
            lblHasta.Visible = true;
            Calendar1.Visible = true;
            Calendar2.Visible = true;
            grvAdmin.Visible = false;
            grvCategorias.Visible = false;
            grvProductos.Visible = false;
            grvFacturas.Visible = true;
            btnFiltrar.Visible = true;
            btnQuitarFiltro.Visible = true;
        }

        public void usuariosSelec() {
            grvCategorias.Visible = false;
            grvFacturas.Visible = false;
            grvProductos.Visible = false;
            grvAdmin.Visible = true;
            txtNomUsuario.Visible = true;
            btnNombreUsuarioFiltrar.Visible = true;
            BtnQuitarFiltroUsuario.Visible = true;
        }

        public void cargarGrvArticulosCategoria()
        {
            articulosNegocios articulo = new articulosNegocios();
            grvProductos.DataSource = articulo.cargarGrvArticulo("select * from Articulos inner join Categorias on Articulos.idCategoria = Categorias.idCategoria where estado = 1 and descripcionCategoria = '" + ddlCategoriasArticulos.SelectedItem.ToString() + "'");
            grvProductos.DataBind();
        }

        public bool verificarFiltros()
        {
            if (Convert.ToBoolean(Session["filtrarFecha"]))
            {
                return true;
            }

            return false;
        }

        public void filtrarNombre()
        {
            usuariosSelec();
            UsuariosNegocios aux = new UsuariosNegocios();
            string consulta = "select * from usuarios where adminMaster <> 1 and estado = 1 and (NombreUsuario like '%"+txtNomUsuario.Text+ "%' or ApellidoUsuario like '%" + txtNomUsuario.Text + "%')";
            grvAdmin.DataSource = aux.cargarGrv(consulta);
            grvAdmin.DataBind();
        }

        public void FiltrarNombreProducto()
        {
            string nombre = txtNombreProducto.Text;
            string consulta = "select * from Articulos inner join Categorias on Articulos.idCategoria = Categorias.idCategoria where estado = 1 and DescripcionArticulo like '%" + nombre + "%'";
            articulosNegocios articulos = new articulosNegocios();
            grvProductos.DataSource = articulos.cargarGrvArticulo(consulta);
            grvProductos.DataBind();
        }

        public void mostrarReportes()
        {
            if (Convert.ToBoolean(Session["mostrarReportes"]))
            {
                lblRecaudado.Visible = true;
                lblProductoMasVendido.Visible = true;
                lblCategoriaMasVendida.Visible = true;
                lblCategoriaMasVendida.Visible = true;
                lblMejorCliente.Visible = true;
            }
            else
            {
                lblRecaudado.Visible = false;
                lblProductoMasVendido.Visible = false;
                lblCategoriaMasVendida.Visible = false;
                lblCategoriaMasVendida.Visible = false;
                lblMejorCliente.Visible = false;
            }
        }

        public void cargarGrv()
        {
            grvDetalleFactura.Visible = false;
            switch (ddlAdmin.SelectedIndex)
            {
                case 1:
                    categoriaselec();
                    categoriasNegocios categoriasNegocios = new categoriasNegocios();
                    grvCategorias.DataSource = categoriasNegocios.cargarCategorias();
                    grvCategorias.DataBind();
                    break;
                case 2:
                    productosSelec();
                    if (Convert.ToBoolean(Session["filtrarNombreProducto"]))
                    {
                        FiltrarNombreProducto();
                        return;
                    }
                    if (ddlCategoriasArticulos.SelectedIndex != 0)
                    {
                        cargarGrvArticulosCategoria();
                        return;
                    }
                    if (Convert.ToBoolean(Session["filtrarNombreProducto"]) == false)
                    {
                        Session["filtrarNombreProducto"] = false;
                        articulosNegocios articulos = new articulosNegocios();
                        string consulta = "select * from Articulos inner join Categorias on Articulos.idCategoria = Categorias.idCategoria where estado = 1";
                        grvProductos.DataSource = articulos.cargarGrvArticulo(consulta);
                        grvProductos.DataBind();
                    }
                    break;
                case 3:
                    mostrarReportes();
                    if (verificarFiltros())
                    {
                        filtrarPorFecha();
                        return;
                    }
                    facturasSelec();
                    facturasNegocios facturas = new facturasNegocios();
                    DataTable dt = facturas.cargarGrv("select id_Factura,dni_Usuario,fecha_venta,monto_final,(NombreUsuario +' '+ApellidoUsuario) as [NombreCliente] from facturas inner join Usuarios on facturas.dni_Usuario = Usuarios.dniUsuario");
                    mostrarFiltros();
                    reportesTotales(dt);
                    grvFacturas.DataSource = dt;
                    grvFacturas.DataBind();
                    grvDetalleFactura.Visible = true;
                    break;
                case 4:
                   if (Convert.ToBoolean(Session["filtrarNombre"]))
                   {
                        filtrarNombre();
                        return;
                   }
                    usuariosSelec();
                    UsuariosNegocios aux = new UsuariosNegocios();
                    grvAdmin.DataSource = aux.cargarGrv("select * from usuarios where adminMaster <> 1 and estado = 1");
                    grvAdmin.DataBind();
                    break;
            }
        }

        public void mostrarPreguntaConfirmacion()
        {

            lblPreguntaConfirmacion.Text = "¿ESTA SEGURO QUE DESEA ELIMINAR ESTE REGISTRO?";

            if (lblPreguntaConfirmacion.Visible)
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

        //USUARIOS

        protected void grvAdmin_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvAdmin.PageIndex = e.NewPageIndex;
            cargarGrv();
        }

        protected void grvAdmin_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grvAdmin.EditIndex = e.NewEditIndex;
            cargarGrv();
        }

        protected void grvAdmin_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grvAdmin.EditIndex = -1;
            cargarGrv();
        }

        protected void grvAdmin_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string Id = ((Label)grvAdmin.Rows[e.RowIndex].FindControl("lblDniEditIT")).Text;
            bool abc = ((CheckBox)grvAdmin.Rows[e.RowIndex].FindControl("CheckBoxEstadoAdminUsuario")).Checked;
            UsuariosNegocios usuariosNegocios = new UsuariosNegocios();
            usuariosNegocios.editarUsuario(Id, abc);
            grvAdmin.EditIndex = -1;
            cargarGrv();
        }

        //CATEGORIAS

        protected void grvCategorias_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            mostrarPreguntaConfirmacion();
            int id = Convert.ToInt32(((Label)grvCategorias.Rows[e.RowIndex].FindControl("lblIdCategoria")).Text);
            Session["idBorrar"] = id;
        }

        protected void grvCategorias_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grvCategorias.EditIndex = e.NewEditIndex;
            cargarGrv();
        }

        protected void grvCategorias_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            string Id = ((Label)grvCategorias.Rows[e.RowIndex].FindControl("lblIdCatEIT")).Text;
            string nuevaDescripcion = ((TextBox)grvCategorias.Rows[e.RowIndex].FindControl("txtEITcatDes")).Text;
            lblAgregar.Text = nuevaDescripcion;

            if(nuevaDescripcion == "")
            {
                lblAgregar.ForeColor = Color.Red;
                lblAgregar.Text = "DEBE COMPLETAR LA DESCRIPCION PARA QUE LOS CAMBIOS PUEDAN SER GUARDADOS";
                return;
            }

            categoriasNegocios categoriasNegocios = new categoriasNegocios();

            if (categoriasNegocios.editarCategoria(Id, nuevaDescripcion))
            {
                lblAgregar.Text = "";
                grvCategorias.EditIndex = -1;
                cargarGrv();
            }
        }

        protected void btnAgregarCategoria_Click(object sender, EventArgs e)
        {
            string descripcion = txtDescripcionCategoria.Text;
            categoriasNegocios categoriasNegocios = new categoriasNegocios();
            if (categoriasNegocios.agregarCategoria(descripcion))
            {
                txtDescripcionCategoria.Text = "";
            }

            cargarGrv();
        }

        //articulos

        public void cargarArticulo(ref articulosEntidades articulos)
        {
            articulos.IdCategoria = Convert.ToInt32(ddlCategoriasArt.SelectedValue);
            articulos.PrecioUnitarioArticulo = float.Parse(txtPrecioUnitario.Text);
            articulos.StockDisponibleArticulo = Convert.ToInt32(txtStock.Text);
            articulos.Url_articulo_img = txtUrlImagen.Text;
            articulos.DescripcionArticulo1 = txtDescripcionArticulo.Text;
        }

        protected void btnAgregarArticulo_Click(object sender, EventArgs e)
        {
            articulosNegocios articulosNegocio = new articulosNegocios();
            articulosEntidades articulos = new articulosEntidades();
            cargarArticulo(ref articulos);
            if (articulosNegocio.agregarArticulo(articulos))
            {
                txtDescripcionArticulo.Text = "";
                txtPrecioUnitario.Text = "";
                txtStock.Text = "";
                txtUrlImagen.Text = "";
                cargarGrv();
            }
        }

        //categorias

        protected void grvCategorias_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grvCategorias.EditIndex = -1;
            cargarGrv();
        }

        //articulos

        protected void grvProductos_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grvProductos.EditIndex = e.NewEditIndex;
            cargarGrv();
        }

        protected void grvProductos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grvProductos.EditIndex = -1;
            cargarGrv();
        }

        protected void grvProductos_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(((Label)grvProductos.Rows[e.RowIndex].FindControl("lblIdArticuloGrvProductos")).Text);
            Session["idBorrar"] = id;
            mostrarPreguntaConfirmacion();
        }

        public void cargarArticuloEditado(ref articulosEntidades articulos, GridViewUpdateEventArgs e)
        {
            articulos.IdArticulo = Convert.ToInt32(((Label)grvProductos.Rows[e.RowIndex].FindControl("lblIdArticuloETgrvProdcuts")).Text);
            articulos.IdCategoria = Convert.ToInt32(((DropDownList)grvProductos.Rows[e.RowIndex].FindControl("ddlETcategoriasGrvArticulos")).SelectedValue);
            articulos.DescripcionArticulo1 = ((TextBox)grvProductos.Rows[e.RowIndex].FindControl("txtDescripcionArticuloET")).Text;
            articulos.PrecioUnitarioArticulo = float.Parse(((TextBox)grvProductos.Rows[e.RowIndex].FindControl("txtPrecioGrvProductos")).Text);
            articulos.StockDisponibleArticulo = Convert.ToInt32(((TextBox)grvProductos.Rows[e.RowIndex].FindControl("txtStockAgregar")).Text);
        }

        public bool validarCamposETproductos(GridViewUpdateEventArgs e)
        {
            bool estado = true;

            lblAgregar.Text = "";

            if (((TextBox)grvProductos.Rows[e.RowIndex].FindControl("txtDescripcionArticuloET")).Text == "")
            {
                lblAgregar.ForeColor = Color.Red;
                lblAgregar.Text = "LA DESCRIPCION DEL PRODUCTO NO PUEDE QUEDAR VACIA \n";
                estado = false;
            }

            if (((TextBox)grvProductos.Rows[e.RowIndex].FindControl("txtPrecioGrvProductos")).Text == "")
            {
                lblAgregar.ForeColor = Color.Red;
                lblAgregar.Text += " EL PRECIO NO PUEDE QUEDAR VACIO \n";
                estado = false;
            }

            if (((TextBox)grvProductos.Rows[e.RowIndex].FindControl("txtStockAgregar")).Text == "")
            {
                lblAgregar.ForeColor = Color.Red;
                lblAgregar.Text += " EL STOCK NO PUEDE QUEDAR VACIO";
                estado = false;
            }

            return estado;

        }

        protected void grvProductos_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            if (!validarCamposETproductos(e))
            {
                return;
            }

            lblAgregar.ForeColor = Color.Green;
            lblAgregar.Text = "EL REGISTRO SE EDITO CORRECTAMENTE";

            articulosEntidades articulos = new articulosEntidades();
            cargarArticuloEditado(ref articulos, e);
            articulosNegocios articulosNegocios = new articulosNegocios();

            if (articulosNegocios.editarArticulo(articulos))
            {
                grvProductos.EditIndex = -1;
                cargarGrv();
            }
        }

        public void mostrarFiltros()
        {
            btnFiltrar.Visible = true;
            btnQuitarFiltro.Visible = true;
            btnMostrarReporte.Visible = true;
        }

        public void filtrarPorFecha()
        {
           btnMostrarReporte.Visible = false;
           string fechaInicio = Calendar1.SelectedDate.ToString();
           string fechaFin = Calendar2.SelectedDate.ToString();
           facturasNegocios facturas = new facturasNegocios();
           string consulta = "select id_Factura,dni_Usuario,fecha_venta,monto_final,(NombreUsuario +' '+ApellidoUsuario) as [NombreCliente] from facturas inner join Usuarios on facturas.dni_Usuario = Usuarios.dniUsuario and fecha_venta between '"+fechaInicio+"' and '"+fechaFin+"'";
           DataTable dt = facturas.cargarGrv(consulta);
           grvFacturas.DataSource = dt;
           grvFacturas.DataBind();
           mostrarFiltros();
            reportesPorFecha(dt,fechaInicio,fechaFin);
           grvDetalleFactura.DataSource = null;
           grvDetalleFactura.DataBind();
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            if(Calendar1.SelectedDate > Calendar2.SelectedDate)
            {
                lblAgregar.Visible = true;
                lblAgregar.Text = "LA FECHA DE INICIO DEBE SER MENOR A LA FECHA FINAL DEL INTERVALO";
                return;
            }
            filtrarPorFecha();
            Session["filtrarFecha"] = true;
            btnFiltrar.Visible = true;
        }

        public void quitarFiltros()
        {
            Session["mostrarReportes"] = false;
            btnFiltrar.Visible = false;
            facturasNegocios facturas = new facturasNegocios();
            Session["filtrarFecha"] = false;
            grvDetalleFactura.DataSource = null;
            grvDetalleFactura.DataBind();
            grvFacturas.SelectedIndex = -1;
            Calendar1.SelectedDates.Clear();
            Calendar2.SelectedDates.Clear();
            lblMejorCliente.Visible = false;
        }

        protected void btnQuitarFiltro_Click(object sender, EventArgs e)
        {
            Session["mostrarReportes"] = false;
            btnFiltrar.Visible = false;
            facturasNegocios facturas = new facturasNegocios();
            Session["filtrarFecha"] = false;
            grvDetalleFactura.DataSource = null;
            grvDetalleFactura.DataBind();
            grvFacturas.SelectedIndex = -1;
            Calendar1.SelectedDates.Clear();
            Calendar2.SelectedDates.Clear();
            mostrarFiltros();
            cargarGrv();
        }

        protected void ddlAdmin_SelectedIndexChanged(object sender, EventArgs e)
        {
            esconderControles();
            if(ddlAdmin.SelectedIndex == 2)
            {
                cargarCategoriasDDLproductos();
            }

            if (ddlAdmin.SelectedIndex != 3)
                quitarFiltros();

            cargarGrv();
        }

        protected void grvFacturas_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            facturasNegocios facturas = new facturasNegocios();
            string id = ((Label)grvFacturas.Rows[e.NewSelectedIndex].FindControl("lblIdFactura")).Text;
            grvDetalleFactura.DataSource = facturas.cargarGrv("select * from detalleFacturas where id_factura = '" + id + "'");
            grvDetalleFactura.DataBind();
            mostrarFiltros();
        }

        protected void grvCategorias_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvCategorias.PageIndex = e.NewPageIndex;
            cargarGrv();
        }

        protected void grvProductos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvProductos.PageIndex = e.NewPageIndex;
            cargarGrv();
        }

        protected void grvFacturas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvFacturas.PageIndex = e.NewPageIndex;
            cargarGrv();
        }

        protected void grvDetalleFactura_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvDetalleFactura.PageIndex = e.NewPageIndex;
            cargarGrv();
        }

        protected void btnMostrarReporte_Click(object sender, EventArgs e)
        {
            mostrarFiltros();
            Session["mostrarReportes"] = true;
            cargarGrv();
        }

        protected void btnFiltrarNombreProducto_Click(object sender, EventArgs e)
        {
            Session["filtrarNombreProducto"] = true;
            cargarGrv();
        }

        protected void btnQuitarFiltros_Click(object sender, EventArgs e)
        {
            Session["filtrarNombreProducto"] = false;
            txtNombreProducto.Text = "";
            ddlCategoriasArticulos.SelectedIndex = 0;
            cargarGrv();
        }

        protected void ddlCategoriasArticulos_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarGrvArticulosCategoria();
        }

        public void borrarRegistro()
        {
            int id = Convert.ToInt32(Session["idBorrar"]);
            switch (ddlAdmin.SelectedIndex)
            {
                case 1:
                    categoriasNegocios categoriasNegocios = new categoriasNegocios();
                    if (categoriasNegocios.verificarUsoCategoria(id))
                    {
                        lblAgregar.ForeColor = Color.Red;
                        lblAgregar.Text = "ESA CATEGORIA ESTA SIENDO UTILIZADA POR UN PRODUCTO NO PUEDE SER ELIMINADA";
                        return;
                    }

                    if (categoriasNegocios.borrarCategoria(id))
                    {
                        cargarGrv();
                    }
                    break;
                case 2:
                    articulosNegocios articulosNegocios = new articulosNegocios();
                    if (articulosNegocios.verificarUsoArticulo(id))
                    {
                        if (articulosNegocios.bajaLogica(id))
                        {
                            lblAgregar.ForeColor = Color.Green;
                            lblAgregar.Text = "EL PRODUCTO HA SIDO VENDIDO, SE HA HECHO UNA BAJA LOGICA, EL PRODUCTO NO SE MOSTRARA PERO SU INFORMACION SEGUIRA DISPONIBLE EN LA BASE DE DATOS.";
                            cargarGrv();
                            return;
                        }
                    }
                    if (articulosNegocios.borrarArticulo(id))
                    {
                        lblAgregar.ForeColor = Color.Green;
                        lblAgregar.Text = "EL PRODUCTO SE HA ELIMINADO EXITOSAMENTE DE LA BASE DE DATOS";
                        cargarGrv();
                    }
                    break;
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            borrarRegistro();
            mostrarPreguntaConfirmacion();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            mostrarPreguntaConfirmacion();
            lblAgregar.Text = "HA SIDO CANCELADA LA ELIMINACION DEL REGISTRO";
        }
        protected void BtnQuitarFiltroUsuario_Click(object sender, EventArgs e)
        {
            txtNomUsuario.Text = "";
            Session["filtrarNombre"] = false;
            cargarGrv();
        }
        protected void btnNombreUsuarioFiltrar_Click(object sender, EventArgs e)
        {
            Session["filtrarNombre"] = true;
            cargarGrv();
        }
    }
}