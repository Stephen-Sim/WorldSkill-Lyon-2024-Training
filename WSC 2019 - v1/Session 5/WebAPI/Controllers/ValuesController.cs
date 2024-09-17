using Asp.net_core_web_api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Asp.net_core_web_api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public Wsc2019Session5Context ent { get; set; }

        public ValuesController()
        {
            ent = new Wsc2019Session5Context();
        }

        [HttpGet]
        public IActionResult GetWells()
        {
            var well = ent.Wells.ToList().Select(x => new
            {
                x.Id,
                Name = x.WellName,
                Capacity = x.Capacity + "m3"
            });

            return Ok(well);
        }

        [HttpGet]
        public IActionResult GetWellLayers(int id)
        {
            var well = ent.Wells.FirstOrDefault(x => x.Id == id);

            var layers = well.WellLayers.OrderBy(x => x.StartPoint).ToList().Select(x => new
            {
                x.StartPoint,
                x.EndPoint,
                x.RockType.Name,
                Height = (x.EndPoint - x.StartPoint) / 6,
                Color = x.RockType.BackgroundColor,
                Display = x.StartPoint + " m",
                FontColor = "black"
            }).ToList();

            layers.Add(new
            {
                StartPoint = layers.Last().EndPoint,
                EndPoint = 0L,
                Name = "Oil / Gas",
                Height = (well.GasOilDepth - layers.Last().StartPoint) / 6,
                Color = "Black",
                Display = layers.Last().EndPoint + " m",
                FontColor = "white"
            });

            return Ok(layers);
        }

        [HttpGet]
        public IActionResult test()
        {
            var well = new Well()
            { 
                Capacity = 99,
                GasOilDepth = 11,
                WellName = "test test",
                WellTypeId = 1,
            };

            ent.Wells.Add(well);
            ent.SaveChanges();

            return Ok();
        }

        [HttpGet]
        public IActionResult test1()
        {
            var well = ent.Wells.First(x => x.Id == 4);
            well.WellName = "I love yung huey ";
            ent.SaveChanges();

            return Ok();
        }

        [HttpGet]
        public IActionResult test2()
        {
            var well = ent.Wells.First(x => x.Id == 4);
            ent.Wells.Remove(well);
            ent.SaveChanges();

            return Ok();
        }
    }
}
