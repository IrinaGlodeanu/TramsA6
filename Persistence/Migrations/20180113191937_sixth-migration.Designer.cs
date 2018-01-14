﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Persistence.PersistenceFolder;
using System;

namespace Persistence.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20180113191937_sixth-migration")]
    partial class sixthmigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Domain.Entities.Comment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationDate");

                    b.Property<double>("Rating");

                    b.Property<string>("Text");

                    b.Property<Guid?>("TransportMeanId");

                    b.Property<Guid>("TransportationMeanId");

                    b.Property<double>("Trust");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("TransportMeanId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("Domain.Entities.TransportMean", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("IdentifyingCode");

                    b.Property<double>("Rating");

                    b.HasKey("Id");

                    b.ToTable("MeansOfTransport");
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(25);

                    b.Property<string>("Password");

                    b.Property<byte[]>("Salt");

                    b.Property<double>("Trust");

                    b.Property<string>("Username");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Domain.Entities.Comment", b =>
                {
                    b.HasOne("Domain.Entities.TransportMean")
                        .WithMany("Comments")
                        .HasForeignKey("TransportMeanId");

                    b.HasOne("Domain.Entities.User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Domain.Entities.TransportMean", b =>
                {
                    b.OwnsOne("Domain.ValueObjects.Coordinates", "Location", b1 =>
                        {
                            b1.Property<Guid>("TransportMeanId");

                            b1.Property<double>("Latitude");

                            b1.Property<double>("Longitude");

                            b1.ToTable("MeansOfTransport");

                            b1.HasOne("Domain.Entities.TransportMean")
                                .WithOne("Location")
                                .HasForeignKey("Domain.ValueObjects.Coordinates", "TransportMeanId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });
#pragma warning restore 612, 618
        }
    }
}