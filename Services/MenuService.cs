using apiReact.Context;
using apiReact.Models;
using Dapper;
using System.Data;

namespace apiReact.Services
{
    public interface IMenuService
    {
        public Task<List<RootMenu>> getListMenu(string emp_no);
    }
    public class MenuService : IMenuService
    {
        private readonly DapperContext _context;

        public MenuService(DapperContext context)
        {
            _context = context;
        }

        public async Task<List<RootMenu>> getListMenu(string emp_no)
        {
            try
            {
                string query = "SYS_GETDATA_WITH_API";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@ACTION", "GET_MENU");
                parameters.Add("@EMP_NO", emp_no);

                using (IDbConnection conn = _context.CreateConnect())
                {
                    var menuDic = new Dictionary<string, RootMenu>();
                    var subMenuDic = new Dictionary<string, ParentMenu>();
                    var menus = await conn.QueryAsync<RootMenu, ParentMenu,ChildMenu, RootMenu>(query, (rootmenu, parentmenu,childmenu) =>
                    {
                        if (!menuDic.TryGetValue(rootmenu.pgm_no, out var upCurrent))
                        {
                            upCurrent = rootmenu;
                            menuDic.Add(upCurrent.pgm_no.ToString(), upCurrent);
                        }

                        if (!subMenuDic.TryGetValue(parentmenu.pgm_no, out var subMenuCurr))
                        {
                            subMenuCurr = parentmenu;
                            subMenuDic.Add(subMenuCurr.pgm_no, subMenuCurr);
                            upCurrent.items.Add(subMenuCurr);
                        }
                        if (childmenu != null)
                        {
                            subMenuCurr.items.Add(childmenu);
                        }
                        return upCurrent;
                    }, parameters, splitOn: "pgm_no", commandType: CommandType.StoredProcedure);

                    //var menus = await conn.QueryAsync<Menu>(query, parameters, commandType: CommandType.StoredProcedure);

                    return menus.Distinct().ToList();
                }
            }
            catch
            {
                return null;

            }

        }
    }
}
