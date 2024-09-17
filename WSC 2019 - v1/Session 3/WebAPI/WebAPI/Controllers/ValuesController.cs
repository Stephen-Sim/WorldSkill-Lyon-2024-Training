using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Web.Http;

namespace WebAPI.Controllers
{
    public class ValuesController : ApiController
    {
        public WSC2019_Session3Entities ent { get; set; }

        public ValuesController()
        {
            ent = new WSC2019_Session3Entities();
        }

        [HttpGet]
        public object GetAssets()
        {
            var assets = ent.Assets.ToList().Select(x => new
            {
                x.ID,
                Name = x.AssetName
            });
            return Ok(assets);
        }

        [HttpGet]
        public object GetTasks()
        {
            var tasks = ent.Tasks.ToList().Select(x => new
            {
                x.ID,
                x.Name
            });
            return Ok(tasks);
        }

        [HttpGet]
        public object GetActiveTasks(DateTime dateTime)
        {
            var activeTasks = ent.PMTasks.ToList()
                .OrderBy(x => x.TaskDone).ThenByDescending(x => x.ScheduleDate == null).ThenBy(x => x.ScheduleDate)
                .Where(x => x.ScheduleDate == null || (x.ScheduleDate != null && x.ScheduleDate <= dateTime.AddDays(4))).Select(x => new
                {
                    x.ID,
                    AssetInfo = $"{x.Asset.AssetName} SN: {x.Asset.AssetSN}", 
                    TaskName = x.Task.Name,
                    x.TaskDone,
                    x.TaskID,
                    x.AssetID,
                    DisplayTaskTypeInfo = new Func<string>(() =>
                    {
                        if (x.ScheduleDate != null)
                        {
                            return $"Monthly - at {((DateTime)x.ScheduleDate).ToString("yyyy-MM-dd")}";
                        }

                        return $"Milage - at {x.ScheduleKilometer} kilometer";
                    })(),
                    x.ScheduleDate,
                    Color = new Func<string>(() =>
                    {
                        if (x.ScheduleDate == null)
                        {
                            if (x.TaskDone == true)
                            {
                                return "gray";
                            }

                            return "black";
                        }

                        if (x.ScheduleDate < DateTime.Today)
                        {
                            if (x.TaskDone == true)
                            {
                                return "orange";
                            }

                            return "red";
                        }

                        if (x.ScheduleDate == DateTime.Today)
                        {
                            if (x.TaskDone == true)
                            {
                                return "green";
                            }

                            return "black";
                        }

                        if (x.ScheduleDate > DateTime.Today)
                        {
                            if (x.TaskDone == true)
                            {
                                return "black";
                            }

                            return "purple";
                        }

                        return "";
                    })()
                });

            return Ok(activeTasks);
        }

        [HttpGet]
        public object UpdateStatus(long Id)
        {
            var pmTask = ent.PMTasks.FirstOrDefault(x => x.ID == Id);
            
            pmTask.TaskDone = true;
            ent.SaveChanges();

            return Ok();
        }

        [HttpPost]
        public object StoreTask(CreatePMTaskRequest Temp)
        {
            foreach (var assetId in Temp.AssetIds)
            {
                if (Temp.ModelId == 1)
                {
                    for (var i = Temp.StartMilage; i <= Temp.EndMilage; i = i + Temp.MilageInteval)
                    {
                        if (ent.PMTasks.ToList().Any(x => x.TaskID == Temp.TaskId && x.AssetID == assetId && i <= x.ScheduleKilometer))
                        {
                            continue;
                        }

                        var pmTask = new PMTask()
                        {
                            AssetID = assetId,
                            TaskID = Temp.TaskId,
                            PMScheduleTypeID = 1,
                            TaskDone = false,
                            ScheduleKilometer = i,
                        };

                        ent.PMTasks.Add(pmTask);
                    }
                }
                else if (Temp.ModelId == 2)
                {
                    for (var i = (DateTime) Temp.StartDate; i <= Temp.EndDate; i = i.AddDays((int)Temp.DailyInterval))
                    {
                        var pmTask = new PMTask()
                        {
                            AssetID = assetId,
                            TaskID = Temp.TaskId,
                            PMScheduleTypeID = 2,
                            TaskDone = false,
                            ScheduleDate = i,
                        };

                        ent.PMTasks.Add(pmTask);
                    }
                }
                else if (Temp.ModelId == 3)
                {
                    for (var i = (DateTime)Temp.StartDate; i <= Temp.EndDate; i = i.AddDays(1))
                    {
                        if (i.DayOfWeek == Temp.DayOfWeekInterval)
                        {
                            var pmTask = new PMTask()
                            {
                                AssetID = assetId,
                                TaskID = Temp.TaskId,
                                PMScheduleTypeID = 2,
                                TaskDone = false,
                                ScheduleDate = i,
                            };

                            ent.PMTasks.Add(pmTask);
                        }
                    }
                }
                else if (Temp.ModelId == 4)
                {
                    for (var i = (DateTime)Temp.StartDate; i <= Temp.EndDate; i = i.AddDays(1))
                    {
                        if (i.Day == Temp.MonthlyInterval)
                        {
                            var pmTask = new PMTask()
                            {
                                AssetID = assetId,
                                TaskID = Temp.TaskId,
                                PMScheduleTypeID = 2,
                                TaskDone = false,
                                ScheduleDate = i,
                            };

                            ent.PMTasks.Add(pmTask);
                        }
                    }
                }
            }

            ent.SaveChanges();

            return Ok();
        }

        public class CreatePMTaskRequest
        {
            public long TaskId { get; set; }
            public long ModelId { get; set; }
            public List<long> AssetIds { get; set; }

            public DateTime? StartDate { get; set; }
            public DateTime? EndDate { get; set; }

            public int? DailyInterval { get; set; }
            public DayOfWeek? DayOfWeekInterval { get; set; }
            public int? MonthlyInterval { get; set; }

            public int? StartMilage { get; set; }
            public int? EndMilage { get; set; }
            public int? MilageInteval { get; set; }
        }
    }
}
