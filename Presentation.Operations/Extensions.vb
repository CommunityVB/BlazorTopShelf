Imports System.Runtime.CompilerServices
Imports Microsoft.AspNetCore.Builder
Imports Microsoft.AspNetCore.Routing
Imports Microsoft.EntityFrameworkCore
Imports Microsoft.Extensions.DependencyInjection
Imports Persistence

Public Module Extensions
  <Extension>
  Public Function MapAdditionalIdentityEndpoints(Instance As IEndpointRouteBuilder) As IEndpointConventionBuilder
    '
    ' Note: The delegates are necessary because VB.NET doesn't support attributes
    ' on lambda parameters. See Handlers.vb for the attributes in question.
    '
    Dim oDownloadPersonalDataHandler As Delegates.DownloadPersonalDataDelegate
    Dim oLinkExternalLoginHandler As Delegates.LinkExternalLoginDelegate
    Dim oExternalLoginHandler As Delegates.PerformExternalLoginDelegate
    Dim oLogoutHandler As Delegates.LogoutDelegate
    Dim oAccountGroup As RouteGroupBuilder
    Dim oManageGroup As RouteGroupBuilder
    Dim oHandlers As Handlers

    oHandlers = New Handlers

    oDownloadPersonalDataHandler = AddressOf oHandlers.DownloadPersonalData
    oLinkExternalLoginHandler = AddressOf oHandlers.LinkExternalLogin
    oExternalLoginHandler = AddressOf oHandlers.PerformExternalLogin
    oLogoutHandler = AddressOf oHandlers.LogoutAsync

    oAccountGroup = Instance.MapGroup("/Account")
    oAccountGroup.MapPost("/PerformExternalLogin", oExternalLoginHandler)
    oAccountGroup.MapPost("/Logout", oLogoutHandler)

    oManageGroup = oAccountGroup.MapGroup("/Manage").RequireAuthorization
    oManageGroup.MapPost("/DownloadPersonalData", oDownloadPersonalDataHandler)
    oManageGroup.MapPost("/LinkExternalLogin", oLinkExternalLoginHandler)

    Return oAccountGroup
  End Function



  <Extension>
  Public Sub SeedData(Of T As Context)(Instance As WebApplication)
    Using oScope As IServiceScope = Instance.Services.CreateScope
      Seeder.Seed(oScope.ServiceProvider.GetRequiredService(Of T))
    End Using
  End Sub



  <Extension>
  Public Sub ApplyMigrations(Of T As DbContext)(Instance As WebApplication)
    Using oScope As IServiceScope = Instance.Services.CreateScope
      oScope.ServiceProvider.GetRequiredService(Of T).Database.Migrate
    End Using
  End Sub
End Module
