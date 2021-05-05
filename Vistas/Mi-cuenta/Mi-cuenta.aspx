<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaEdicion.Master" AutoEventWireup="true" CodeBehind="Mi-cuenta.aspx.cs" Inherits="ShopMarket.Vistas.Mi_cuenta.Mi_cuenta" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contenidoCentral" runat="server">
    <div class="contenedorCentral">
        <center>
            <br />
            <asp:Label ID="lblAvisoUsuario" runat="server" Font-Bold="True" ForeColor="Red" Text="DEBE HABER INICIADO SESION PARA ACCEDER A LA INFORMACION DE ESTA PAGINA"></asp:Label>
            <br />
            <br />
            <asp:Button ID="btnAdministrar" runat="server" Text="Administrar" PostBackUrl="~/Vistas/Administrador/Administrador.aspx" Visible="False" />
        <br />
            <asp:Label ID="lblValidacion" runat="server" Font-Bold="True" Font-Italic="True" ForeColor="Red"></asp:Label>
            <br />
            <br />
            <div class="infoCliente" style="width:55%; background-color:white; height:170px; border-color:black; border-width: 3px; border-style: solid;">
            <div class="hijo">
                <asp:Label ID="lblNombreUsuario" runat="server"></asp:Label>
            </div><div class="hijo2">Nombre:</div>
            <br />
            <div class="hijo">
                <asp:Label ID="lblApellidoUsuario" runat="server"></asp:Label>
            </div><div class="hijo2">Apellido: </div>
            <br />
            <div class="hijo">
                <asp:Label ID="lblDniUsuario" runat="server"></asp:Label>
            </div><div class="hijo2">Dni:&nbsp; </div>
            <br />
            <div class="hijo">
                <asp:TextBox ID="txtDireccion" runat="server"></asp:TextBox>
                <asp:Button ID="btnModificarDireccion" runat="server" Text="Modificar" OnClick="btnModificarDireccion_Click" />
            </div><div class="hijo2">Direccion: </div>
            <br />
            <div class="hijo">
                <asp:Label ID="lblEmailUsuario" runat="server"></asp:Label>
            </div><div class="hijo2">Email:</div>
            <br />
            <div class="hijo">
                <asp:TextBox ID="txtNumTarjetaCred" runat="server" TextMode="Number" min="0"></asp:TextBox>
&nbsp;<asp:TextBox ID="txtCodSeguridad" runat="server" TextMode="Number" Width="50px" min="0"></asp:TextBox>
                <asp:Button ID="btnModificar" runat="server" Text="Modificar" OnClick="btnModificar_Click" />
            </div><div class="hijo2">NumTarjetaCred:</div>
        </div>
            <br />
            <div>
                <asp:GridView ID="grvFactuas" runat="server" AllowPaging="True" AutoGenerateColumns="False" AutoGenerateSelectButton="True" BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" CellSpacing="2" ForeColor="Black" OnPageIndexChanging="grvFactuas_PageIndexChanging" OnSelectedIndexChanging="grvFactuas_SelectedIndexChanging">
                    <Columns>
                        <asp:TemplateField HeaderText="Numero de factura">
                            <ItemTemplate>
                                <asp:Label ID="lblNumeroFactura" runat="server" Text='<%# Bind("id_Factura") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Fecha de venta">
                            <ItemTemplate>
                                <asp:Label ID="lblFecha" runat="server" Text='<%# Bind("fecha_venta") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Total">
                            <ItemTemplate>
                                <asp:Label ID="lblMontoFinal" runat="server" Text='<%# Bind("monto_final") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="#CCCCCC" />
                    <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
                    <RowStyle BackColor="White" />
                    <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#808080" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#383838" />
                </asp:GridView>
                <br />
                <asp:GridView ID="grvDetalleFacturas" runat="server" BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" CellSpacing="2" ForeColor="Black">
                    <FooterStyle BackColor="#CCCCCC" />
                    <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
                    <RowStyle BackColor="White" />
                    <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#808080" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#383838" />
                </asp:GridView>
            </div>
        </center>
    </div>
</asp:Content>
