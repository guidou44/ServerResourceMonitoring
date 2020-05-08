﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ServerResourcesDataAccess;

namespace ServerResourcesDataAccess.Migrations
{
    [DbContext(typeof(ServerResourcesContext))]
    [Migration("20191124172922_Made_sample_time_Unique")]
    partial class Made_sample_time_Unique
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity("ServerResourcesDataAccess.Process", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("PID");

                    b.Property<int?>("Server_User_FK");

                    b.Property<string>("Status");

                    b.HasKey("Id");

                    b.HasIndex("Server_User_FK");

                    b.ToTable("Process");
                });

            modelBuilder.Entity("ServerResourcesDataAccess.Resource_Type", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<string>("Short_Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Resource_Type");
                });

            modelBuilder.Entity("ServerResourcesDataAccess.Sample_Time", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Time");

                    b.HasKey("Id");

                    b.HasIndex("Time")
                        .IsUnique();

                    b.ToTable("Sample_Time");
                });

            modelBuilder.Entity("ServerResourcesDataAccess.Server_Resource_Info", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("Process_FK");

                    b.Property<int?>("Resource_Type_FK");

                    b.Property<int>("Sample_Time_FK");

                    b.Property<int>("Server_Resource_Unit_FK");

                    b.Property<double>("Value");

                    b.HasKey("Id");

                    b.HasIndex("Process_FK");

                    b.HasIndex("Resource_Type_FK");

                    b.HasIndex("Sample_Time_FK");

                    b.HasIndex("Server_Resource_Unit_FK");

                    b.ToTable("Server_Resource_Info");
                });

            modelBuilder.Entity("ServerResourcesDataAccess.Server_Resource_Unit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ShortName")
                        .IsRequired();

                    b.Property<string>("Unit")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Server_Resource_Unit");
                });

            modelBuilder.Entity("ServerResourcesDataAccess.Server_User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Server_User");
                });

            modelBuilder.Entity("ServerResourcesDataAccess.Process", b =>
                {
                    b.HasOne("ServerResourcesDataAccess.Server_User", "Server_User")
                        .WithMany("Processes")
                        .HasForeignKey("Server_User_FK");
                });

            modelBuilder.Entity("ServerResourcesDataAccess.Server_Resource_Info", b =>
                {
                    b.HasOne("ServerResourcesDataAccess.Process", "Process")
                        .WithMany("Computer_Resources")
                        .HasForeignKey("Process_FK");

                    b.HasOne("ServerResourcesDataAccess.Resource_Type", "Resource_Type")
                        .WithMany("Server_Resources")
                        .HasForeignKey("Resource_Type_FK");

                    b.HasOne("ServerResourcesDataAccess.Sample_Time", "Sample_Time")
                        .WithMany("Computer_Resources")
                        .HasForeignKey("Sample_Time_FK")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ServerResourcesDataAccess.Server_Resource_Unit", "Server_Resource_Unit")
                        .WithMany("Computer_Resources")
                        .HasForeignKey("Server_Resource_Unit_FK")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
