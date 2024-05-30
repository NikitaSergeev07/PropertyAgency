﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PropertyAgency.DAL;

#nullable disable

namespace PropertyAgency.DAL.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240529001905_add AddressModel")]
    partial class addAddressModel
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.0-preview.4.24267.1");

            modelBuilder.Entity("PropertyAgency.Domain.Entities.Address", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("PropertyId")
                        .HasColumnType("TEXT");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ZipCode")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("PropertyId")
                        .IsUnique();

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("PropertyAgency.Domain.Entities.Favorite", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("PropertyId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("PropertyId");

                    b.HasIndex("UserId");

                    b.ToTable("Favorites");
                });

            modelBuilder.Entity("PropertyAgency.Domain.Entities.Property", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Price")
                        .HasColumnType("TEXT");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("TypeProperty")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Properties");
                });

            modelBuilder.Entity("PropertyAgency.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Role")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("PropertyAgency.Domain.Entities.Address", b =>
                {
                    b.HasOne("PropertyAgency.Domain.Entities.Property", "Property")
                        .WithOne("Address")
                        .HasForeignKey("PropertyAgency.Domain.Entities.Address", "PropertyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Property");
                });

            modelBuilder.Entity("PropertyAgency.Domain.Entities.Favorite", b =>
                {
                    b.HasOne("PropertyAgency.Domain.Entities.Property", "Property")
                        .WithMany("Favorites")
                        .HasForeignKey("PropertyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PropertyAgency.Domain.Entities.User", "User")
                        .WithMany("Favorites")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Property");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PropertyAgency.Domain.Entities.Property", b =>
                {
                    b.HasOne("PropertyAgency.Domain.Entities.User", "User")
                        .WithMany("Properties")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("PropertyAgency.Domain.Entities.Property", b =>
                {
                    b.Navigation("Address")
                        .IsRequired();

                    b.Navigation("Favorites");
                });

            modelBuilder.Entity("PropertyAgency.Domain.Entities.User", b =>
                {
                    b.Navigation("Favorites");

                    b.Navigation("Properties");
                });
#pragma warning restore 612, 618
        }
    }
}
