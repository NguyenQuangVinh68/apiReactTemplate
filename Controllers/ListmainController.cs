using apiReact.Context;
using apiReact.Services;
using Microsoft.AspNetCore.Mvc;

namespace apiReact.Controllers
{

    [Route("[controller]")]
    public class ListmainController : Controller
    {
        public IListmainService _listmain;

        public ListmainController(IListmainService listmain)
        {
            _listmain = listmain;
        }


        [HttpGet("{pgmno}")]
        public async Task<IActionResult> Index( string pgmno)
        {
            try
            {
                var result = await _listmain.GetTable(pgmno);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
