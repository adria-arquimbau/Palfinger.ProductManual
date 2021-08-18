﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Palfinger.ProductManual.Infrastructure.Data;

namespace Palfinger.ProductManual.Infrastructure.Data.Migrations
{
    [DbContext(typeof(ProductManualDbContext))]
    partial class ProductManualDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.9");

            modelBuilder.Entity("Palfinger.ProductManual.Domain.Attribute", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("ProductId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("Attribute");
                });

            modelBuilder.Entity("Palfinger.ProductManual.Domain.Configuration", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int?>("AttributeId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AttributeId");

                    b.ToTable("Configuration");
                });

            modelBuilder.Entity("Palfinger.ProductManual.Domain.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("Palfinger.ProductManual.Domain.Attribute", b =>
                {
                    b.HasOne("Palfinger.ProductManual.Domain.Product", null)
                        .WithMany("Attributes")
                        .HasForeignKey("ProductId");
                });

            modelBuilder.Entity("Palfinger.ProductManual.Domain.Configuration", b =>
                {
                    b.HasOne("Palfinger.ProductManual.Domain.Attribute", null)
                        .WithMany("Configurations")
                        .HasForeignKey("AttributeId");
                });

            modelBuilder.Entity("Palfinger.ProductManual.Domain.Attribute", b =>
                {
                    b.Navigation("Configurations");
                });

            modelBuilder.Entity("Palfinger.ProductManual.Domain.Product", b =>
                {
                    b.Navigation("Attributes");
                });
#pragma warning restore 612, 618
        }
    }
}
