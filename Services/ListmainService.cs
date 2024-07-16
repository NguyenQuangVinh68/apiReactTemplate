using apiReact.Context;
using apiReact.Models;
using Azure.Core;
using Dapper;
using Newtonsoft.Json.Linq;
using System.Data;

namespace apiReact.Services
{
    public interface IListmainService
    {
        public Task<Object> GetTable(string pgm_no);
    }
    public class ListmainService : IListmainService
    {

        private readonly DapperContext _context;

        public ListmainService(DapperContext context)
        {
            _context = context;
        }
        public async Task<Object> GetTable(string pgm_no)
        {
            try
            {
                string strsql = "SYS_GETDATA_WITH_API";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@ACTION", "GetDataListMain");
                parameters.Add("@PGM_NO", pgm_no);

                using (IDbConnection conn = _context.CreateConnect())
                {
                    var result = await conn.QueryAsync(strsql, parameters, commandType: CommandType.StoredProcedure);
                    if (result == null)
                        return null;

                    return result.ToList();
                    //return new AuthenResponse(result, token);
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
