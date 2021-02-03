﻿// <auto-generated />
using System;
using Aktien.Data.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Aktien.Data.Migrations
{
    [DbContext(typeof(Infrastructure.Repository))]
    [Migration("20201223195650_3")]
    partial class _3
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Data.Model.AktieModel.Aktie", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ISIN")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("WKN")
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.ToTable("Aktie");
                });

            modelBuilder.Entity("Data.Model.AktieModel.Dividende", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("AktieID")
                        .HasColumnType("integer");

                    b.Property<double>("Betrag")
                        .HasColumnType("double precision");

                    b.Property<DateTime>("Datum")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("ID");

                    b.HasIndex("AktieID");

                    b.ToTable("Dividende");
                });

            modelBuilder.Entity("Data.Model.AktieModel.Dividende", b =>
                {
                    b.HasOne("Data.Model.AktieModel.Aktie", "Aktie")
                        .WithMany("Dividenden")
                        .HasForeignKey("AktieID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
