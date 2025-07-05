Public Class FormCliente
    Inherits System.Web.UI.Page

    ' Propiedad para guardar temporalmente el ID del cliente que se está editando
    Protected Property ClienteIdEditando As Integer
        Get
            Dim val = ViewState("ClienteIdEditando")
            If val Is Nothing Then Return 0 Else Return CInt(val)
        End Get
        Set(value As Integer)
            ViewState("ClienteIdEditando") = value
        End Set
    End Property

    ' Evento que se ejecuta al cargar la página
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LimpiarFormulario() ' Solo limpia el formulario la primera vez
        End If
    End Sub

    ' Evento que se ejecuta al hacer clic en el botón Guardar
    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs)
        LblMensaje.Text = ""
        If Not ValidarFormulario() Then Exit Sub

        If ClienteIdEditando = 0 Then
            ' Si no hay cliente seleccionado, inserta uno nuevo
            Try
                SqlDataSource1.InsertParameters.Clear()
                SqlDataSource1.InsertParameters.Add("Nombre", txtNombre.Text.Trim())
                SqlDataSource1.InsertParameters.Add("Apellidos", txtApellidos.Text.Trim())
                SqlDataSource1.InsertParameters.Add("Email", txtEmail.Text.Trim())
                SqlDataSource1.InsertParameters.Add("Telefono", txtTelefono.Text.Trim())
                SqlDataSource1.Insert()
                LblMensaje.Text = "Cliente guardado exitosamente."
                LimpiarFormulario()
            Catch ex As Exception
                ' Manejo de error para claves únicas duplicadas y otros errores
                If ex.Message.Contains("UNIQUE") AndAlso ex.Message.Contains("Email") Then
                    LblMensaje.Text = "El Email ya está registrado para otro cliente."
                ElseIf ex.Message.Contains("UNIQUE") AndAlso ex.Message.Contains("Telefono") Then
                    LblMensaje.Text = "El Teléfono ya está registrado para otro cliente."
                Else
                    LblMensaje.Text = "Error al guardar: " & ex.Message
                End If
            End Try
        Else
            ' Si hay cliente seleccionado, actualiza sus datos
            Try
                SqlDataSource1.UpdateParameters.Clear()
                SqlDataSource1.UpdateParameters.Add("Nombre", txtNombre.Text.Trim())
                SqlDataSource1.UpdateParameters.Add("Apellidos", txtApellidos.Text.Trim())
                SqlDataSource1.UpdateParameters.Add("Email", txtEmail.Text.Trim())
                SqlDataSource1.UpdateParameters.Add("Telefono", txtTelefono.Text.Trim())
                SqlDataSource1.UpdateParameters.Add("ClienteId", ClienteIdEditando.ToString())
                SqlDataSource1.Update()
                LblMensaje.Text = "Cliente actualizado exitosamente."
                LimpiarFormulario()
            Catch ex As Exception
                ' Manejo de error para claves únicas duplicadas y otros errores
                If ex.Message.Contains("UNIQUE") AndAlso ex.Message.Contains("Email") Then
                    LblMensaje.Text = "El Email ya está registrado para otro cliente."
                ElseIf ex.Message.Contains("UNIQUE") AndAlso ex.Message.Contains("Telefono") Then
                    LblMensaje.Text = "El Teléfono ya está registrado para otro cliente."
                Else
                    LblMensaje.Text = "Error al actualizar: " & ex.Message
                End If
            End Try
            ClienteIdEditando = 0
        End If
        GridView1.DataBind() ' Refresca la tabla de clientes
    End Sub

    ' Evento que se ejecuta al hacer clic en el botón Cancelar
    Protected Sub btnCancelar_Click(sender As Object, e As EventArgs)
        LimpiarFormulario()
        ClienteIdEditando = 0 ' Quita la selección de cliente
        GridView1.SelectedIndex = -1
    End Sub

    ' Limpia los campos de entrada del formulario y el mensaje
    Protected Sub LimpiarFormulario()
        txtNombre.Text = ""
        txtApellidos.Text = ""
        txtEmail.Text = ""
        txtTelefono.Text = ""
        LblMensaje.Text = ""
    End Sub

    ' Evento que se ejecuta al seleccionar una fila del GridView
    Protected Sub GridView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView1.SelectedIndexChanged
        If GridView1.SelectedIndex >= 0 Then
            Dim row As GridViewRow = GridView1.SelectedRow
            ClienteIdEditando = Convert.ToInt32(GridView1.SelectedDataKey.Value)
            ' Carga los datos de la fila seleccionada en los campos del formulario
            txtNombre.Text = Server.HtmlDecode(row.Cells(2).Text)
            txtApellidos.Text = Server.HtmlDecode(row.Cells(3).Text)
            txtEmail.Text = Server.HtmlDecode(row.Cells(4).Text)
            txtTelefono.Text = Server.HtmlDecode(row.Cells(5).Text)
        End If
    End Sub

    ' Evento que se ejecuta al eliminar una fila del GridView
    Protected Sub GridView1_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles GridView1.RowDeleting
        ' El SqlDataSource1 realiza el borrado automáticamente
        LblMensaje.Text = "Cliente eliminado correctamente."
        LimpiarFormulario()
    End Sub

    ' Valida que los campos obligatorios estén completos y que el email tenga formato válido
    Protected Function ValidarFormulario() As Boolean
        If String.IsNullOrWhiteSpace(txtNombre.Text) OrElse
           String.IsNullOrWhiteSpace(txtApellidos.Text) OrElse
           String.IsNullOrWhiteSpace(txtEmail.Text) OrElse
           String.IsNullOrWhiteSpace(txtTelefono.Text) Then
            LblMensaje.Text = "Todos los campos son obligatorios."
            Return False
        End If
        Dim emailRegex As New Text.RegularExpressions.Regex("^[^@\s]+@[^@\s]+\.[^@\s]+$")
        If Not emailRegex.IsMatch(txtEmail.Text) Then
            LblMensaje.Text = "Formato de email inválido."
            Return False
        End If
        Return True
    End Function

End Class