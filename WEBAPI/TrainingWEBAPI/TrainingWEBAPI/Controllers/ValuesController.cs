using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using TrainingWEBAPI.Models;

namespace TrainingWEBAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public SgDbContext ent { get; set; }

        public ValuesController()
        {
            ent = new SgDbContext();    
        }

        [HttpGet]
        public IActionResult Get(int quizId, bool isExpertOnly)
        {
            var result = ent.Attempts.ToList().GroupBy(x => new
            {
                x.User,
                DateTime = DateTime.ParseExact(x.DateTime, "d/M/yyyy H:mm", null) 
            }).Select(x => new
            {
                Name = x.Key.User.Name + $" ({x.Key.User.Role})",
                Attempt = new Func<int>(() =>
                {
                    return ent.Attempts.Include(y => y.User).ToList().Where(y => y.User.UserId == x.Key.User.).GroupBy(y => new
                    {
                        y.User,
                        DateTime = DateTime.ParseExact(y.DateTime, "d/M/yyyy H:mm", null)
                    }).ToList().Where(y => y.Key.DateTime <= x.Key.DateTime).Count() + 1;
                })()
            });
            
            return Ok(result);
        }
    }
}
