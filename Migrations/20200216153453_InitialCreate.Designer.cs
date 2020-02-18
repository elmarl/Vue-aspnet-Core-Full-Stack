﻿// <auto-generated />
using System;
using AspNetCoreVueStarter.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AspNetCoreVueStarter.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20200216153453_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AspNetCoreVueStarter.Models.EventsModel", b =>
                {
                    b.Property<int>("Eventid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Details")
                        .HasColumnType("nvarchar(1500)")
                        .HasMaxLength(1500);

                    b.Property<DateTime>("EventDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("EventName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Eventid");

                    b.ToTable("EventModel");
                });

            modelBuilder.Entity("AspNetCoreVueStarter.Models.ParticipantModel", b =>
                {
                    b.Property<int>("Participantid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Details")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasMaxLength(5000);

                    b.Property<int>("EventModelEventid")
                        .HasColumnType("int");

                    b.Property<string>("Familyname")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Idcode")
                        .IsRequired()
                        .HasColumnName("code")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<int?>("NumParticipants")
                        .HasColumnType("int");

                    b.Property<string>("ParticipantType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Paymentmethod")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Participantid");

                    b.HasIndex("EventModelEventid");

                    b.ToTable("ParticipantModel");
                });

            modelBuilder.Entity("AspNetCoreVueStarter.Models.ParticipantModel", b =>
                {
                    b.HasOne("AspNetCoreVueStarter.Models.EventsModel", "EventModel")
                        .WithMany("Participants")
                        .HasForeignKey("EventModelEventid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}