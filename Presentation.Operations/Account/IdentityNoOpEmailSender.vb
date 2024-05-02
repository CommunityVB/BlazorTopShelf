Imports System.Threading.Tasks
Imports Microsoft.AspNetCore.Identity
Imports Microsoft.AspNetCore.Identity.UI.Services
Imports Persistence.Models

Namespace Account
  Public NotInheritable Class IdentityNoOpEmailSender
    Implements IEmailSender(Of User)

    Public Function SendConfirmationLinkAsync(User As User, Email As String, ConfirmationLink As String) As Task Implements IEmailSender(Of User).SendConfirmationLinkAsync
      Return Me.EmailSender.SendEmailAsync(Email, "Confirm your email", $"Please confirm your account by <a href='{ConfirmationLink}'>clicking here</a>.")
    End Function



    Public Function SendPasswordResetLinkAsync(User As User, Email As String, ResetLink As String) As Task Implements IEmailSender(Of User).SendPasswordResetLinkAsync
      Return Me.EmailSender.SendEmailAsync(Email, "Reset your password", $"Please reset your password by <a href='{ResetLink}'>clicking here</a>.")
    End Function



    Public Function SendPasswordResetCodeAsync(User As User, Email As String, ResetCode As String) As Task Implements IEmailSender(Of User).SendPasswordResetCodeAsync
      Return Me.EmailSender.SendEmailAsync(Email, "Reset your password", $"Please reset your password using the following code: {ResetCode}")
    End Function



    Private ReadOnly EmailSender As New NoOpEmailSender
  End Class
End Namespace
