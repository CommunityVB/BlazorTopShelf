Imports System
Imports Microsoft.EntityFrameworkCore.Migrations
Imports Microsoft.VisualBasic

Namespace Global.Persistence.Migrations
    ''' <inheritdoc />
    Partial Public Class _001
        Inherits Migration

        ''' <inheritdoc />
        Protected Overrides Sub Up(migrationBuilder As MigrationBuilder)
            migrationBuilder.CreateTable(
                name:="AspNetRoles",
                columns:=Function(table) New With {
                    .Id = table.Column(Of Integer)(type:="INTEGER", nullable:=False).
                        Annotation("Sqlite:Autoincrement", True),
                    .Name = table.Column(Of String)(type:="TEXT COLLATE NOCASE", maxLength:=256, nullable:=True),
                    .NormalizedName = table.Column(Of String)(type:="TEXT COLLATE NOCASE", maxLength:=256, nullable:=True),
                    .ConcurrencyStamp = table.Column(Of String)(type:="TEXT COLLATE NOCASE", nullable:=True)
                },
                constraints:=Sub(table)
                    table.PrimaryKey("PK_AspNetRoles", Function(x) x.Id)
                End Sub)

            migrationBuilder.CreateTable(
                name:="AspNetUsers",
                columns:=Function(table) New With {
                    .Id = table.Column(Of Integer)(type:="INTEGER", nullable:=False).
                        Annotation("Sqlite:Autoincrement", True),
                    .UserName = table.Column(Of String)(type:="TEXT COLLATE NOCASE", maxLength:=256, nullable:=True),
                    .NormalizedUserName = table.Column(Of String)(type:="TEXT COLLATE NOCASE", maxLength:=256, nullable:=True),
                    .Email = table.Column(Of String)(type:="TEXT COLLATE NOCASE", maxLength:=256, nullable:=True),
                    .NormalizedEmail = table.Column(Of String)(type:="TEXT COLLATE NOCASE", maxLength:=256, nullable:=True),
                    .EmailConfirmed = table.Column(Of Boolean)(type:="INTEGER", nullable:=False),
                    .PasswordHash = table.Column(Of String)(type:="TEXT COLLATE NOCASE", nullable:=True),
                    .SecurityStamp = table.Column(Of String)(type:="TEXT COLLATE NOCASE", nullable:=True),
                    .ConcurrencyStamp = table.Column(Of String)(type:="TEXT COLLATE NOCASE", nullable:=True),
                    .PhoneNumber = table.Column(Of String)(type:="TEXT COLLATE NOCASE", nullable:=True),
                    .PhoneNumberConfirmed = table.Column(Of Boolean)(type:="INTEGER", nullable:=False),
                    .TwoFactorEnabled = table.Column(Of Boolean)(type:="INTEGER", nullable:=False),
                    .LockoutEnd = table.Column(Of DateTimeOffset)(type:="TEXT", nullable:=True),
                    .LockoutEnabled = table.Column(Of Boolean)(type:="INTEGER", nullable:=False),
                    .AccessFailedCount = table.Column(Of Integer)(type:="INTEGER", nullable:=False)
                },
                constraints:=Sub(table)
                    table.PrimaryKey("PK_AspNetUsers", Function(x) x.Id)
                End Sub)

            migrationBuilder.CreateTable(
                name:="AspNetRoleClaims",
                columns:=Function(table) New With {
                    .Id = table.Column(Of Integer)(type:="INTEGER", nullable:=False).
                        Annotation("Sqlite:Autoincrement", True),
                    .RoleId = table.Column(Of Integer)(type:="INTEGER", nullable:=False),
                    .ClaimType = table.Column(Of String)(type:="TEXT COLLATE NOCASE", nullable:=True),
                    .ClaimValue = table.Column(Of String)(type:="TEXT COLLATE NOCASE", nullable:=True)
                },
                constraints:=Sub(table)
                    table.PrimaryKey("PK_AspNetRoleClaims", Function(x) x.Id)
                    table.ForeignKey(
                        name:="FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column:=Function(x) x.RoleId,
                        principalTable:="AspNetRoles",
                        principalColumn:="Id",
                        onDelete:=ReferentialAction.Cascade)
                End Sub)

            migrationBuilder.CreateTable(
                name:="AspNetUserClaims",
                columns:=Function(table) New With {
                    .Id = table.Column(Of Integer)(type:="INTEGER", nullable:=False).
                        Annotation("Sqlite:Autoincrement", True),
                    .UserId = table.Column(Of Integer)(type:="INTEGER", nullable:=False),
                    .ClaimType = table.Column(Of String)(type:="TEXT COLLATE NOCASE", nullable:=True),
                    .ClaimValue = table.Column(Of String)(type:="TEXT COLLATE NOCASE", nullable:=True)
                },
                constraints:=Sub(table)
                    table.PrimaryKey("PK_AspNetUserClaims", Function(x) x.Id)
                    table.ForeignKey(
                        name:="FK_AspNetUserClaims_AspNetUsers_UserId",
                        column:=Function(x) x.UserId,
                        principalTable:="AspNetUsers",
                        principalColumn:="Id",
                        onDelete:=ReferentialAction.Cascade)
                End Sub)

            migrationBuilder.CreateTable(
                name:="AspNetUserLogins",
                columns:=Function(table) New With {
                    .LoginProvider = table.Column(Of String)(type:="TEXT COLLATE NOCASE", nullable:=False),
                    .ProviderKey = table.Column(Of String)(type:="TEXT COLLATE NOCASE", nullable:=False),
                    .ProviderDisplayName = table.Column(Of String)(type:="TEXT COLLATE NOCASE", nullable:=True),
                    .UserId = table.Column(Of Integer)(type:="INTEGER", nullable:=False)
                },
                constraints:=Sub(table)
                    table.PrimaryKey("PK_AspNetUserLogins", Function(x) New With {x.LoginProvider, x.ProviderKey})
                    table.ForeignKey(
                        name:="FK_AspNetUserLogins_AspNetUsers_UserId",
                        column:=Function(x) x.UserId,
                        principalTable:="AspNetUsers",
                        principalColumn:="Id",
                        onDelete:=ReferentialAction.Cascade)
                End Sub)

            migrationBuilder.CreateTable(
                name:="AspNetUserRoles",
                columns:=Function(table) New With {
                    .UserId = table.Column(Of Integer)(type:="INTEGER", nullable:=False),
                    .RoleId = table.Column(Of Integer)(type:="INTEGER", nullable:=False)
                },
                constraints:=Sub(table)
                    table.PrimaryKey("PK_AspNetUserRoles", Function(x) New With {x.UserId, x.RoleId})
                    table.ForeignKey(
                        name:="FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column:=Function(x) x.RoleId,
                        principalTable:="AspNetRoles",
                        principalColumn:="Id",
                        onDelete:=ReferentialAction.Cascade)
                    table.ForeignKey(
                        name:="FK_AspNetUserRoles_AspNetUsers_UserId",
                        column:=Function(x) x.UserId,
                        principalTable:="AspNetUsers",
                        principalColumn:="Id",
                        onDelete:=ReferentialAction.Cascade)
                End Sub)

            migrationBuilder.CreateTable(
                name:="AspNetUserTokens",
                columns:=Function(table) New With {
                    .UserId = table.Column(Of Integer)(type:="INTEGER", nullable:=False),
                    .LoginProvider = table.Column(Of String)(type:="TEXT COLLATE NOCASE", nullable:=False),
                    .Name = table.Column(Of String)(type:="TEXT COLLATE NOCASE", nullable:=False),
                    .Value = table.Column(Of String)(type:="TEXT COLLATE NOCASE", nullable:=True)
                },
                constraints:=Sub(table)
                    table.PrimaryKey("PK_AspNetUserTokens", Function(x) New With {x.UserId, x.LoginProvider, x.Name})
                    table.ForeignKey(
                        name:="FK_AspNetUserTokens_AspNetUsers_UserId",
                        column:=Function(x) x.UserId,
                        principalTable:="AspNetUsers",
                        principalColumn:="Id",
                        onDelete:=ReferentialAction.Cascade)
                End Sub)

            migrationBuilder.CreateIndex(
                name:="IX_AspNetRoleClaims_RoleId",
                table:="AspNetRoleClaims",
                column:="RoleId")

            migrationBuilder.CreateIndex(
                name:="RoleNameIndex",
                table:="AspNetRoles",
                column:="NormalizedName",
                unique:=True)

            migrationBuilder.CreateIndex(
                name:="IX_AspNetUserClaims_UserId",
                table:="AspNetUserClaims",
                column:="UserId")

            migrationBuilder.CreateIndex(
                name:="IX_AspNetUserLogins_UserId",
                table:="AspNetUserLogins",
                column:="UserId")

            migrationBuilder.CreateIndex(
                name:="IX_AspNetUserRoles_RoleId",
                table:="AspNetUserRoles",
                column:="RoleId")

            migrationBuilder.CreateIndex(
                name:="EmailIndex",
                table:="AspNetUsers",
                column:="NormalizedEmail")

            migrationBuilder.CreateIndex(
                name:="UserNameIndex",
                table:="AspNetUsers",
                column:="NormalizedUserName",
                unique:=True)
        End Sub

        ''' <inheritdoc />
        Protected Overrides Sub Down(migrationBuilder As MigrationBuilder)
            migrationBuilder.DropTable(
                name:="AspNetRoleClaims")

            migrationBuilder.DropTable(
                name:="AspNetUserClaims")

            migrationBuilder.DropTable(
                name:="AspNetUserLogins")

            migrationBuilder.DropTable(
                name:="AspNetUserRoles")

            migrationBuilder.DropTable(
                name:="AspNetUserTokens")

            migrationBuilder.DropTable(
                name:="AspNetRoles")

            migrationBuilder.DropTable(
                name:="AspNetUsers")
        End Sub
    End Class
End Namespace
