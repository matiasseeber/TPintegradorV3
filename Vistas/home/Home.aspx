<%@ Page Title="" EnableEventValidation="true" Language="C#" MasterPageFile="~/PaginaEdicion.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="ShopMarket.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    </asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContenidoCentral" runat="server">
    <div style="height: 100%; width: 100%;">
        <center>
            <div style="width:100%;">
                <asp:TextBox ID="txtNombreArticulo" placeholder="Buscar Producto" runat="server"></asp:TextBox>&nbsp<asp:Button ID="btnBuscarNombre" runat="server" Text="Buscar" OnClick="btnBuscarNombre_Click"></asp:Button>&nbsp<asp:TextBox ID="txtPrecioMin" runat="server" placeholder="Precio Minimo" TextMode="Number"></asp:TextBox>&nbsp<asp:TextBox ID="txtPrecioMaximo" runat="server" placeholder="Precio Maximo" TextMode="Number" min ="1"></asp:TextBox>&nbsp<asp:Button ID="btnFiltrarPrecio" runat="server" Text="Filtrar" OnClick="btnFiltrarPrecio_Click"></asp:Button>&nbsp<asp:DropDownList ID="ddlCategorias" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCategorias_SelectedIndexChanged"></asp:DropDownList>
            &nbsp;<asp:Button ID="btnQuitarFiltros" runat="server" OnClick="btnQuitarFiltros_Click" Text="Quitar Filtros" />
            </div>
            <br />
            <asp:Label ID="lblCarrito" runat="server" ForeColor="Blue"></asp:Label>
            <br />
            <br />
            <div class="divListView" style="width:100%;">
                <br />
                <asp:ListView ID="listViewArticulos" runat="server" GroupItemCount="3" OnPagePropertiesChanging="listViewArticulos_PagePropertiesChanging">
               
                    <EditItemTemplate>
                        <td runat="server" style="background-color:#008A8C;color: #FFFFFF;">DescripcionArticulo:
                            <asp:TextBox ID="DescripcionArticuloTextBox" runat="server" Text='<%# Bind("DescripcionArticulo") %>' />
                            <br />precioUnitarioArticulo:
                            <asp:TextBox ID="precioUnitarioArticuloTextBox" runat="server" Text='<%# Bind("precioUnitarioArticulo") %>' />
                            <br />stockDisponibleArticulo:
                            <asp:TextBox ID="stockDisponibleArticuloTextBox" runat="server" Text='<%# Bind("stockDisponibleArticulo") %>' />
                            <br />url_articulo_img:
                            <asp:TextBox ID="url_articulo_imgTextBox" runat="server" Text='<%# Bind("url_articulo_img") %>' />
                            <br />idCategoria:
                            <asp:TextBox ID="idCategoriaTextBox" runat="server" Text='<%# Bind("idCategoria") %>' />
                            <br />idArticulo:
                            <asp:Label ID="idArticuloLabel1" runat="server" Text='<%# Eval("idArticulo") %>' />
                            <br />
                            <asp:CheckBox ID="estadoCheckBox" runat="server" Checked='<%# Bind("estado") %>' Text="estado" />
                            <br />
                            <asp:Button ID="UpdateButton" runat="server" CommandName="Update" Text="Actualizar" />
                            <br />
                            <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Cancelar" />
                            <br /></td>
                    </EditItemTemplate>
                    <EmptyDataTemplate>
                        <table runat="server" style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;">
                            <tr>
                                <td>No se han devuelto datos.</td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <EmptyItemTemplate>
<td runat="server" />
                    </EmptyItemTemplate>
                    <GroupTemplate>
                        <tr id="itemPlaceholderContainer" runat="server">
                            <td id="itemPlaceholder" runat="server"></td>
                        </tr>
                    </GroupTemplate>
                    <InsertItemTemplate>
                        <td runat="server" style="">DescripcionArticulo:
                            <asp:TextBox ID="DescripcionArticuloTextBox" runat="server" Text='<%# Bind("DescripcionArticulo") %>' />
                            <br />precioUnitarioArticulo:
                            <asp:TextBox ID="precioUnitarioArticuloTextBox" runat="server" Text='<%# Bind("precioUnitarioArticulo") %>' />
                            <br />stockDisponibleArticulo:
                            <asp:TextBox ID="stockDisponibleArticuloTextBox" runat="server" Text='<%# Bind("stockDisponibleArticulo") %>' />
                            <br />url_articulo_img:
                            <asp:TextBox ID="url_articulo_imgTextBox" runat="server" Text='<%# Bind("url_articulo_img") %>' />
                            <br />idCategoria:
                            <asp:TextBox ID="idCategoriaTextBox" runat="server" Text='<%# Bind("idCategoria") %>' />
                            <br />
                            <asp:CheckBox ID="estadoCheckBox" runat="server" Checked='<%# Bind("estado") %>' Text="estado" />
                            <br />
                            <asp:Button ID="InsertButton" runat="server" CommandName="Insert" Text="Insertar" />
                            <br />
                            <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Borrar" />
                            <br /></td>
                    </InsertItemTemplate>
                    <ItemTemplate>
                        <td runat="server" style="background-color:#FFFFFF;color: #000000;">&nbsp;<asp:Label ID="DescripcionArticuloLabel" runat="server" Text='<%# Eval("DescripcionArticulo") %>' />
                            <br />Precio:
                            <asp:Label ID="precioUnitarioArticuloLabel" runat="server" Text='<%# Eval("precioUnitarioArticulo") %>' />
                            <br />Stock Disponible:
                            <asp:Label ID="stockDisponibleArticuloLabel" runat="server" Text='<%# Eval("stockDisponibleArticulo") %>' />
                            <br />
                            <asp:ImageButton ID="ImageButton" runat="server" Height="170px" ImageUrl='<%# Bind("url_articulo_img") %>' Width="170px" />
                            <br />
                            <br />
                            <br />
                            <br />
                            <asp:Button ID="btnSeleccionar" runat="server" CommandName="Seleccionar" OnCommand="btnSeleccionar_Command1" Text='Seleccionar' CommandArgument='<%# Eval("idArticulo")+"-"+Eval("DescripcionArticulo")+"-"+Eval("precioUnitarioArticulo")+"-"+"" %>' />
                            <br />
                            <br /></td>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table runat="server">
                            <tr runat="server">
                                <td runat="server">
                                    <table id="groupPlaceholderContainer" runat="server" border="1" style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:solid;border-width:3px;font-family: Verdana, Arial, Helvetica, sans-serif; text-align:center">
                                        <tr id="groupPlaceholder" runat="server">
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr runat="server">
                                <td runat="server" style="text-align: center;background-color: #CCCCCC;font-family: Verdana, Arial, Helvetica, sans-serif;color: #000000;">
                                    <asp:DataPager ID="DataPager1" runat="server" PageSize="9">
                                        <Fields>
                                            <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowLastPageButton="True" />
                                        </Fields>
                                    </asp:DataPager>
                                </td>
                            </tr>
                        </table>
                    </LayoutTemplate>
                    <SelectedItemTemplate>
                        <td runat="server" style="background-color:#008A8C;font-weight: bold;color: #FFFFFF;">DescripcionArticulo:
                            <asp:Label ID="DescripcionArticuloLabel" runat="server" Text='<%# Eval("DescripcionArticulo") %>' />
                            <br />precioUnitarioArticulo:
                            <asp:Label ID="precioUnitarioArticuloLabel" runat="server" Text='<%# Eval("precioUnitarioArticulo") %>' />
                            <br />stockDisponibleArticulo:
                            <asp:Label ID="stockDisponibleArticuloLabel" runat="server" Text='<%# Eval("stockDisponibleArticulo") %>' />
                            <br />url_articulo_img:
                            <asp:Label ID="url_articulo_imgLabel" runat="server" Text='<%# Eval("url_articulo_img") %>' />
                            <br />idCategoria:
                            <asp:Label ID="idCategoriaLabel" runat="server" Text='<%# Eval("idCategoria") %>' />
                            <br />idArticulo:
                            <asp:Label ID="idArticuloLabel" runat="server" Text='<%# Eval("idArticulo") %>' />
                            <br />
                            <asp:CheckBox ID="estadoCheckBox" runat="server" Checked='<%# Eval("estado") %>' Enabled="false" Text="estado" />
                            <br /></td>
                    </SelectedItemTemplate>
                </asp:ListView>
                <br />
            </div>
            <div class="avisos" style="width:100%;">
                <asp:Label ID="lblPreguntaConfirmacion" runat="server" Text="" ForeColor="Red"></asp:Label>
                <br />
                <br />
                <br />
                <br />
            </div>
        </center>
    </div>
</asp:Content>
