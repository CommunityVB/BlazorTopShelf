Imports System
Imports System.Linq
Imports Common
Imports Microsoft.AspNetCore.Authentication
Imports Microsoft.AspNetCore.Builder
Imports Microsoft.AspNetCore.Components.Authorization
Imports Microsoft.AspNetCore.Identity
Imports Microsoft.AspNetCore.ResponseCompression
Imports Microsoft.AspNetCore.SignalR
Imports Microsoft.Extensions.DependencyInjection
Imports Microsoft.Extensions.Hosting
Imports Persistence
Imports Persistence.Models
Imports Presentation.Operations.Account

Public Class ServiceInitializer
  Public Shared Function OnStart(Of TApp, THub As Hub)(Website As WebApplication) As Action
    Return Sub()
             ' Configure the HTTP request pipeline.
             If Website.Environment.IsDevelopment Then
               Website.UseMigrationsEndPoint
             Else
               Website.UseResponseCompression
               Website.UseExceptionHandler("/Error")
               ' The default HSTS value Is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
               Website.UseHsts
             End If

             Website.UseHttpsRedirection
             Website.ApplyMigrations(Of Context)
             Website.SeedData(Of Context)
             Website.UseStaticFiles
             Website.UseAntiforgery
             Website.MapRazorComponents(Of TApp).AddInteractiveServerRenderMode
             Website.MapHub(Of THub)("/chathub")

             ' Add additional endpoints required by the Identity /Account Razor components.
             Website.MapAdditionalIdentityEndpoints

             Website.StartAsync()
           End Sub
  End Function



  Public Shared Function OnStop(Website As WebApplication) As Action
    Return Sub() Website.StopAsync()
  End Function



  Public Shared Function CreateHostBuilder(Args As String()) As WebApplicationBuilder
    Dim oIdentityOptions As Action(Of IdentityOptions)
    Dim oAuthOptions As Action(Of AuthenticationOptions)
    Dim oBuilder As WebApplicationBuilder
    Dim oCompressionOptions As Action(Of ResponseCompressionOptions)

    oCompressionOptions = Sub(Options) Options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat({"application/octet-stream"})
    oIdentityOptions = Sub(Options) Options.SignIn.RequireConfirmedAccount = True

    oAuthOptions = Sub(Options)
                     Options.DefaultSignInScheme = IdentityConstants.ExternalScheme
                     Options.DefaultScheme = IdentityConstants.ApplicationScheme
                   End Sub

    oBuilder = WebApplication.CreateBuilder(Args)
    oBuilder.Services.AddResponseCompression(oCompressionOptions)
    oBuilder.Services.AddDatabaseDeveloperPageExceptionFilter
    oBuilder.Services.AddCascadingAuthenticationState
    oBuilder.Services.AddRazorComponents.AddInteractiveServerComponents
    oBuilder.Services.AddAuthentication(oAuthOptions).AddIdentityCookies
    oBuilder.Services.AddIdentityCore(Of User)(oIdentityOptions).
      AddEntityFrameworkStores(Of Context).
      AddDefaultTokenProviders.
      AddSignInManager
    oBuilder.Services.AddSingleton(Of IEmailSender(Of User), IdentityNoOpEmailSender)
    oBuilder.Services.AddDbContext(Of Context)(Sub(Builder) Builder.Configure)
    oBuilder.Services.AddScoped(Of AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider)
    oBuilder.Services.AddScoped(Of IdentityRedirectManager)
    oBuilder.Services.AddScoped(Of IdentityUserAccessor)

    Return oBuilder
  End Function
End Class
