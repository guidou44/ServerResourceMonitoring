using Microsoft.EntityFrameworkCore;
using ServerResourcesDataAccess;
using System;
using System.Linq;

namespace EfCoreTester
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new ServerResourcesContext())
            {
                var test = context.Server_Resource_Info
                                  .Include(sri => sri.Sample_Time)
                                  .FirstOrDefault().Sample_Time.Time;
                var test2 = context.Server_Resource_Info
                                   .Include(sri => sri.Server_Resource_Unit)
                                   .FirstOrDefault().Server_Resource_Unit.Unit;
                Console.WriteLine(test);
                Console.WriteLine(test2);
            }

            Console.WriteLine(DateTime.Now > (DateTime.Now - TimeSpan.FromMinutes(1)));


                Console.WriteLine("Added entities");
            Console.ReadLine();
        }
    }
}
