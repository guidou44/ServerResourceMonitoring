using System;
using System.Collections.Generic;
using System.Text;

namespace ServerResourcesFetcher.Models
{
    public class SensorsProcessOutput
    {
        public ICollection<CpuTemperatureInformation> Cpus_Temp { get; set; }
        public ICollection<FanSpeedInformation> Fans_Speed { get; set; }
    }
}
