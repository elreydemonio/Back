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
        string ConectionString = "Server=DESKTOP-DER5DC8\\SQLEXPRESS;Database=NgZorroMerakiF3;Trusted_Connection=True;";
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
        public async Task<int> ValidarSuspencion(string id)
        {

        } 
    }
}
