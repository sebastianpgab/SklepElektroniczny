﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SklepElektroniczny1501.Entities;

namespace SklepElektroniczny1501.Migrations
{
    [DbContext(typeof(SklepDbContext))]
    [Migration("20230217223542_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.32")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SklepElektroniczny1501.Entities.Kategoria", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nazwa")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Kategorie");
                });

            modelBuilder.Entity("SklepElektroniczny1501.Entities.Produkt", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Cena")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("IloscDostepna")
                        .HasColumnType("int");

                    b.Property<string>("Model")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nazwa")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Opis")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Produkty");
                });

            modelBuilder.Entity("SklepElektroniczny1501.Entities.ProduktKategoria", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("IdKategoria")
                        .HasColumnType("int");

                    b.Property<int>("IdProduktu")
                        .HasColumnType("int");

                    b.Property<int?>("KategoriaId")
                        .HasColumnType("int");

                    b.Property<int?>("ProduktId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("KategoriaId");

                    b.HasIndex("ProduktId");

                    b.ToTable("ProduktKatergorie");
                });

            modelBuilder.Entity("SklepElektroniczny1501.Entities.Zamowienie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DataZamowienia")
                        .HasColumnType("datetime2");

                    b.Property<string>("NumerZamowienia")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Zmowienia");
                });

            modelBuilder.Entity("SklepElektroniczny1501.Entities.ZamowienieProdukt", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Cena")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("IdProdukt")
                        .HasColumnType("int");

                    b.Property<int>("IdZamowienie")
                        .HasColumnType("int");

                    b.Property<int>("Ilosc")
                        .HasColumnType("int");

                    b.Property<int?>("ProduktId")
                        .HasColumnType("int");

                    b.Property<int?>("ZamowienieId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProduktId");

                    b.HasIndex("ZamowienieId");

                    b.ToTable("ZamowienieProdukty");
                });

            modelBuilder.Entity("SklepElektroniczny1501.Entities.ProduktKategoria", b =>
                {
                    b.HasOne("SklepElektroniczny1501.Entities.Kategoria", "Kategoria")
                        .WithMany("ProduktKategorie")
                        .HasForeignKey("KategoriaId");

                    b.HasOne("SklepElektroniczny1501.Entities.Produkt", "Produkt")
                        .WithMany("ProduktKategorie")
                        .HasForeignKey("ProduktId");
                });

            modelBuilder.Entity("SklepElektroniczny1501.Entities.ZamowienieProdukt", b =>
                {
                    b.HasOne("SklepElektroniczny1501.Entities.Produkt", "Produkt")
                        .WithMany("ZamowienieProdukty")
                        .HasForeignKey("ProduktId");

                    b.HasOne("SklepElektroniczny1501.Entities.Zamowienie", "Zamowienie")
                        .WithMany("ZamowienieProdukty")
                        .HasForeignKey("ZamowienieId");
                });
#pragma warning restore 612, 618
        }
    }
}
