using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServerResourcesDataAccess
{
    public class ServerResourcesContext : DbContext
    {
        public DbSet<Process> Process { get; set; }
        public DbSet<Resource_Type> Resource_Type { get; set; }
        public DbSet<Server_Resource_Info> Server_Resource_Info { get; set; }
        public DbSet<Server_Resource_Unit> Server_Resource_Unit { get; set; }
        public DbSet<Sample_Time> Sample_Time { get; set; }
        public DbSet<Server_User> Server_User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=192.168.50.107;database=ComputerResourcesDB;user=resource_admin2;password=!Admin_44");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Server_Resource_Info>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Value).IsRequired();

                entity.HasOne(e => e.Sample_Time)
                      .WithMany(ST => ST.Computer_Resources);
                entity.HasOne(e => e.Server_Resource_Unit)
                      .WithMany(CRU => CRU.Computer_Resources);
            });

            modelBuilder.Entity<Sample_Time>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Time).IsRequired();
                entity.HasIndex(e => e.Time).IsUnique();
            });

            modelBuilder.Entity<Server_Resource_Unit>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Unit).IsRequired();
            });

            modelBuilder.Entity<Process>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<Server_User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
                entity.HasIndex(e => e.Name).IsUnique();
            });

            modelBuilder.Entity<Resource_Type>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Short_Name).IsRequired();
            });

        }
    }
}
