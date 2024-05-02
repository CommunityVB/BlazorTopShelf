Imports Microsoft.EntityFrameworkCore
Imports Microsoft.EntityFrameworkCore.Metadata.Builders
Imports Persistence.Models

Namespace Configurations
  Public Class CityConfiguration
    Implements IEntityTypeConfiguration(Of City)

    Public Sub Configure(Builder As EntityTypeBuilder(Of City)) Implements IEntityTypeConfiguration(Of City).Configure
      Builder.Property(Function(Contact) Contact.Name).IsRequired.HasDefaultValue(String.Empty).HasMaxLength(25)
      Builder.Property(Function(Contact) Contact.Code).IsRequired.HasDefaultValue(String.Empty).HasMaxLength(7)

      Builder.HasIndex(Function(Contact) Contact.Name)
      Builder.HasIndex(Function(Contact) Contact.Code)
    End Sub
  End Class
End Namespace
