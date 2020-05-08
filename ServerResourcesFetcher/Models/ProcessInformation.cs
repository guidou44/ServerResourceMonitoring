using System;
using System.Collections.Generic;
using System.Text;

namespace ServerResourcesFetcher.Models
{
    public class ProcessInformation
    {
        public int Pid { get; set; }
        public string User { get; set; }
        public string Status { get; set; }
        public double CpuUsage { get; set; }
        public double RamUsage { get; set; }
        public string Name { get; set; }
    }
}
