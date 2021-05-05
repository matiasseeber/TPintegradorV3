<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="registrarse.aspx.cs" Inherits="TPintegrador.registrarse" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.5.3/dist/css/bootstrap.min.css" integrity="sha384-TX8t27EcRE3e/ihU7zmQxVncDAy5uIKz4rEkgIXeMed4M0jlfIDPvg6uqKI2xXr2" crossorigin="anonymous">
    <link rel ="stylesheet" href="hojaLogin.css" type="text/css" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="logo"><img src="../../imagenes/1 (2).png" /></div>
        <br />
        <center>
            <div class="caja">
            <br />
                <div class="hijo2">Nombre: </div>
            <div class="hijo"><asp:TextBox ID="txtNombreReg" runat="server" placeholder="Ingrese su nombre" ValidationGroup="1"></asp:TextBox><asp:RequiredFieldValidator ID="rfvNombre" runat="server" ErrorMessage="Ingrese su nombre" ControlToValidate="txtNombreReg" ForeColor="Red" ValidationGroup="1">*</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revNombre" runat="server" ControlToValidate="txtNombreReg" ErrorMessage="Ingrese solo letras" ForeColor="Red" ValidationExpression="[A-Za-z ]*" ValidationGroup="1">*</asp:RegularExpressionValidator>
                </div>
            <br />
            <div class="hijo2">Apellido: </div>
            <div class="hijo"><asp:TextBox ID="txtApellidoReg" runat="server" placeholder="Ingrese su apellido"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvApellido" runat="server" ControlToValidate="txtApellidoReg" ErrorMessage="ingrese su apellido" ForeColor="Red" ValidationGroup="1">*</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revApellido" runat="server" ControlToValidate="txtApellidoReg" ErrorMessage="Ingrese solo letras" ForeColor="Red" ValidationExpression="[A-Za-z ]*" ValidationGroup="1">*</asp:RegularExpressionValidator>
                </div>
            <br />
            <div class="hijo2">Dni: </div>
            <div class="hijo"><asp:TextBox ID="txtDniReg" runat="server" placeholder="Ingrese su dni" TextMode="Number" min="1"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvDni" runat="server" ControlToValidate="txtDniReg" ErrorMessage="Ingrese su dni" ForeColor="Red" ValidationGroup="1">*</asp:RequiredFieldValidator>
                </div>
            <br />
            <div class="hijo2">Email: </div>
            <div class="hijo"><asp:TextBox ID="txtEmailReg" runat="server" placeholder="Ingrese su email" TextMode="Email"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmailReg" ErrorMessage="Ingrese su email" ForeColor="Red" ValidationGroup="1">*</asp:RequiredFieldValidator>
                </div>
            <br />
            <div class="hijo2">Contraseña: </div>
            <div class="hijo"><asp:TextBox ID="txtContraseñaReg" runat="server" placeholder="Ingrese su contraseña" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvContraseña" runat="server" ControlToValidate="txtContraseñaReg" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="1">*</asp:RequiredFieldValidator>
                </div>
                <br />
            <div class="hijo2">Repita Contraseña: </div>
            <div class="hijo"><asp:TextBox ID="txtRepitaContraseñaReg" runat="server" placeholder="Repita su contraseña" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvRepetirContraseña" runat="server" ControlToValidate="txtRepitaContraseñaReg" ErrorMessage="Repita la contraseña" ForeColor="Red" ValidationGroup="1">*</asp:RequiredFieldValidator>
                <asp:CompareValidator ID="cmpRepetirContraseña" runat="server" ControlToCompare="txtContraseñaReg" ControlToValidate="txtRepitaContraseñaReg" ErrorMessage="Las contraseñas no coinciden" ForeColor="Red" ValidationGroup="1">*</asp:CompareValidator>
                </div>
                <div class="hijo2">Direccion: </div>
            <div class="hijo"><asp:TextBox ID="txtDireccionReg" runat="server" placeholder="Ingrese su direccion"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvDireccion" runat="server" ControlToValidate="txtDireccionReg" ErrorMessage="Ingrese su direccion" ForeColor="Red" ValidationGroup="1">*</asp:RequiredFieldValidator>
                </div>
               <br />
                <div class="hijo2">Sexo: </div>
            <div class="hijo"><asp:RadioButtonList ID="rdbSexoReg" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Selected="True">Hombre</asp:ListItem>
                <asp:ListItem>Mujer</asp:ListItem>
                <asp:ListItem>Otro</asp:ListItem>
                </asp:RadioButtonList></div>
                <br />
            <div class="hijo2">Ingrese numero de tarjeta: </div>
            <div class="hijo"><asp:TextBox ID="txtNumTarjetaReg" runat="server" placeholder="Numero frontal" TextMode="Number" min="0"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNumTarjetaReg" ErrorMessage="ingrese numero de tarjeta" ForeColor="Red">*</asp:RequiredFieldValidator>
                <asp:TextBox placeholder="Cod" ID="txtNumSegTarjReg" runat="server" Width="50px" TextMode="Number" min="0"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvNumSeg" runat="server" ControlToValidate="txtNumSegTarjReg" ErrorMessage="Ingrese numero de seguridad de la tarjeta" ForeColor="Red">*</asp:RequiredFieldValidator>
                </div>
                <br />
                <br />
                <br />
                <br />
<asp:Button ID="btnRegistrarse" runat="server" Text="Registrarse" OnClick="btnRegistrarse_Click" ValidationGroup="1"></asp:Button>    
                <br />
                <div style="margin-top:10px;">
                    <asp:Label ID="lblMsjres" runat="server" ForeColor="Red"></asp:Label>
                    <br />
                    <asp:Label ID="lblMsjres2" runat="server" Text="" ForeColor="Green" ></asp:Label>
                </div>
                <asp:ValidationSummary ID="vsummary" runat="server" ForeColor="White" ValidationGroup="1" ShowMessageBox="True" ShowSummary="False"></asp:ValidationSummary>
        </div>
        </center>
    </form>
</body>
</html>
