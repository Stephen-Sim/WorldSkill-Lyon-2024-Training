using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static WebAPI.Controllers.ValuesController;

namespace WebAPI.Controllers
{
    public class ValuesController : ApiController
    {
        public WSC2019_Session1Entities ent { get; set; }
        public ValuesController()
        {
            ent = new WSC2019_Session1Entities();
        }

        [HttpGet]
        public object GetDepartments()
        {
            var d = ent.Departments.ToList().Select(x => new
            {
                x.ID,
                x.Name
            });

            return Ok(d);
        }

        [HttpGet]
        public object GetEmployees()
        {
            var d = ent.Employees.ToList().Select(x => new
            {
                x.ID,
                Name = x.FirstName + " " + x.LastName
            });

            return Ok(d);
        }

        [HttpGet]
        public object GetLocations()
        {
            var d = ent.Locations.ToList().Select(x => new
            {
                x.ID,
                x.Name
            });

            return Ok(d);
        }

        [HttpGet]
        public object GetAssetGroups()
        {
            var d = ent.AssetGroups.ToList().Select(x => new
            {
                x.ID,
                x.Name
            });

            return Ok(d);
        }

        [HttpGet]
        public object GetAssets()
        {
            var a = ent.Assets.ToList().Select(x => new { 
                x.ID,
                x.AssetSN,
                x.AssetName,
                x.DepartmentLocationID,
                x.EmployeeID,
                x.AssetGroupID,
                x.Description,
                x.WarrantyDate,
                
                x.DepartmentLocation.DepartmentID,
                x.DepartmentLocation.LocationID,
                DepartmentName = x.DepartmentLocation.Department.Name,
            });

            return Ok(a);
        }

        [HttpGet]
        public object GetAssetLogs(long Id)
        {
            var a = ent.AssetTransferLogs.Where(x => x.AssetID == Id).ToList().Select(x => new
            {
                TransferDate = x.TransferDate.ToString("yyyy-MM-dd"),
                OldDepartment = x.DepartmentLocation.Department.Name,
                NewDepartment = x.DepartmentLocation1.Department.Name,
                OldAssetSN = x.FromAssetSN,
                NewAssetSN = x.ToAssetSN
            });

            return Ok(a);
        }

        [HttpGet]
        public object GetNewAssetSN(long DepartmentId, long LocationId, long AGId)
        {
            string NewAssetSN = DepartmentId.ToString("00") + "/" + LocationId.ToString("00") + "/";

            var count = ent.Assets.Where(x => x.DepartmentLocation.DepartmentID == DepartmentId).Count() + 1;

            NewAssetSN += count.ToString("0000");

            return Ok(NewAssetSN);
        }

        [HttpGet]
        public object GetAssetPhotos(long Id)
        {
            var a = ent.AssetPhotos.Where(x => x.AssetID == Id).ToList().Select(x => 
                x.AssetPhoto1
            ).ToList();

            return a;
        }

        [HttpPost]
        public object StoreAsset(StoreAssetRequest storeAssetRequest)
        {
            var departmentLocation = ent.DepartmentLocations.FirstOrDefault(x => x.DepartmentID == storeAssetRequest.DepartmentId && x.LocationID == storeAssetRequest.LocationId);

            if (departmentLocation == null)
            {
                return BadRequest();
            }

            var asset = new Asset()
            {
               AssetGroupID = storeAssetRequest.AssetGroupId,
               AssetSN = storeAssetRequest.AssetSN,
               AssetName = storeAssetRequest.AssetName,
               EmployeeID = storeAssetRequest.EmployeeId,
               WarrantyDate = storeAssetRequest.ExipredDate,
               Description = storeAssetRequest.AssetDesc,
               DepartmentLocationID = departmentLocation.ID,
            };

            ent.Assets.Add(asset);

            foreach (var image in storeAssetRequest.AssetImages)
            {
                var newImage = new AssetPhoto()
                {
                    AssetID = asset.ID,
                    AssetPhoto1 = image
                };

                ent.AssetPhotos.Add(newImage);
            }

            ent.SaveChanges();

            return Ok();
        }

        [HttpPost]
        public object EditAsset(EditAssetRequest editAssetRequest)
        {
            var asset = ent.Assets.FirstOrDefault(x => x.ID == editAssetRequest.AssetID);

            asset.AssetName = editAssetRequest.AssetName;
            asset.EmployeeID = editAssetRequest.EmployeeId;
            asset.Description = editAssetRequest.AssetDesc;
            asset.WarrantyDate = editAssetRequest.ExipredDate;

            ent.AssetPhotos.RemoveRange(ent.AssetPhotos.Where(x => x.AssetID == editAssetRequest.AssetID).ToList());

            foreach (var image in editAssetRequest.AssetImages)
            {
                var newImage = new AssetPhoto()
                {
                    AssetID = editAssetRequest.AssetID,
                    AssetPhoto1 = image
                };

                ent.AssetPhotos.Add(newImage);
            }

            ent.SaveChanges();

            return Ok();
        }

        [HttpPost]
        public object TransferAsset(TransferAssetRequest transferAssetRequest)
        {
            var departmentLocation = ent.DepartmentLocations.FirstOrDefault(x => x.DepartmentID == transferAssetRequest.DepartmentId && x.LocationID == transferAssetRequest.LocationId);

            if (departmentLocation == null)
            {
                return BadRequest();
            }

            var asset = ent.Assets.FirstOrDefault(x => x.ID == transferAssetRequest.AssetID);

            var transferLog = new AssetTransferLog()
            {
                AssetID = transferAssetRequest.AssetID,
                FromDepartmentLocationID = asset.DepartmentLocationID,
                FromAssetSN = asset.AssetSN,
                ToDepartmentLocationID = departmentLocation.ID,
                ToAssetSN = transferAssetRequest.NewAssetSN,
                TransferDate = DateTime.Now,
            };

            ent.AssetTransferLogs.Add(transferLog);

            asset.DepartmentLocationID = departmentLocation.ID;
            asset.AssetSN = transferAssetRequest.NewAssetSN;

            ent.SaveChanges();

            return Ok();
        }

        public class TransferAssetRequest
        {
            public long AssetID { get; set; }
            public long DepartmentId { get; set; }
            public long LocationId { get; set; }
            public string NewAssetSN { get; set; }
        }

        public class StoreAssetRequest
        {
            public string AssetName { get; set; }
            public string AssetSN { get; set; }
            public long DepartmentId { get; set; }
            public long LocationId { get; set; }
            public long AssetGroupId { get; set; }
            public long EmployeeId { get; set; }
            public string AssetDesc { get; set; }

            public DateTime? ExipredDate { get; set; }
            public List<byte[]> AssetImages { get; set; } = new List<byte[]>();
        }

        public class EditAssetRequest
        {
            public long AssetID { get; set; }
            public string AssetName { get; set; }
            public long EmployeeId { get; set; }
            public string AssetDesc { get; set; }

            public DateTime? ExipredDate { get; set; }
            public List<byte[]> AssetImages { get; set; } = new List<byte[]>();
        }
    }
}
