Imports System
Imports System.Collections.Generic
Imports System.Diagnostics.CodeAnalysis
Imports Microsoft.AspNetCore.Components
Imports Microsoft.AspNetCore.Http

Namespace Account
  Public NotInheritable Class IdentityRedirectManager
    Public Sub New(NavigationManager As NavigationManager)
      Me.NavigationManager = NavigationManager
    End Sub



    <DoesNotReturn>
    Public Sub RedirectTo(Uri As String)
      Uri = If(Uri, "")


      ' Prevent open redirects.
      If Not System.Uri.IsWellFormedUriString(Uri, UriKind.Relative) Then
        Uri = Me.NavigationManager.ToBaseRelativePath(Uri)
      End If

      ' During static rendering, NavigateTo throws a NavigationException which is handled by the framework as a redirect.
      ' So as long as this is called from a statically rendered Identity component, the InvalidOperationException is never thrown.
      Me.NavigationManager.NavigateTo(Uri)

      Throw New InvalidOperationException($"{NameOf(IdentityRedirectManager)} can only be used during static rendering.")
    End Sub



    <DoesNotReturn>
    Public Sub RedirectTo(Url As String, QueryParameters As Dictionary(Of String, Object))
      Dim sUrlWithoutQuery As String
      Dim sNewUrl As String

      sUrlWithoutQuery = Me.NavigationManager.ToAbsoluteUri(Url).GetLeftPart(UriPartial.Path)
      sNewUrl = Me.NavigationManager.GetUriWithQueryParameters(sUrlWithoutQuery, QueryParameters)

      Me.RedirectTo(sNewUrl)
    End Sub



    <DoesNotReturn>
    Public Sub RedirectToWithStatus(Url As String, Message As String, Context As HttpContext)
      Context.Response.Cookies.Append(StatusCookieName, Message, StatusCookieBuilder.Build(Context))

      Me.RedirectTo(Url)
    End Sub



    Private ReadOnly Property CurrentPath As String
      Get
        Return Me.NavigationManager.ToAbsoluteUri(Me.NavigationManager.Uri).GetLeftPart(UriPartial.Path)
      End Get
    End Property



    <DoesNotReturn>
    Public Sub RedirectToCurrentPage()
      Me.RedirectTo(Me.CurrentPath)
    End Sub



    <DoesNotReturn>
    Public Sub RedirectToCurrentPageWithStatus(message As String, context As HttpContext)
      Me.RedirectToWithStatus(Me.CurrentPath, message, context)
    End Sub




    Public Const StatusCookieName As String = "Identity.StatusMessage"

    Private ReadOnly NavigationManager As NavigationManager

    Private Shared ReadOnly StatusCookieBuilder As New CookieBuilder With {
      .IsEssential = True,
      .SameSite = SameSiteMode.Strict,
      .HttpOnly = True,
      .MaxAge = TimeSpan.FromSeconds(5)
    }
  End Class
End Namespace
