Imports System.Threading.Tasks
Imports Microsoft.AspNetCore.SignalR

Namespace SignalR
  Public Class ChatHub
    Inherits Hub

    Public Async Function SendMessage(User As String, Message As String) As Task
      Await Me.Clients.All.SendAsync("ReceiveMessage", User, Message)
    End Function
  End Class
End Namespace
