﻿// <auto-generated />
using System;
using GreenOcean.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GreenOcean.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240421150453_third")]
    partial class third
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GreenOcean.Data.Entities.Code", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("GeneratedCode")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Codes");
                });

            modelBuilder.Entity("GreenOcean.Data.Entities.Equipment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GreenhouseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<float>("MaxHumidity")
                        .HasColumnType("real");

                    b.Property<float>("MaxLightLevel")
                        .HasColumnType("real");

                    b.Property<float>("MaxTemperature")
                        .HasColumnType("real");

                    b.Property<float>("MinHumidity")
                        .HasColumnType("real");

                    b.Property<float>("MinLightLevel")
                        .HasColumnType("real");

                    b.Property<float>("MinTemperature")
                        .HasColumnType("real");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RegisteredEquipmentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("GreenhouseId");

                    b.HasIndex("RegisteredEquipmentId")
                        .IsUnique();

                    b.ToTable("Equipments");
                });

            modelBuilder.Entity("GreenOcean.Data.Entities.Greenhouse", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Number")
                        .HasColumnType("int");

                    b.Property<string>("Street")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Greenhouses");
                });

            modelBuilder.Entity("GreenOcean.Data.Entities.Plant", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GreenhouseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<float?>("Height")
                        .HasColumnType("real");

                    b.Property<float>("Humidity")
                        .HasColumnType("real");

                    b.Property<float>("MositureLevel")
                        .HasColumnType("real");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhotoId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhotoURL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Soil")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Temperature")
                        .HasColumnType("real");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("GreenhouseId");

                    b.ToTable("Plants");
                });

            modelBuilder.Entity("GreenOcean.Data.Entities.Process", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("GreenhouseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ProcessName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("GreenhouseId");

                    b.ToTable("Processes");
                });

            modelBuilder.Entity("GreenOcean.Data.Entities.RegisteredEquipment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("RegisteredEquipments");
                });

            modelBuilder.Entity("GreenOcean.Data.Entities.SensorData", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("EquipmentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<float>("Humidity")
                        .HasColumnType("real");

                    b.Property<float>("LightLevel")
                        .HasColumnType("real");

                    b.Property<string>("SoilMoisture")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Temperature")
                        .HasColumnType("real");

                    b.Property<string>("Timestamp")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("EquipmentId");

                    b.ToTable("SensorData");
                });

            modelBuilder.Entity("GreenOcean.Data.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Password")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Salt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("GreenOcean.Data.Entities.Code", b =>
                {
                    b.HasOne("GreenOcean.Data.Entities.User", "User")
                        .WithOne("Code")
                        .HasForeignKey("GreenOcean.Data.Entities.Code", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("GreenOcean.Data.Entities.Equipment", b =>
                {
                    b.HasOne("GreenOcean.Data.Entities.Greenhouse", "Greenhouse")
                        .WithMany("Equipments")
                        .HasForeignKey("GreenhouseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GreenOcean.Data.Entities.RegisteredEquipment", "RegisteredEquipment")
                        .WithOne("Equipment")
                        .HasForeignKey("GreenOcean.Data.Entities.Equipment", "RegisteredEquipmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Greenhouse");

                    b.Navigation("RegisteredEquipment");
                });

            modelBuilder.Entity("GreenOcean.Data.Entities.Greenhouse", b =>
                {
                    b.HasOne("GreenOcean.Data.Entities.User", "User")
                        .WithMany("Greenhouses")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("GreenOcean.Data.Entities.Plant", b =>
                {
                    b.HasOne("GreenOcean.Data.Entities.Greenhouse", "Greenhouse")
                        .WithMany("Plants")
                        .HasForeignKey("GreenhouseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Greenhouse");
                });

            modelBuilder.Entity("GreenOcean.Data.Entities.Process", b =>
                {
                    b.HasOne("GreenOcean.Data.Entities.Greenhouse", "Greenhouse")
                        .WithMany("Posts")
                        .HasForeignKey("GreenhouseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Greenhouse");
                });

            modelBuilder.Entity("GreenOcean.Data.Entities.SensorData", b =>
                {
                    b.HasOne("GreenOcean.Data.Entities.Equipment", "Equipment")
                        .WithMany("SensorData")
                        .HasForeignKey("EquipmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Equipment");
                });

            modelBuilder.Entity("GreenOcean.Data.Entities.Equipment", b =>
                {
                    b.Navigation("SensorData");
                });

            modelBuilder.Entity("GreenOcean.Data.Entities.Greenhouse", b =>
                {
                    b.Navigation("Equipments");

                    b.Navigation("Plants");

                    b.Navigation("Posts");
                });

            modelBuilder.Entity("GreenOcean.Data.Entities.RegisteredEquipment", b =>
                {
                    b.Navigation("Equipment");
                });

            modelBuilder.Entity("GreenOcean.Data.Entities.User", b =>
                {
                    b.Navigation("Code");

                    b.Navigation("Greenhouses");
                });
#pragma warning restore 612, 618
        }
    }
}