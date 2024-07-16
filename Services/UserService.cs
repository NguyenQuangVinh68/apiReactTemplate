using apiReact.Context;
using Dapper;
using Newtonsoft.Json.Linq;
using System.Data;

namespace apiReact.Services
{
    public interface IUserService
    {
        public Task<JObject> GetByID(int id);
    }
    public class UserService : IUserService
    {
        private readonly DapperContext _context;

        public UserService(DapperContext context)
        {
            _context = context;
        }
        public async Task<JObject> GetByID(int id)
        {
            try
            {
                using (IDbConnection conn = _context.CreateConnect())
                {
                    string strSql = "UserProc";
                    conn.Open();
                    DynamicParameters param = new DynamicParameters();
                    param.Add("@Mode", "GetAll");
                    var result = await conn.QuerySingleOrDefaultAsync<JObject>(strSql, param, commandType: CommandType.StoredProcedure);
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
