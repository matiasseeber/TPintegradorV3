<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="TPintegrador.Inicio" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link rel ="stylesheet" href="hojaLogin.css" />
    <link rel="icon" href="logo.ico" />
    </head>
<body>
    <form id="form1" runat="server">
    <div class="logo"><img src="../../imagenes/1 (2).png" /></div>
        <br />
        <div class="contenedorLogin">
            <div class="padre"><div class="hijo">
            <asp:TextBox ID="txtNomUsuario" runat="server" BorderWidth="2px" BorderColor="Green" CssClass="textBox" ForeColor="Black"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNomUsuario" ValidationGroup="1">*</asp:RequiredFieldValidator>
            </div>
            <div class="hijo2"><strong>Email: </strong> </div>
        </div>
        <br />
        <div class="padre"> 
            <div class="hijo2"><strong>Contraseña: </strong> </div>
            <div class="hijo"><asp:TextBox ID="txtContra" runat="server" BorderWidth="2px" BorderColor="Green" CssClass="textBox" ForeColor="Black" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtContra" ValidationGroup="1">*</asp:RequiredFieldValidator>
            </div>
        </div>
        <br />
        <div class="padre">
            <center>
                <asp:Button ID="btnLogin" runat="server" Text="Login" Width="100px" ValidationGroup="1" OnClick="btnLogin_Click" />
            </asp:TextBox>&nbsp;<asp:Button ID="btnRegistrarse" runat="server" Text="Registrarse" PostBackUrl="~/Vistas/Login/registrarse.aspx" Width="100px" />
            &nbsp;<asp:Button ID="btnCerrarSession" runat="server" Text="Cerrar Sesion" Width="100px" OnClick="btnCerrarSession_Click" />
            </center>
        </div>
        <br />
        <br />
        <br />
        <div style="width:100%;">
            <center>
                <asp:Label ID="lblMsjAclaratorio" runat="server" Text=""></asp:Label>
            </center>
        </div>
    </form>
    </body>
</html>
