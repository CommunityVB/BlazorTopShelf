Imports System
Imports System.Security.Claims
Imports System.Threading.Tasks
Imports Intexx
Imports Microsoft.AspNetCore.Components.Authorization
Imports Microsoft.AspNetCore.Components.Server
Imports Microsoft.AspNetCore.Identity
Imports Microsoft.Extensions.DependencyInjection
Imports Microsoft.Extensions.Logging
Imports Microsoft.Extensions.Options
Imports Persistence.Models

Namespace Account
  ' This is a server-side AuthenticationStateProvider that revalidates the security stamp for the connected user
  ' every 30 minutes an interactive circuit is connected.
  Public NotInheritable Class IdentityRevalidatingAuthenticationStateProvider
    Inherits RevalidatingServerAuthenticationStateProvider

    Public Sub New(LoggerFactory As ILoggerFactory, ScopeFactory As IServiceScopeFactory, Options As IOptions(Of IdentityOptions))
      MyBase.New(LoggerFactory)

      Me.ScopeFactory = ScopeFactory
      Me.Options = Options
    End Sub



    Protected Overrides ReadOnly Property RevalidationInterval As TimeSpan
      Get
        Return TimeSpan.FromMinutes(30)
      End Get
    End Property



    Protected Overrides Async Function ValidateAuthenticationStateAsync(authenticationState As AuthenticationState, cancellationToken As Threading.CancellationToken) As Task(Of Boolean)
      Dim oUserManager As UserManager(Of User)

      ' Get the user manager from a new scope to ensure it fetches fresh data
      oUserManager = Me.ScopeFactory.
        CreateAsyncScope.
        ServiceProvider.
        GetRequiredService(Of UserManager(Of User))

      Return Await Me.ValidateSecurityStampAsync(oUserManager, authenticationState.User)
    End Function



    Private Async Function ValidateSecurityStampAsync(UserManager As UserManager(Of User), Principal As ClaimsPrincipal) As Task(Of Boolean)
      Dim sPrincipalStamp As String
      Dim sUserStamp As String
      Dim lReturn As Boolean
      Dim oUser As User

      oUser = Await UserManager.GetUserAsync(Principal)

      If oUser.IsNothing Then
        lReturn = False

      ElseIf Not UserManager.SupportsUserSecurityStamp Then
        lReturn = True

      Else
        sPrincipalStamp = Principal.FindFirstValue(Me.Options.Value.ClaimsIdentity.SecurityStampClaimType)
        sUserStamp = Await UserManager.GetSecurityStampAsync(oUser)

        lReturn = Equals(sPrincipalStamp, sUserStamp)
      End If

      Return lReturn
    End Function



    Private ReadOnly ScopeFactory As IServiceScopeFactory
    Private ReadOnly Options As IOptions(Of IdentityOptions)
  End Class
End Namespace
