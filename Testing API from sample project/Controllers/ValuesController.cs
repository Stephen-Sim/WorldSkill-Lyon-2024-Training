using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Asp.net_core_web_api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {


        public ValuesController()
        {
            using (HttpClient client  = new HttpClient())
            {

            }       
        }
    }
}
