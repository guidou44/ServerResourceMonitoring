
using ServerResourcesDataAccess;
using ServerResourcesFetcher.Models;
using ServerResourcesFetcher.ResourceHelpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;

namespace ServerResourcesFetcher
{
    class Program
    {
        static void Main(string[] args)
        {
            var topHelper = new TopProcessHelper();
            var sensorsHelper = new SensorsProcessHelper();

            while (true)
            {
                var sampling_time = DateTime.Now;
                var usage_info = topHelper.GetCurrentTopProcessReadings();
                var temperature_info = sensorsHelper.GetCurrentSensorsProcessReadings();
                ManageNewData(usage_info, temperature_info, sampling_time);
                ManageOldData(sampling_time);
                Thread.Sleep(60000
                    );
            }
        }

        static void ManageNewData(TopProcessOutput usage_info, SensorsProcessOutput temperature_info, DateTime samplingTime)
        {
            InsertSamplingTime(samplingTime);
            InsertTemperature(temperature_info);
            InsertTotalUsages(usage_info);
            InsertProcessSpecificUsage(usage_info);
        }

        static void InsertSamplingTime(DateTime samplingTime)
        {
            using (var context = new ServerResourcesContext())
            {
                context.Sample_Time.Add(new Sample_Time() { Time = samplingTime });
                context.SaveChanges();
            }
        }

        static void InsertTemperature(SensorsProcessOutput temperature_info)
        {
            double total_temps = 0;
            temperature_info.Cpus_Temp.ToList().ForEach(c => total_temps += c.Cpu_Temp);
            var cpu_temp = (total_temps / temperature_info.Cpus_Temp.Count());

            using (var context = new ServerResourcesContext())
            {
                context.Server_Resource_Info.Add(new Server_Resource_Info()
                {
                    Value = cpu_temp,
                    Sample_Time_FK = context.Sample_Time.Last().Id,
                    Server_Resource_Unit_FK = context.Server_Resource_Unit.Where(SRU => SRU.ShortName == "TEMP").SingleOrDefault().Id,
                    Process_FK = null,
                    Resource_Type_FK = context.Resource_Type.Where(RT => RT.Short_Name == "CPU_TEMP").SingleOrDefault().Id
                }); ;
                context.SaveChanges();
            }
        }

        static void InsertTotalUsages(TopProcessOutput usage_info)
        {
            using (var context = new ServerResourcesContext())
            {
                context.Server_Resource_Info.Add(new Server_Resource_Info()
                {
                    Value = usage_info.TotalCpuUsage,
                    Sample_Time_FK = context.Sample_Time.Last().Id,
                    Server_Resource_Unit_FK = context.Server_Resource_Unit.Where(SRU => SRU.ShortName == "PRCENT").SingleOrDefault().Id,
                    Process_FK = null,
                    Resource_Type_FK = context.Resource_Type.Where(RT => RT.Short_Name == "CPU_USE").SingleOrDefault().Id
                });
                context.Server_Resource_Info.Add(new Server_Resource_Info()
                {
                    Value = usage_info.TotalRamUsage,
                    Sample_Time_FK = context.Sample_Time.Last().Id,
                    Server_Resource_Unit_FK = context.Server_Resource_Unit.Where(SRU => SRU.ShortName == "PRCENT").SingleOrDefault().Id,
                    Process_FK = null,
                    Resource_Type_FK = context.Resource_Type.Where(RT => RT.Short_Name == "RAM_USE").SingleOrDefault().Id
                });
                context.SaveChanges();
            }
        }

        static void InsertProcessSpecificUsage(TopProcessOutput usage_info)
        {
            foreach (var proc_info in usage_info.ProcessesSpecificUsage)
            {
                if (proc_info.Status == "R")
                {
                    InsertProcess(proc_info);
                    using (var context = new ServerResourcesContext())
                    {
                        context.Server_Resource_Info.Add(new Server_Resource_Info()
                        {
                            Value = proc_info.CpuUsage,
                            Sample_Time_FK = context.Sample_Time.Last().Id,
                            Server_Resource_Unit_FK = context.Server_Resource_Unit.Where(SRU => SRU.ShortName == "PRCENT").SingleOrDefault().Id,
                            Process_FK = context.Process.Where(P => P.PID == proc_info.Pid).SingleOrDefault().Id,
                            Resource_Type_FK = context.Resource_Type.Where(RT => RT.Short_Name == "CPU_USE").SingleOrDefault().Id
                        });
                        context.Server_Resource_Info.Add(new Server_Resource_Info()
                        {
                            Value = proc_info.RamUsage,
                            Sample_Time_FK = context.Sample_Time.Last().Id,
                            Server_Resource_Unit_FK = context.Server_Resource_Unit.Where(SRU => SRU.ShortName == "PRCENT").SingleOrDefault().Id,
                            Process_FK = context.Process.Where(P => P.PID == proc_info.Pid).SingleOrDefault().Id,
                            Resource_Type_FK = context.Resource_Type.Where(RT => RT.Short_Name == "RAM_USE").SingleOrDefault().Id
                        });
                        context.SaveChanges();
                    }
                }
            }
        }

        static void InsertProcess(ProcessInformation proc_info)
        {
            var proc = new ServerResourcesDataAccess.Process()
            { 
                Name = proc_info.Name,
                Status = proc_info.Status,
                PID = proc_info.Pid
            };

            using (var context = new ServerResourcesContext())
            {
                var associated_proc = context.Process.Where(P => P.Name == proc.Name).SingleOrDefault();
                proc.Server_User_FK = context.Server_User.Where(SU => SU.Name == proc_info.User).SingleOrDefault().Id;

                if (associated_proc != null)
                {
                    associated_proc.PID = proc.PID;
                    associated_proc.Server_User_FK = proc.Server_User_FK;
                }
                else
                {
                    context.Add(proc);
                }

                context.SaveChanges();
            }
        }

        static void ManageOldData(DateTime samplingTime)
        {
            using (var context = new ServerResourcesContext())
            {
                var oldest_sample = context.Sample_Time.OrderBy(ST => ST.Time).First();

                if (oldest_sample.Time < (samplingTime - TimeSpan.FromMinutes(60))) context.Remove(oldest_sample);
                context.SaveChanges();
            }
        }
    }
}
