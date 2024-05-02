Imports System.Linq
Imports Persistence.Models

Public Class Seeder
  Public Shared Sub Seed(Context As Context)
    If Not Context.Cities.Any Then
      Dim oChicago As City
      Dim oNewYork As City

      oChicago = New City With {
        .Code = "H236TDM",
        .Name = "Chicago"
      }

      oNewYork = New City With {
        .Code = "BD7X3W8",
        .Name = "New York"
      }

      Context.Cities.AddRange(oChicago, oNewYork)
      Context.SaveChanges()
    End If
  End Sub
End Class
