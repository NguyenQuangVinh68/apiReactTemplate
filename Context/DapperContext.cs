using Microsoft.Data.SqlClient;
using System.Data;

namespace apiReact.Context
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionstring;

        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionstring = _configuration.GetConnectionString("ConnectionDB");
        }

        public IDbConnection CreateConnect() => new SqlConnection(_connectionstring);
    }
}
