Imports System.Security.Claims
Imports System.Threading.Tasks
Imports Microsoft.AspNetCore.Http
Imports Microsoft.AspNetCore.Http.HttpResults
Imports Microsoft.AspNetCore.Identity
Imports Microsoft.Extensions.Logging
Imports Persistence.Models

Friend Class Delegates
  Friend Delegate Function PerformExternalLoginDelegate(
    Context As HttpContext,
    SignInManager As SignInManager(Of User),
    Provider As String,
    ReturnUrl As String) As ChallengeHttpResult

  Friend Delegate Function LogoutDelegate(
    User As ClaimsPrincipal,
    SignInManager As SignInManager(Of User),
    ReturnUrl As String) As Task(Of RedirectHttpResult)

  Friend Delegate Function LinkExternalLoginDelegate(
    Context As HttpContext,
    SignInManager As SignInManager(Of User),
    Provider As String) As Task(Of ChallengeHttpResult)

  Friend Delegate Function DownloadPersonalDataDelegate(
    Context As HttpContext,
    UserManager As UserManager(Of User),
    Logger As ILogger) As Task(Of FileContentHttpResult)
End Class
