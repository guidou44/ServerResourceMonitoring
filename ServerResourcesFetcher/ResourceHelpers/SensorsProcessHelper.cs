using ServerResourcesFetcher.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace ServerResourcesFetcher.ResourceHelpers
{
    public class SensorsProcessHelper
    {
        private ProcessStartInfo _sensorsProcessStartInfo;

        public SensorsProcessHelper()
        {
            _sensorsProcessStartInfo = new ProcessStartInfo
            {
                FileName = "//usr//bin//sensors",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };
        }

        public SensorsProcessOutput GetCurrentSensorsProcessReadings()
        {
            var output = new SensorsProcessOutput()
            {
                Cpus_Temp = new HashSet<CpuTemperatureInformation>(),
                Fans_Speed = new HashSet<FanSpeedInformation>()
            };

            using (var sensors_process = Process.Start(_sensorsProcessStartInfo))
            {
                var stream = sensors_process.StandardOutput;
                string line = null;
                Regex reg = new Regex(@"\s+", RegexOptions.Compiled);
                int line_ctr = 0;

                while (!stream.EndOfStream)
                {
                    line = stream.ReadLine();
                    if (line_ctr >= 12 && line_ctr <= 17)
                    {
                        line = line.Trim();
                        var array_line = reg.Replace(line, ";").Split(";");
                        var cpuTemp = new CpuTemperatureInformation()
                        {
                            Cpu_Id = int.Parse(array_line[1].Substring(0, 1)),
                            Cpu_Temp = double.Parse((array_line[2].Substring(1)).Replace("°C", ""))
                        };
                        output.Cpus_Temp.Add(cpuTemp);
                    }

                    line_ctr++;
                }

            }
            return output;
        }

    }
}
