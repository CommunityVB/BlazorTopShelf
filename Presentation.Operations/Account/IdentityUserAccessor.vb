Imports System.Threading.Tasks
Imports Microsoft.AspNetCore.Identity
Imports Persistence.Models

Namespace Account
  Public NotInheritable Class IdentityUserAccessor
    Public Sub New(UserManager As UserManager(Of User), RedirectManager As IdentityRedirectManager)
      Me.UserManager = UserManager
      Me.RedirectManager = RedirectManager
    End Sub



    Public Async Function GetRequiredUserAsync(context As Microsoft.AspNetCore.Http.HttpContext) As Task(Of User)
      Dim oUser As User

      oUser = Await Me.UserManager.GetUserAsync(context.User)

      If oUser Is Nothing Then
        Me.RedirectManager.RedirectToWithStatus("Account/InvalidUser", $"Error: Unable to load user with ID '{Me.UserManager.GetUserId(context.User)}'.", context)
      End If

      Return oUser
    End Function



    Private ReadOnly RedirectManager As IdentityRedirectManager
    Private ReadOnly UserManager As UserManager(Of User)
  End Class
End Namespace
