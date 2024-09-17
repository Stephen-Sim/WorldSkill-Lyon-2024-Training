using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ValuesController : ApiController
    {
        public logistic_dbEntities ent { get; set; }

        public ValuesController()
        {
            ent = new logistic_dbEntities();
        }

        [HttpGet]
        public object login(string u, string p)
        {
            var user = ent.Partners.ToList().FirstOrDefault(x => x.name == u && x.password == p);

            if (user == null)
            {
                return Ok();
            }

            return Ok(new
            {
                user.id,
                user.name,
            });
        }

        /*[HttpGet]
        public object GetPathParthers(long uid)
        {

        }*/

        [HttpGet]
        public object GetParcels(long uid)
        {
            var parcels = ent.Path_Partner_Parcel.ToList().Where(x => x.path_partner_id == uid).Select(x => new
            {
                id = x.parcelId,
                x.Parcel.name,
                x.status,
                next = x.Path_Partner.nextNode == null ? "-" : ent.Path_Partner.FirstOrDefault(y => y.id == x.Path_Partner.nextNode).Area.name
            }).ToList();

            return Ok(parcels);
        }

        [HttpGet]
        public async Task<object> Validate(string pass)
        {
            using (var client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(pass);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var res = await client.PostAsync("http://localhost:5000/api/Passcode/validate", content);

                if (res.IsSuccessStatusCode)
                {
                    var result = await res.Content.ReadAsStringAsync();
                    var isTrue = JsonConvert.DeserializeObject<Test>(result);

                    if (isTrue.isValid)
                    {
                        return Ok();
                    }
                    else
                    {
                        return BadRequest();
                    }
                }

                return BadRequest();
            }
        }

        [HttpPost]
        public object Collect(CollectParcelRequest collect)
        {
            var path_parcel = ent.Path_Partner_Parcel.FirstOrDefault(x => x.Path_Partner.partnerId == collect.uid && x.parcelId == collect.parcelid);

            if (path_parcel == null)
            {
                return BadRequest();
            }

            path_parcel.deliverTime = DateTime.Now;
            path_parcel.description = collect.desc;
            path_parcel.image = collect.img;
            path_parcel.status = "Delivered";

            ent.SaveChanges();

            if (path_parcel.Path_Partner.nextNode != null)
            {
                var Path_Partner_Parcel = new Path_Partner_Parcel()
                {
                    path_partner_id = (int)path_parcel.Path_Partner.nextNode,
                    id = ent.Path_Partner_Parcel.Count() + 1,
                    parcelId = collect.parcelid,
                    status = "Delivering"
                };

                ent.Path_Partner_Parcel.Add(Path_Partner_Parcel);
                ent.SaveChanges();
            }

            return Ok();
        }

        [HttpGet]
        public object GetPathPartners(long uid)
        {
            var all_path_partners = GetAllPathPartners();

            var user_path_partners = new List<List<Path_Partner>>();

            var nextPartners = new List<int>();

            foreach (var path_partners in all_path_partners)
            {
                foreach (var path_partner in path_partners)
                {
                    if (path_partner.partnerId == uid)
                    {
                        user_path_partners.Add(path_partners);

                        if (path_partner.nextNode != null)
                        {
                            nextPartners.Add((int)ent.Path_Partner.First(x => x.id == path_partner.nextNode).partnerId);
                        }

                        break;
                    }
                }
            }

            var pathPartners = new List<PathPartnerDTO>();

            foreach (var path_partners in all_path_partners)
            {
                foreach (var path_partner in path_partners)
                {
                    if (path_partner.partnerId != null && path_partner.partnerId != uid && !pathPartners.Any(x => x.partnerId == path_partner.partnerId))
                    {
                        pathPartners.Add(new PathPartnerDTO
                        {
                            partnerId = (long)path_partner.partnerId,
                            name = path_partner.Partner.name,
                            phone_no = path_partner.Partner.tel_no,
                            isBold = nextPartners.Any(x => x == (int)path_partner.partnerId)
                        });
                    }
                }
            }

            return Ok(pathPartners);
        }

        public class PathPartnerDTO
        {
            public long partnerId { get; set; }
            public string name { get; set; }
            public string phone_no { get; set; }
            public bool isBold { get; set; }
        }


        List<List<Path_Partner>> GetAllPathPartners()
        {
            var first_nodes = ent.Path_Partner.ToList().Where(x => x.isFirst == true).ToList();

            List<List<Path_Partner>> all_paths = new List<List<Path_Partner>>();

            foreach (var path in first_nodes)
            {
                var current_path = path;
                var temp = new List<Path_Partner>();

                while (current_path != null)
                {
                    temp.Add(current_path);

                    current_path = ent.Path_Partner.FirstOrDefault(x => x.id == current_path.nextNode);
                }

                all_paths.Add(temp);
            }

            return all_paths;
        }

        public class CollectParcelRequest
        {
            public int uid { get; set; }
            public int parcelid { get; set; }
            public byte[] img { get; set; }
            public string desc { get; set; }
        }

        public class Test { public bool isValid { get; set; } }

    }
}
