﻿// <auto-generated />
using System;
using InfrastructureL.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace InfrastructureL.Migrations
{
    [DbContext(typeof(RegionalContext))]
    [Migration("20210430171459_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DomainL.Models.City", b =>
                {
                    b.Property<int>("CityCode")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(2)
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("RegionCode")
                        .HasColumnType("nvarchar(3)");

                    b.Property<string>("RegionName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("CityCode");

                    b.HasIndex("RegionCode");

                    b.ToTable("CitiesGH");
                });

            modelBuilder.Entity("DomainL.Models.Locality", b =>
                {
                    b.Property<int>("ConstID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CityCode")
                        .HasColumnType("int");

                    b.Property<string>("CityName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("LocalityCode")
                        .IsRequired()
                        .HasMaxLength(2)
                        .HasColumnType("nvarchar(2)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("RegionCode")
                        .HasColumnType("nvarchar(3)");

                    b.Property<string>("RegionName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("ConstID");

                    b.HasIndex("CityCode");

                    b.HasIndex("RegionCode");

                    b.ToTable("LocalitiesGH");
                });

            modelBuilder.Entity("DomainL.Models.Region", b =>
                {
                    b.Property<string>("RegionCode")
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.Property<string>("CapitalCity")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("RegionCode");

                    b.ToTable("RegionsGH");
                });

            modelBuilder.Entity("DomainL.Models.City", b =>
                {
                    b.HasOne("DomainL.Models.Region", "Region")
                        .WithMany("Cities")
                        .HasForeignKey("RegionCode");

                    b.Navigation("Region");
                });

            modelBuilder.Entity("DomainL.Models.Locality", b =>
                {
                    b.HasOne("DomainL.Models.City", "City")
                        .WithMany("Localities")
                        .HasForeignKey("CityCode");

                    b.HasOne("DomainL.Models.Region", "Region")
                        .WithMany("Localities")
                        .HasForeignKey("RegionCode");

                    b.Navigation("City");

                    b.Navigation("Region");
                });

            modelBuilder.Entity("DomainL.Models.City", b =>
                {
                    b.Navigation("Localities");
                });

            modelBuilder.Entity("DomainL.Models.Region", b =>
                {
                    b.Navigation("Cities");

                    b.Navigation("Localities");
                });
#pragma warning restore 612, 618
        }
    }
}
