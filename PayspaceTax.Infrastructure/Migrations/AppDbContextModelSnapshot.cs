﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PayspaceTax.Infrastructure.Database;

#nullable disable

namespace PayspaceTax.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PayspaceTax.Domain.Entities.PostalCodeTaxCalculationType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TaxCalculationType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("PostalCodeTaxCalculationTypes", (string)null);
                });

            modelBuilder.Entity("PayspaceTax.Domain.Entities.ProgressiveTaxBracket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("From")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("RatePercentage")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("To")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("ProgressiveTaxBrackets", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
