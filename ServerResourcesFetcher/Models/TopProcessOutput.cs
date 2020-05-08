using System;
using System.Collections.Generic;
using System.Text;

namespace ServerResourcesFetcher.Models
{
    public class TopProcessOutput
    {
        public double TotalCpuUsage { get; set; }
        public double TotalRamUsage { get; set; }
        public ICollection<ProcessInformation> ProcessesSpecificUsage { get; set; }
    }
}
