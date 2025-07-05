Public Class FormCliente
    Inherits System.Web.UI.Page

    ' Para almacenar temporalmente el ClienteId que se está editando
    Protected Property ClienteIdEditando As Integer
        Get
            Dim val = ViewState("ClienteIdEditando")
            If val Is Nothing Then Return 0 Else Return CInt(val)
        End Get
        Set(value As Integer)
            ViewState("ClienteIdEditando") = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LimpiarFormulario()
        End If
    End Sub

    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs)
        LblMensaje.Text = ""
        If Not ValidarFormulario() Then Exit Sub

        If ClienteIdEditando = 0 Then
            ' Insertar nuevo cliente
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
                If ex.Message.Contains("UNIQUE") AndAlso ex.Message.Contains("Email") Then
                    LblMensaje.Text = "El Email ya está registrado para otro cliente."
                ElseIf ex.Message.Contains("UNIQUE") AndAlso ex.Message.Contains("Telefono") Then
                    LblMensaje.Text = "El Teléfono ya está registrado para otro cliente."
                Else
                    LblMensaje.Text = "Error al guardar: " & ex.Message
                End If
            End Try
        Else
            ' Actualizar cliente existente
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
        GridView1.DataBind()
    End Sub

    Protected Sub btnCancelar_Click(sender As Object, e As EventArgs)
        LimpiarFormulario()
        ClienteIdEditando = 0
        GridView1.SelectedIndex = -1
    End Sub

    Protected Sub LimpiarFormulario()
        txtNombre.Text = ""
        txtApellidos.Text = ""
        txtEmail.Text = ""
        txtTelefono.Text = ""
        LblMensaje.Text = ""
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView1.SelectedIndexChanged
        If GridView1.SelectedIndex >= 0 Then
            Dim row As GridViewRow = GridView1.SelectedRow
            ClienteIdEditando = Convert.ToInt32(GridView1.SelectedDataKey.Value)
            txtNombre.Text = Server.HtmlDecode(row.Cells(2).Text)
            txtApellidos.Text = Server.HtmlDecode(row.Cells(3).Text)
            txtEmail.Text = Server.HtmlDecode(row.Cells(4).Text)
            txtTelefono.Text = Server.HtmlDecode(row.Cells(5).Text)
        End If
    End Sub

    Protected Sub GridView1_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles GridView1.RowDeleting
        ' No necesitas hacer nada extra, el SqlDataSource1 borra el registro automáticamente
        LblMensaje.Text = "Cliente eliminado correctamente."
        LimpiarFormulario()
    End Sub

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