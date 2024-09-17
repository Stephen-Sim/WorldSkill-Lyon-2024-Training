using Asp.net_core_web_api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Asp.net_core_web_api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public Wsc2022seSession5Context ent { get; set; }

        public ValuesController()
        {
            ent = new Wsc2022seSession5Context();
        }

        [HttpGet]
        public IActionResult Login(string u, string p)
        {
            var user = ent.Users.ToList().FirstOrDefault(x => x.Username == u && x.Password == p);

            if (user == null)
            {
                return Ok();
            }

            return Ok(new
            {
                user.Id,
                user.FullName,
                user.FamilyCount
            });
        }

        [HttpGet]
        public IActionResult GetServiceTypes()
        {
            var st = ent.ServiceTypes.ToList().Select(x => new
            {
                x.Id,
                x.Name,
                x.Description,
                x.IconName
            });

            return Ok(st);
        }
    }
}
