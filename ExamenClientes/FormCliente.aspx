<%@ Page Title="Examen Clientes" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="FormCliente.aspx.vb" Inherits="ExamenClientes.FormCliente" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<div class="row mb-3">
    <div class="col-md-4">
        <!-- Campo para ingresar el nombre del cliente -->
        <div class="form-group mb-3">
            <label for="txtNombre">Nombre</label>
            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <!-- Campo para ingresar los apellidos del cliente -->
        <div class="form-group mb-3">
            <label for="txtApellidos">Apellidos</label>
            <asp:TextBox ID="txtApellidos" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <!-- Campo para ingresar el email del cliente -->
        <div class="form-group mb-3">
            <label for="txtEmail">Email</label>
            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <!-- Campo para ingresar el teléfono del cliente -->
        <div class="form-group mb-4">
            <label for="txtTelefono">Telefono</label>
            <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <!-- Botones para guardar o cancelar la operación -->
        <div class="form-group d-flex justify-content-between">
           <asp:Button ID="btnGuardar" CssClass="btn btn-success me-2" runat="server" Text="Guardar" OnClick="btnGuardar_Click" />
           <asp:Button ID="btnCancelar" CssClass="btn btn-success" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" />
        </div>
    </div>
    
    <asp:Label ID="LblMensaje" runat="server" Text=""></asp:Label>
</div>
     <!-- GridView para mostrar la lista de clientes con botones de seleccionar y eliminar -->
     <!-- Botón para seleccionar fila y cargar datos al formulario -->
     <!-- Botón para eliminar el registro seleccionado -->
     <!-- Columnas para mostrar los datos del cliente -->
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="ClienteId" DataSourceID="SqlDataSource1">
        <Columns>
            <asp:CommandField ShowSelectButton="True"/> 
            <asp:CommandField ShowDeleteButton="True"/>
            <asp:BoundField DataField="ClienteId" HeaderText="ClienteId" InsertVisible="False" ReadOnly="True" SortExpression="ClienteId" />
            <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" />
            <asp:BoundField DataField="Apellidos" HeaderText="Apellidos" SortExpression="Apellidos" />
            <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
            <asp:BoundField DataField="Telefono" HeaderText="Telefono" SortExpression="Telefono" />
        </Columns>
</asp:GridView>
<asp:SqlDataSource ID="SqlDataSource1" runat="server"
    ConnectionString="<%$ ConnectionStrings:ClientesDBConnectionString5 %>"
    ProviderName="<%$ ConnectionStrings:ClientesDBConnectionString5.ProviderName %>"
    SelectCommand="SELECT * FROM [Clientes]"
    InsertCommand="INSERT INTO Clientes (Nombre, Apellidos, Email, Telefono) VALUES (@Nombre, @Apellidos, @Email, @Telefono)"
    UpdateCommand="UPDATE Clientes SET Nombre=@Nombre, Apellidos=@Apellidos, Email=@Email, Telefono=@Telefono WHERE ClienteId=@ClienteId"
    DeleteCommand="DELETE FROM Clientes WHERE ClienteId=@ClienteId">
    <InsertParameters>
        <asp:Parameter Name="Nombre" Type="String" />
        <asp:Parameter Name="Apellidos" Type="String" />
        <asp:Parameter Name="Email" Type="String" />
        <asp:Parameter Name="Telefono" Type="String" />
    </InsertParameters>    
    <UpdateParameters>
        <asp:Parameter Name="Nombre" Type="String" />
        <asp:Parameter Name="Apellidos" Type="String" />
        <asp:Parameter Name="Email" Type="String" />
        <asp:Parameter Name="Telefono" Type="String" />
        <asp:Parameter Name="ClienteId" Type="Int32" />
    </UpdateParameters>
    <DeleteParameters>
        <asp:Parameter Name="ClienteId" Type="Int32" />
    </DeleteParameters>
</asp:SqlDataSource>
</asp:Content>