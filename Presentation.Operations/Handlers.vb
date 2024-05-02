Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Reflection
Imports System.Security.Claims
Imports System.Text.Json
Imports System.Threading.Tasks
Imports Intexx
Imports Microsoft.AspNetCore.Authentication
Imports Microsoft.AspNetCore.Http
Imports Microsoft.AspNetCore.Http.Extensions
Imports Microsoft.AspNetCore.Http.HttpResults
Imports Microsoft.AspNetCore.Identity
Imports Microsoft.AspNetCore.Mvc
Imports Microsoft.Extensions.Logging
Imports Persistence.Models
Imports Presentation.Operations.Account.Pages
Imports Presentation.Operations.Account.Pages.Manage

Friend Class Handlers
  Friend Function PerformExternalLogin(
    Context As HttpContext,
    <FromServices> SignInManager As SignInManager(Of User),
    <FromForm> Provider As String,
    <FromForm> ReturnUrl As String) As ChallengeHttpResult

    Dim oProperties As AuthenticationProperties
    Dim sRedirectUrl As String
    Dim oQuery As IEnumerable(Of KeyValuePair(Of String, String))

    oQuery = {
      New KeyValuePair(Of String, String)("ReturnUrl", ReturnUrl),
      New KeyValuePair(Of String, String)("Action", ExternalLogin.LoginCallbackAction)
    }

    sRedirectUrl = UriHelper.BuildRelative(
            Context.Request.PathBase,
            "/Account/ExternalLogin",
            QueryString.Create(oQuery))

    oProperties = SignInManager.ConfigureExternalAuthenticationProperties(Provider, sRedirectUrl)

    Return TypedResults.Challenge(oProperties, {Provider})
  End Function



  Friend Async Function LogoutAsync(
    User As ClaimsPrincipal,
    SignInManager As SignInManager(Of User),
    <FromForm> ReturnUrl As String) As Task(Of RedirectHttpResult)

    Await SignInManager.SignOutAsync

    Return TypedResults.LocalRedirect($"~/{ReturnUrl}")
  End Function



  Friend Async Function LinkExternalLogin(
    Context As HttpContext,
    <FromServices> SignInManager As SignInManager(Of User),
    <FromForm> Provider As String) As Task(Of ChallengeHttpResult)

    Dim oQueryString As QueryString
    Dim sRedirectUrl As String
    Dim oProperties As AuthenticationProperties

    Await Context.SignOutAsync(IdentityConstants.ExternalScheme)

    oQueryString = QueryString.Create("Action", ExternalLogins.LinkLoginCallbackAction)

    sRedirectUrl = UriHelper.BuildRelative(
                    Context.Request.PathBase,
                    "/Account/Manage/ExternalLogins",
                    oQueryString)

    oProperties = SignInManager.ConfigureExternalAuthenticationProperties(
      Provider,
      sRedirectUrl,
      SignInManager.UserManager.GetUserId(Context.User)
    )

    Return TypedResults.Challenge(oProperties, {Provider})
  End Function



  Friend Async Function DownloadPersonalData(
    Context As HttpContext,
    <FromServices> UserManager As UserManager(Of User),
    <FromServices> Logger As ILogger) As Task(Of FileContentHttpResult)

    Dim oPersonalData As Dictionary(Of String, String)
    Dim aPersonalData As Byte()
    Dim oProperties As IEnumerable(Of PropertyInfo)
    Dim oProperty As PropertyInfo
    Dim oLogins As IList(Of UserLoginInfo)
    Dim oResult As FileContentHttpResult
    Dim sUserId As String
    Dim oLogin As UserLoginInfo
    Dim oUser As User

    oUser = Await UserManager.GetUserAsync(Context.User)

    If oUser.IsNothing Then
      oResult = Results.NotFound($"Unable to load user with ID '{UserManager.GetUserId(Context.User)}'.")
    Else
      sUserId = Await UserManager.GetUserIdAsync(oUser)
      oLogins = Await UserManager.GetLoginsAsync(oUser)

      Context.Response.Headers.TryAdd("Content-Disposition", "attachment; filename=PersonalData.json")
      Logger.LogInformation("User with ID '{UserId}' requested their personal data.", sUserId)

      ' Only include personal data for download
      oPersonalData = New Dictionary(Of String, String)
      oProperties = GetType(User).GetProperties.Where(Function(Info) Attribute.IsDefined(Info, GetType(PersonalDataAttribute)))

      For Each oProperty In oProperties
        oPersonalData.Add(oProperty.Name, If(oProperty.GetValue(oUser)?.ToString, "null"))
      Next

      For Each oLogin In oLogins
        oPersonalData.Add($"{oLogin.LoginProvider} external login provider key", oLogin.ProviderKey)
      Next

      oPersonalData.Add("Authenticator Key", (Await UserManager.GetAuthenticatorKeyAsync(oUser)))

      aPersonalData = JsonSerializer.SerializeToUtf8Bytes(oPersonalData)
      oResult = TypedResults.File(aPersonalData, contentType:="application/json", fileDownloadName:="PersonalData.json")
    End If

    Return oResult
  End Function
End Class
