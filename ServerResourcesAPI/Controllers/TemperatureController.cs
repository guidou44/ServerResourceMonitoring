using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServerResourcesDataAccess;
using ServerResourcesFetcher.ResourceHelpers;

namespace ServerResourcesAPI.Controllers
{
    public class TemperatureController : ControllerBase
    {
        private SensorsProcessHelper _sensorsHelper;
        public TemperatureController() : base()
        {
            _sensorsHelper = new SensorsProcessHelper();
        }

        [HttpGet]
        [Route("api/[controller]/Cpu")]
        public ActionResult<Server_Resource_Info> GetCpu()
        {
            try
            {
                using (var context = new ServerResourcesContext())
                {
                    var lastCpuTemp = context.Server_Resource_Info
                                              .Include(SRI => SRI.Resource_Type)
                                              .Include(SRI => SRI.Sample_Time)
                                              .Include(SRI => SRI.Server_Resource_Unit)
                                              .Where(SRI => SRI.Process_FK == null && SRI.Resource_Type_FK == 3)
                                              .OrderBy(SRI => SRI.Id)
                                              .LastOrDefault();
                    if (lastCpuTemp.Sample_Time.Time >= (DateTime.Now - TimeSpan.FromSeconds(5))) return Ok(lastCpuTemp);
                }

                var sensorOutput = _sensorsHelper.GetCurrentSensorsProcessReadings();

                double total_temps = 0;
                sensorOutput.Cpus_Temp.ToList().ForEach(c => total_temps += c.Cpu_Temp);
                var cpu_temp = (total_temps / sensorOutput.Cpus_Temp.Count());

                var lastCalculatedCpuTemp = new Server_Resource_Info()
                {
                    Id = 0,
                    Value = cpu_temp,
                    Sample_Time_FK = 0,
                    Server_Resource_Unit_FK = 2,
                    Sample_Time = new Sample_Time() { Id = 0, Time = DateTime.Now },
                    Resource_Type = new Resource_Type() { Id = 0, Short_Name = "CPU_TEMP" },
                    Server_Resource_Unit = new Server_Resource_Unit() { Id = 0, Unit = "°C", ShortName = "TEMP" }
                };

                return Ok(lastCalculatedCpuTemp);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}