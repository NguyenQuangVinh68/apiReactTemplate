using apiReact.Context;
using apiReact.Models;
using Dapper;
using Newtonsoft.Json.Linq;
using System.Data;

namespace apiReact.Services
{
    public interface IAuthService
    {
        public Task<JObject> Login(LoginRequest request);
    }
    public class AuthService: IAuthService
    {
        private readonly DapperContext _context;
        private readonly ITokenService _tokenUtil;

        public AuthService(DapperContext context, ITokenService tokenUtil)
        {
            _context = context;
            _tokenUtil = tokenUtil;
        }

        public async Task<JObject> Login(LoginRequest request) 
        {
            try
            {
                string strsql = "SYS_GETDATA_WITH_API";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@ACTION", "GetUserLogin");
                parameters.Add("@EMP_NO", request.emp_no);
                parameters.Add("@PASS", request.password);

                using (IDbConnection conn = _context.CreateConnect())
                {
                    var result = await conn.QueryFirstOrDefaultAsync<UserResponse>(strsql, parameters, commandType: CommandType.StoredProcedure);
                    if (result == null)
                        return null;

                    var token = _tokenUtil.CreateToken(result);
                    return new JObject {
                        ["token"] = token,
                        ["status"] = "200"
                    };
                    //return new AuthenResponse(result, token);
                }
            }
            catch (Exception ex)
            {
                return new JObject
                {
                    ["message"] = ex.Message,
                    ["status"] = "500"
                };
            }
        }
    }
}
