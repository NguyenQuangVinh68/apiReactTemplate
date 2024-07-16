using apiReact.Models;
using apiReact.Services;
using Microsoft.AspNetCore.Mvc;

namespace apiReact.Controllers
{

    [Route("[controller]")]
    public class MenuController : Controller
    {
        public IMenuService _menu;

        public MenuController(IMenuService menu)
        {
            _menu = menu;
        }

        [HttpPost]
        public async Task<IActionResult>Index ([FromBody]  string emp_no)
        {
            //return Unauthorized();
            try
            {
                var result = await _menu.getListMenu(emp_no);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
