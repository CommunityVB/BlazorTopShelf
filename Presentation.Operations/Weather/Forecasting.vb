Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Threading.Tasks

Namespace Weather
  Public Class Forecasting
    Public Shared Async Function GetForecasts() As Task(Of IEnumerable(Of WeatherForecast))
      Dim dStartDate As DateOnly
      Dim aSummaries As String()
      Dim oSelector As Func(Of Integer, WeatherForecast)

      ' Simulate asynchronous loading to demonstrate streaming rendering
      Await Task.Delay(500)

      dStartDate = DateOnly.FromDateTime(Date.Now)
      aSummaries = {"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"}
      oSelector = Function(Index) New WeatherForecast With {
        .Date = dStartDate.AddDays(Index),
        .TemperatureC = Random.Shared.Next(-20, 55),
        .Summary = aSummaries(Random.Shared.Next(aSummaries.Length))
      }

      Return Enumerable.Range(1, 5).Select(oSelector).ToArray
    End Function
  End Class



  Public Class WeatherForecast
    Public ReadOnly Property TemperatureF As Integer
      Get
        Return 32 + CInt(Me.TemperatureC / (5 / 9))
      End Get
    End Property



    Public Property TemperatureC As Integer
    Public Property [Date] As DateOnly
    Public Property Summary As String
  End Class
End Namespace
