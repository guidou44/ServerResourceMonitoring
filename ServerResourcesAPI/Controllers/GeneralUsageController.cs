using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServerResourcesDataAccess;
using ServerResourcesFetcher.ResourceHelpers;
using ServerResourcesAPI.Extensions;
using System.Net;

namespace ServerResourcesAPI.Controllers
{
    [ApiController]
    public class GeneralUsageController : ControllerBase
    {
        private TopProcessHelper _topHelper;
        public GeneralUsageController() : base()
        {
            _topHelper = new TopProcessHelper();
        }

        [HttpGet]
        [Route("api/[controller]/Cpu")]
        public ActionResult<Server_Resource_Info> GetCpu()
        {
            var request = new HttpRequestMessage();
            try
            {
                using (var context = new ServerResourcesContext())
                {
                    var lastCpuUsage = context.Server_Resource_Info
                                              .Include(SRI => SRI.Resource_Type)
                                              .Include(SRI => SRI.Sample_Time)
                                              .Include(SRI => SRI.Server_Resource_Unit)
                                              .Where(SRI => SRI.Process_FK == null && SRI.Resource_Type_FK == 2)
                                              .OrderBy(SRI => SRI.Id)
                                              .LastOrDefault();

                    if (lastCpuUsage.Sample_Time.Time >= (DateTime.Now - TimeSpan.FromSeconds(2))) return Ok(lastCpuUsage);
                }

                var topOutput = _topHelper.GetCurrentTopProcessReadings();
                var lastCalculatedCpuUsage = new Server_Resource_Info()
                {
                    Id = 0,
                    Value = topOutput.TotalCpuUsage,
                    Sample_Time_FK = 0,
                    Server_Resource_Unit_FK = 2,
                    Sample_Time = new Sample_Time() { Id = 0, Time = DateTime.Now },
                    Resource_Type = new Resource_Type() { Id = 0, Short_Name = "CPU_USE" },
                    Server_Resource_Unit = new Server_Resource_Unit() { Id = 0, Unit = "%", ShortName = "PRCENT" }
                };

                return Ok(lastCalculatedCpuUsage);
            }
            catch (Exception e) 
            {
                //return request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("api/[controller]/Ram")]
        public ActionResult<Server_Resource_Info> GetRam()
        {
            try
            {
                using (var context = new ServerResourcesContext())
                {
                    var lastRamUsage = context.Server_Resource_Info
                                              .Include(SRI => SRI.Resource_Type)
                                              .Include(SRI => SRI.Sample_Time)
                                              .Include(SRI => SRI.Server_Resource_Unit)
                                              .Where(SRI => SRI.Process_FK == null && SRI.Resource_Type_FK == 1)
                                              .OrderBy(SRI => SRI.Id)
                                              .LastOrDefault();
                    if (lastRamUsage.Sample_Time.Time >= (DateTime.Now - TimeSpan.FromSeconds(2))) return Ok(lastRamUsage);
                }

                var topOutput = _topHelper.GetCurrentTopProcessReadings();
                var lastCalculatedRamUsage = new Server_Resource_Info()
                {
                    Id = 0,
                    Value = topOutput.TotalRamUsage,
                    Sample_Time_FK = 0,
                    Server_Resource_Unit_FK = 2,
                    Sample_Time = new Sample_Time() { Id = 0, Time = DateTime.Now },
                    Resource_Type = new Resource_Type() { Id = 0, Short_Name = "RAM_USE" },
                    Server_Resource_Unit = new Server_Resource_Unit() { Id = 0, Unit = "%", ShortName = "PRCENT" }
                };

                return Ok(lastCalculatedRamUsage);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}