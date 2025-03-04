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
    [Migration("20240604004457_add Operation")]
    partial class addOperation
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

            modelBuilder.Entity("PropertyAgency.Domain.Entities.Image", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("PropertyId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("PropertyId");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("PropertyAgency.Domain.Entities.Operation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Amount")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("BuyerId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("OperationDate")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("PropertyId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("SellerId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("BuyerId");

                    b.HasIndex("PropertyId");

                    b.HasIndex("SellerId");

                    b.ToTable("Operations");
                });

            modelBuilder.Entity("PropertyAgency.Domain.Entities.Property", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("AddressId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Price")
                        .HasColumnType("TEXT");

                    b.Property<int>("RoomCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.ToTable("Properties");
                });

            modelBuilder.Entity("PropertyAgency.Domain.Entities.Rental", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("PropertyId")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("RentAmount")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("RenterId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("PropertyId");

                    b.HasIndex("RenterId");

                    b.ToTable("Rentals");
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

            modelBuilder.Entity("PropertyAgency.Domain.Entities.Image", b =>
                {
                    b.HasOne("PropertyAgency.Domain.Entities.Property", "Property")
                        .WithMany("Images")
                        .HasForeignKey("PropertyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Property");
                });

            modelBuilder.Entity("PropertyAgency.Domain.Entities.Operation", b =>
                {
                    b.HasOne("PropertyAgency.Domain.Entities.User", "Buyer")
                        .WithMany("BuyerOperations")
                        .HasForeignKey("BuyerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("PropertyAgency.Domain.Entities.Property", "Property")
                        .WithMany("Operations")
                        .HasForeignKey("PropertyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PropertyAgency.Domain.Entities.User", "Seller")
                        .WithMany("SellerOperations")
                        .HasForeignKey("SellerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Buyer");

                    b.Navigation("Property");

                    b.Navigation("Seller");
                });

            modelBuilder.Entity("PropertyAgency.Domain.Entities.Property", b =>
                {
                    b.HasOne("PropertyAgency.Domain.Entities.Address", "Address")
                        .WithMany("Properties")
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");
                });

            modelBuilder.Entity("PropertyAgency.Domain.Entities.Rental", b =>
                {
                    b.HasOne("PropertyAgency.Domain.Entities.Property", "Property")
                        .WithMany("Rentals")
                        .HasForeignKey("PropertyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PropertyAgency.Domain.Entities.User", "User")
                        .WithMany("Rentals")
                        .HasForeignKey("RenterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Property");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PropertyAgency.Domain.Entities.Address", b =>
                {
                    b.Navigation("Properties");
                });

            modelBuilder.Entity("PropertyAgency.Domain.Entities.Property", b =>
                {
                    b.Navigation("Favorites");

                    b.Navigation("Images");

                    b.Navigation("Operations");

                    b.Navigation("Rentals");
                });

            modelBuilder.Entity("PropertyAgency.Domain.Entities.User", b =>
                {
                    b.Navigation("BuyerOperations");

                    b.Navigation("Favorites");

                    b.Navigation("Rentals");

                    b.Navigation("SellerOperations");
                });
#pragma warning restore 612, 618
        }
    }
}
