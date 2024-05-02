Imports System
Imports Microsoft.EntityFrameworkCore.Migrations
Imports Microsoft.VisualBasic

Namespace Global.Persistence.Migrations
    ''' <inheritdoc />
    Partial Public Class _002
        Inherits Migration

        ''' <inheritdoc />
        Protected Overrides Sub Up(migrationBuilder As MigrationBuilder)
            migrationBuilder.CreateTable(
                name:="Cities",
                columns:=Function(table) New With {
                    .Id = table.Column(Of Integer)(type:="INTEGER", nullable:=False).
                        Annotation("Sqlite:Autoincrement", True),
                    .Code = table.Column(Of String)(type:="TEXT COLLATE NOCASE", maxLength:=7, nullable:=False, defaultValue:=""),
                    .Name = table.Column(Of String)(type:="TEXT COLLATE NOCASE", maxLength:=25, nullable:=False, defaultValue:=""),
                    .Key = table.Column(Of Guid)(type:="TEXT", nullable:=False)
                },
                constraints:=Sub(table)
                    table.PrimaryKey("PK_Cities", Function(x) x.Id)
                End Sub)

            migrationBuilder.CreateIndex(
                name:="IX_Cities_Code",
                table:="Cities",
                column:="Code")

            migrationBuilder.CreateIndex(
                name:="IX_Cities_Name",
                table:="Cities",
                column:="Name")
        End Sub

        ''' <inheritdoc />
        Protected Overrides Sub Down(migrationBuilder As MigrationBuilder)
            migrationBuilder.DropTable(
                name:="Cities")
        End Sub
    End Class
End Namespace
