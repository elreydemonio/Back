using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NgZorroBack.Migrations
{
    public partial class Usuarios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Colores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EstadoServicios",
                columns: table => new
                {
                    IdEstadoServicio = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadoServicios", x => x.IdEstadoServicio);
                });

            migrationBuilder.CreateTable(
                name: "EstadoUsuarios",
                columns: table => new
                {
                    IdEstadoUsuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadoUsuarios", x => x.IdEstadoUsuario);
                });

            migrationBuilder.CreateTable(
                name: "EstadoVehiculos",
                columns: table => new
                {
                    IdEstadoVehiculo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadoVehiculos", x => x.IdEstadoVehiculo);
                });

            migrationBuilder.CreateTable(
                name: "Generos",
                columns: table => new
                {
                    IdGenero = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Generos", x => x.IdGenero);
                });

            migrationBuilder.CreateTable(
                name: "Marcas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marcas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    IdRol = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.IdRol);
                });

            migrationBuilder.CreateTable(
                name: "TipoCargas",
                columns: table => new
                {
                    IdTipoCarga = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoCargas", x => x.IdTipoCarga);
                });

            migrationBuilder.CreateTable(
                name: "TipoDocumentos",
                columns: table => new
                {
                    IdTipoDocumento = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoDocumentos", x => x.IdTipoDocumento);
                });

            migrationBuilder.CreateTable(
                name: "TipoVehiculos",
                columns: table => new
                {
                    IdTipoVehiculo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoVehiculos", x => x.IdTipoVehiculo);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdRol = table.Column<int>(type: "int", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Apellido = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdEstado = table.Column<int>(type: "int", nullable: true),
                    IdTipoDocumento = table.Column<int>(type: "int", nullable: true),
                    IdGenero = table.Column<int>(type: "int", nullable: true),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumeroDocumento = table.Column<int>(type: "int", nullable: true),
                    Celular = table.Column<int>(type: "int", nullable: true),
                    IdEstadoNavigationIdEstadoUsuario = table.Column<int>(type: "int", nullable: true),
                    IdGeneroNavigationIdGenero = table.Column<int>(type: "int", nullable: true),
                    IdRolNavigationIdRol = table.Column<int>(type: "int", nullable: true),
                    IdTipoDocumentoNavigationIdTipoDocumento = table.Column<int>(type: "int", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_EstadoUsuarios_IdEstadoNavigationIdEstadoUsuario",
                        column: x => x.IdEstadoNavigationIdEstadoUsuario,
                        principalTable: "EstadoUsuarios",
                        principalColumn: "IdEstadoUsuario",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Generos_IdGeneroNavigationIdGenero",
                        column: x => x.IdGeneroNavigationIdGenero,
                        principalTable: "Generos",
                        principalColumn: "IdGenero",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Roles_IdRolNavigationIdRol",
                        column: x => x.IdRolNavigationIdRol,
                        principalTable: "Roles",
                        principalColumn: "IdRol",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_TipoDocumentos_IdTipoDocumentoNavigationIdTipoDocumento",
                        column: x => x.IdTipoDocumentoNavigationIdTipoDocumento,
                        principalTable: "TipoDocumentos",
                        principalColumn: "IdTipoDocumento",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Vehiculos",
                columns: table => new
                {
                    CodigoV = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdMarca = table.Column<int>(type: "int", nullable: false),
                    Modelo = table.Column<int>(type: "int", nullable: false),
                    IdColor = table.Column<int>(type: "int", nullable: false),
                    Cilindraje = table.Column<int>(type: "int", nullable: false),
                    Soat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TecnoMecanica = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Placa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FotoV = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SeguroCarga = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdPropietario = table.Column<int>(type: "int", nullable: true),
                    IdTipoVehiculo = table.Column<int>(type: "int", nullable: true),
                    IdEstadoVehiculo = table.Column<int>(type: "int", nullable: true),
                    IdColorNavigationId = table.Column<int>(type: "int", nullable: true),
                    IdEstadoVehiculoNavigationIdEstadoVehiculo = table.Column<int>(type: "int", nullable: true),
                    IdMarcaNavigationId = table.Column<int>(type: "int", nullable: true),
                    IdTipoVehiculoNavigationIdTipoVehiculo = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehiculos", x => x.CodigoV);
                    table.ForeignKey(
                        name: "FK_Vehiculos_Colores_IdColorNavigationId",
                        column: x => x.IdColorNavigationId,
                        principalTable: "Colores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vehiculos_EstadoVehiculos_IdEstadoVehiculoNavigationIdEstadoVehiculo",
                        column: x => x.IdEstadoVehiculoNavigationIdEstadoVehiculo,
                        principalTable: "EstadoVehiculos",
                        principalColumn: "IdEstadoVehiculo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vehiculos_Marcas_IdMarcaNavigationId",
                        column: x => x.IdMarcaNavigationId,
                        principalTable: "Marcas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vehiculos_TipoVehiculos_IdTipoVehiculoNavigationIdTipoVehiculo",
                        column: x => x.IdTipoVehiculoNavigationIdTipoVehiculo,
                        principalTable: "TipoVehiculos",
                        principalColumn: "IdTipoVehiculo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InfoConductores",
                columns: table => new
                {
                    IdInfo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FotoConductor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdConductor = table.Column<int>(type: "int", nullable: true),
                    CodigoV = table.Column<int>(type: "int", nullable: false),
                    CodigoVNavigationCodigoV = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InfoConductores", x => x.IdInfo);
                    table.ForeignKey(
                        name: "FK_InfoConductores_Vehiculos_CodigoVNavigationCodigoV",
                        column: x => x.CodigoVNavigationCodigoV,
                        principalTable: "Vehiculos",
                        principalColumn: "CodigoV",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Servicios",
                columns: table => new
                {
                    IdServicio = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCliente = table.Column<int>(type: "int", nullable: false),
                    IdTipoCarga = table.Column<int>(type: "int", nullable: false),
                    DireccionCarga = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DireccionEntrega = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PersonaRecibe = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CelularRecibe = table.Column<int>(type: "int", nullable: false),
                    IdConductor = table.Column<int>(type: "int", nullable: false),
                    PrecioServicio = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IdEstadoServicio = table.Column<int>(type: "int", nullable: false),
                    IdConductorNavigationIdInfo = table.Column<int>(type: "int", nullable: true),
                    IdEstadoServicioNavigationIdEstadoServicio = table.Column<int>(type: "int", nullable: true),
                    IdTipoCargaNavigationIdTipoCarga = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servicios", x => x.IdServicio);
                    table.ForeignKey(
                        name: "FK_Servicios_EstadoServicios_IdEstadoServicioNavigationIdEstadoServicio",
                        column: x => x.IdEstadoServicioNavigationIdEstadoServicio,
                        principalTable: "EstadoServicios",
                        principalColumn: "IdEstadoServicio",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Servicios_InfoConductores_IdConductorNavigationIdInfo",
                        column: x => x.IdConductorNavigationIdInfo,
                        principalTable: "InfoConductores",
                        principalColumn: "IdInfo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Servicios_TipoCargas_IdTipoCargaNavigationIdTipoCarga",
                        column: x => x.IdTipoCargaNavigationIdTipoCarga,
                        principalTable: "TipoCargas",
                        principalColumn: "IdTipoCarga",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_IdEstadoNavigationIdEstadoUsuario",
                table: "AspNetUsers",
                column: "IdEstadoNavigationIdEstadoUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_IdGeneroNavigationIdGenero",
                table: "AspNetUsers",
                column: "IdGeneroNavigationIdGenero");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_IdRolNavigationIdRol",
                table: "AspNetUsers",
                column: "IdRolNavigationIdRol");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_IdTipoDocumentoNavigationIdTipoDocumento",
                table: "AspNetUsers",
                column: "IdTipoDocumentoNavigationIdTipoDocumento");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_InfoConductores_CodigoVNavigationCodigoV",
                table: "InfoConductores",
                column: "CodigoVNavigationCodigoV");

            migrationBuilder.CreateIndex(
                name: "IX_Servicios_IdConductorNavigationIdInfo",
                table: "Servicios",
                column: "IdConductorNavigationIdInfo");

            migrationBuilder.CreateIndex(
                name: "IX_Servicios_IdEstadoServicioNavigationIdEstadoServicio",
                table: "Servicios",
                column: "IdEstadoServicioNavigationIdEstadoServicio");

            migrationBuilder.CreateIndex(
                name: "IX_Servicios_IdTipoCargaNavigationIdTipoCarga",
                table: "Servicios",
                column: "IdTipoCargaNavigationIdTipoCarga");

            migrationBuilder.CreateIndex(
                name: "IX_Vehiculos_IdColorNavigationId",
                table: "Vehiculos",
                column: "IdColorNavigationId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehiculos_IdEstadoVehiculoNavigationIdEstadoVehiculo",
                table: "Vehiculos",
                column: "IdEstadoVehiculoNavigationIdEstadoVehiculo");

            migrationBuilder.CreateIndex(
                name: "IX_Vehiculos_IdMarcaNavigationId",
                table: "Vehiculos",
                column: "IdMarcaNavigationId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehiculos_IdTipoVehiculoNavigationIdTipoVehiculo",
                table: "Vehiculos",
                column: "IdTipoVehiculoNavigationIdTipoVehiculo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Servicios");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "EstadoServicios");

            migrationBuilder.DropTable(
                name: "InfoConductores");

            migrationBuilder.DropTable(
                name: "TipoCargas");

            migrationBuilder.DropTable(
                name: "EstadoUsuarios");

            migrationBuilder.DropTable(
                name: "Generos");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "TipoDocumentos");

            migrationBuilder.DropTable(
                name: "Vehiculos");

            migrationBuilder.DropTable(
                name: "Colores");

            migrationBuilder.DropTable(
                name: "EstadoVehiculos");

            migrationBuilder.DropTable(
                name: "Marcas");

            migrationBuilder.DropTable(
                name: "TipoVehiculos");
        }
    }
}
