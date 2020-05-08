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
    [ApiController]
    public class ProcessController : ControllerBase
    {
        private TopProcessHelper _topHelper;
        public ProcessController() : base()
        {
            _topHelper = new TopProcessHelper();
        }

        [HttpGet]
        [Route("api/[controller]/Cpu")]
        public ActionResult<List<Server_Resource_Info>> GetCpuPerProcess()
        {
            try
            {
                using (var context = new ServerResourcesContext())
                {
                    var processCollection = context.Server_Resource_Info
                                                   .Include(SRI => SRI.Sample_Time)
                                                   .Include(SRI => SRI.Resource_Type)
                                                   .Include(SRI => SRI.Process)
                                                   .Include(SRI => SRI.Server_Resource_Unit)
                                                   .Where(SRI => SRI.Resource_Type_FK == 2
                                                              && SRI.Process_FK != null);
                    var output = processCollection.Where(PC => PC.Sample_Time.Time > (DateTime.Now - TimeSpan.FromSeconds(70)));
                    if (processCollection.Count() > 0) return Ok(output.ToList());
                }
                throw new FieldAccessException("Could not find running processes");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}