Imports Microsoft.AspNetCore.Identity

Namespace Models
  ''' <summary>
  ''' </summary>
  ''' <seealso cref="IdentityUser(Of Integer)" />
  ''' <remarks>
  ''' Add profile data for application users by adding properties to the User class
  ''' </remarks>
  Public Class User
    Inherits IdentityUser(Of Integer)
  End Class
End Namespace
