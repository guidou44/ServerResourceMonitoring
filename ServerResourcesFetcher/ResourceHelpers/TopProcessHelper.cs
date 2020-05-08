using ServerResourcesFetcher.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ServerResourcesFetcher.ResourceHelpers
{
    public class TopProcessHelper
    {
        private ProcessStartInfo _topProcessStartInfo;

        public TopProcessHelper()
        {
            _topProcessStartInfo = new ProcessStartInfo
            {
                FileName = "//usr//bin//top",
                Arguments = "-b",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };
        }

        public TopProcessOutput GetCurrentTopProcessReadings()
        {
            var output = new TopProcessOutput()
            {
                ProcessesSpecificUsage = new HashSet<ProcessInformation>(),
                TotalCpuUsage = 0,
                TotalRamUsage = 0
            };

            using (var top_process = Process.Start(_topProcessStartInfo))
            {
                var stream = top_process.StandardOutput;
                string line = null;
                Regex reg = new Regex(@"\s+", RegexOptions.Compiled);


                for (int i = 0; i < 15; i++)
                {
                    line = stream.ReadLine();
                    line = line.Trim();
                    line = reg.Replace(line, ";");
                    if (i >= 6)
                    {
                        int checkDigit;
                        Regex reg2 = new Regex(";", RegexOptions.Compiled);
                        if (!int.TryParse(line.Substring(0, 1), out checkDigit)) line = reg2.Replace(line, "", 1, 0);
                    }
                    var array_line = line.Split(";");


                    if (i > 6) //process lines
                    {
                        var process = new ProcessInformation()
                        {
                            Pid = int.Parse(array_line[0]),
                            User = array_line[1],
                            Status = array_line[7],
                            CpuUsage = double.Parse(array_line[8]),
                            RamUsage = double.Parse(array_line[9]),
                            Name = array_line[11]
                        };
                        output.ProcessesSpecificUsage.Add(process);
                        output.TotalCpuUsage += process.CpuUsage;
                        output.TotalRamUsage += process.RamUsage;
                    }
                }
            }

            return output;
        }
    }
}



//if (i == 3) //total ram line
//{
//    var total_string = array_line.ToList().Where(el => el == "total,").SingleOrDefault();
//    var total_index = array_line.ToList().IndexOf(total_string) - 1;
//    var used_string = array_line.ToList().Where(el => el == "used,").SingleOrDefault();
//    var used_index = array_line.ToList().IndexOf(used_string) - 1;
//    long ram_total = long.Parse(array_line.ToList().ElementAt(total_index));
//    long ram_used = long.Parse(array_line.ToList().ElementAt(used_index));
//    Console.WriteLine(ram_total);
//    Console.WriteLine(ram_used);


//    output.TotalRamUsage = 100 * (ram_used / ram_total);

//}