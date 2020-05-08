using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ServerResourcesDataAccess
{
    public class Server_Resource_Info
    {
        public int Id { get; set; }
        public double Value { get; set; }

        public int Sample_Time_FK { get; set; }
        [ForeignKey(nameof(Sample_Time_FK))]
        public virtual Sample_Time Sample_Time { get; set; }

        public int Server_Resource_Unit_FK { get; set; }
        [ForeignKey(nameof(Server_Resource_Unit_FK))]
        public virtual Server_Resource_Unit Server_Resource_Unit { get; set; }

        public int? Process_FK { get; set; }
        [ForeignKey(nameof(Process_FK))]
        public virtual Process Process { get; set; }

        public int? Resource_Type_FK { get; set; }
        [ForeignKey(nameof(Resource_Type_FK))]
        public virtual Resource_Type Resource_Type { get; set; }
    }

    public class Process
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public int PID { get; set; }
        public int? Server_User_FK { get; set; }
        [ForeignKey(nameof(Server_User_FK))]
        public virtual Server_User Server_User { get; set; }

        [JsonIgnore]
        public virtual ICollection<Server_Resource_Info> Computer_Resources { get; set; }
    }

    public class Resource_Type
    {
        public int Id { get; set; }
        public string Short_Name { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public virtual ICollection<Server_Resource_Info> Server_Resources { get; set; }
    }


    public class Sample_Time
    {
        public int Id { get; set; }
        public DateTime Time { get; set; }

        [JsonIgnore]
        public virtual ICollection<Server_Resource_Info> Computer_Resources { get; set; }
    }

    public class Server_Resource_Unit
    {
        public int Id { get; set; }

        [Required]
        public string ShortName { get; set; }

        public string Unit { get; set; }

        [JsonIgnore]
        public virtual ICollection<Server_Resource_Info> Computer_Resources { get; set; }
    }

    public class Server_User
    { 
        public int Id { get; set; }


        public string Name { get; set; }

        [JsonIgnore]
        public virtual ICollection<Process> Processes { get; set; }
    }

}
