using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace NgZorroBack.Models.Join
{
    public class ValidarUsuario
    {
        string ConectionString = "Server=DESKTOP-EMH23CH;Database=NgZorroMerakiF4;Trusted_Connection=True;";
        private IDbConnection Connection
        {
            get
            {
                return new SqlConnection(ConectionString);
            }
        }
        public async Task<int> Validar(string id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = @"select COUNT(*) from InfoConductores I 
				                join UsuariosIdentity U On I.IdConductor = U.Id
				                where I.CodigoV = @id and U.IdEstado = 1";
                dbConnection.Open();
                return await dbConnection.QueryFirstAsync<int>(sQuery, new { Id = id });
            }
        }
        public async Task<object> DetalleUsuario(string id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = @"select u.Id, u.Apellido, U.Nombre, U.Celular, U.UserName, U.NumeroDocumento, U.Email, U.Direccion,
                                    R.NombreRol, T.NombreDoocumento, G.NombreGenero, E.IdEstadoUsuario, E.EstadoNombre
                                    from UsuariosIdentity u
                                    inner join Roles R On U.IdRol = R.IdRol
                                    inner join TipoDocumentos T On U.IdTipoDocumento = T.IdTipoDocumento
                                    inner join Generos G On U.IdGenero = G.IdGenero
                                    inner join EstadoUsuarios E On U.IdEstado = E.IdEstadoUsuario
                                    where U.Id = @Id";
                var okQuery = await dbConnection.QueryAsync<object>(sQuery, new { Id = id });
                return okQuery;
            }
        }
        public async Task<Object> DetalleUsuarioConductor(string id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = @"select u.Id, u.Apellido, U.Nombre, U.Celular, U.UserName, U.NumeroDocumento, U.Email, U.Direccion,
                                    R.NombreRol, T.NombreDoocumento, G.NombreGenero, E.IdEstadoUsuario, E.EstadoNombre,
									I.FotoConductor, I.FechaInicio, I.FechaFin
                                    from UsuariosIdentity u
                                    inner join Roles R On U.IdRol = R.IdRol
                                    inner join TipoDocumentos T On U.IdTipoDocumento = T.IdTipoDocumento
                                    inner join Generos G On U.IdGenero = G.IdGenero
                                    inner join EstadoUsuarios E On U.IdEstado = E.IdEstadoUsuario
									inner join InfoConductores I On U.Id = I.IdConductor
                                    where U.Id = @Id";
                dbConnection.Open();
                var okQuery = await dbConnection.QueryAsync<object>(sQuery, new { Id = id });
                return okQuery;
            }
        }
    }
}
