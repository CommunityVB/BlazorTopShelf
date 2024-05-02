Imports Common
Imports Microsoft.AspNetCore.Identity
Imports Microsoft.AspNetCore.Identity.EntityFrameworkCore
Imports Microsoft.EntityFrameworkCore
Imports Microsoft.EntityFrameworkCore.Metadata
Imports Persistence.Models

Public Class Context
  Inherits IdentityDbContext(Of User, IdentityRole(Of Integer), Integer)

  Public Sub New(Options As DbContextOptions(Of Context))
    MyBase.New(Options)
  End Sub



  Protected Overrides Sub OnConfiguring(Builder As DbContextOptionsBuilder)
    If Not Builder.IsConfigured Then Builder.Configure
    MyBase.OnConfiguring(Builder)
  End Sub



  Protected Overrides Sub OnModelCreating(Builder As ModelBuilder)
    Dim oEntityType As IMutableEntityType
    Dim oProperty As IMutableProperty

    For Each oEntityType In Builder.Model.GetEntityTypes
      For Each oProperty In oEntityType.GetProperties
        If oProperty.ClrType = GetType(String) Then
          ' Ignore case during queries for string columns
          oProperty.SetColumnType("TEXT COLLATE NOCASE")
        End If
      Next
    Next

    Builder.ApplyConfigurationsFromAssembly(Me.GetType.Assembly)

    MyBase.OnModelCreating(Builder)
  End Sub



  Public Property Cities As DbSet(Of City)
End Class
