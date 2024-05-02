Imports System.IO
Imports System.Runtime.CompilerServices
Imports Microsoft.EntityFrameworkCore

Public Module Extensions
  <Extension>
  Public Function Configure(Instance As DbContextOptionsBuilder)
    Dim sFolder As String
    Dim sPath As String

    sFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)
    sFolder = Path.Combine(sFolder, "BlazorTopShelf")
    sPath = Path.Combine(sFolder, "BlazorTopShelf.db")

    Directory.CreateDirectory(sFolder)

    Return Instance.
      EnableSensitiveDataLogging.
      UseLazyLoadingProxies.
      UseSqlite($"Data Source={sPath}")
  End Function
End Module
