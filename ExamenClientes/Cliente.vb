' Clase que representa la entidad Cliente
Public Class Cliente

    ' Propiedad para el identificador único del cliente
    Public Property ClienteId As Integer

    ' Propiedad para el nombre del cliente
    Public Property Nombre As String

    ' Propiedad para los apellidos del cliente
    Public Property Apellidos As String

    ' Propiedad para el email del cliente
    Public Property Email As String

    ' Propiedad para el teléfono del cliente
    Public Property Telefono As String

    ' Constructor de la clase que inicializa todas las propiedades
    Public Sub New(clienteId As Integer, nombre As String, apellidos As String, email As String, telefono As String)
        Me.ClienteId = clienteId
        Me.Nombre = nombre
        Me.Apellidos = apellidos
        Me.Email = email
        Me.Telefono = telefono
    End Sub
End Class