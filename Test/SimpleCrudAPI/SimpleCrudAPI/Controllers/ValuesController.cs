using SimpleCrudAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SimpleCrudAPI.Controllers
{
    public class ValuesController : ApiController
    {
        public testEntities ent { get; set; }
        public ValuesController()
        {
            ent = new testEntities();
        }

        [HttpGet]
        public object Login(string u, string p)
        {
            var user = ent.Users.ToList().FirstOrDefault(x => x.Username == u && x.Password == p);

            if (user == null)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpGet]
        public object GetUsers()
        {
            var users = ent.Users.ToList().Where(x => x.RoleID == 2).ToList();

            return Ok(users.Select(x => new
            {
                x.ID,
                x.Username,
                Age = DateTime.Now.Year - x.Birthdate.Value.Year,
                Color = x.Gender == true ? "Blue" : "Pink"
            }));
        }
    }
}
