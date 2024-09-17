using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Drawing.Imaging;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public Asc2023MobileMyContext ent { get; set; }

        public ValuesController()
        {
            ent = new Asc2023MobileMyContext();    
        }

        [HttpGet]
        public IActionResult GetCompetitors() 
        {
            var coms = ent.Competitors.ToList().Select(x => new
            {
                x.Id,
                x.Name, 
                Country = x.Country.Name,
                Skill = x.Skill.Name,
                Color = new Func<string>(() =>
                {
                    var percentage = x.Sponsorships.Sum(x => x.Amount * x.Currency.Rate) / x.RequiredAmount * 100;

                    if (percentage > 99)
                    {
                        return "Green";
                    }

                    if (percentage > 60)
                    {
                        return "Yellow";
                    }

                    if (percentage > 30)
                    {
                        return "Orange";
                    }

                    return "Red";
                })()
            });

            return Ok(coms);
        }

        [HttpGet]
        public IActionResult GetCaptcha()
        {
            var random = new Random().Next(1000, 9999);

            var bitmap = new Bitmap(200, 100);

            var graphic = Graphics.FromImage(bitmap);
            graphic.Clear(Color.White);
            graphic.DrawString(random.ToString(), new Font("Arial", 20f), Brushes.Black, 30f, 100f);

            byte[] arr = null;

            using (MemoryStream ms = new MemoryStream())
            {
                bitmap.Save(ms, ImageFormat.Png);
                arr = ms.ToArray();
            }

            return Ok(new
            {
                str = random.ToString(),
                image = arr
            });
        }
    }
}
