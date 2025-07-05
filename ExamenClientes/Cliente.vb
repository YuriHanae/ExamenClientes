Public Class Cliente
    Public Property ClienteId As Integer
    Public Property Nombre As String
    Public Property Apellidos As String
    Public Property Email As String
    Public Property Telefono As String

    Public Sub New(clienteId As Integer, nombre As String, apellidos As String, email As String, telefono As String)
        Me.ClienteId = clienteId
        Me.Nombre = nombre
        Me.Apellidos = apellidos
        Me.Email = email
        Me.Telefono = telefono
    End Sub
End Class
