﻿// <auto-generated />
using System;
using MbCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AEM.Migrations
{
    [DbContext(typeof(MatrikelBrowserCTX))]
    [Migration("20250108135017_AddRemarkColumn")]
    partial class AddRemarkColumn
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.0");

            modelBuilder.Entity("AEM.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("BookId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Date1")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Date2")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Date3")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Date4")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("EventType")
                        .HasColumnType("INTEGER");

                    b.Property<double>("H")
                        .HasColumnType("REAL");

                    b.Property<string>("Notes")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Occupation1")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Occupation2")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Occupation3")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Person1")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Person2")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Person3")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Person4")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Person5")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Person6")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Person7")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Remarks")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("SheetNr")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Transcript")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("W")
                        .HasColumnType("REAL");

                    b.Property<double>("X")
                        .HasColumnType("REAL");

                    b.Property<double>("Y")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("AEM.Tectonics.SettingsEntry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("SettingsTable");
                });

            modelBuilder.Entity("MbCore.Archive", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ArchiveType")
                        .HasColumnType("INTEGER");

                    b.Property<string>("BookInfoUrl")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("CountryId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("REFID")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ViewerUrl")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("Archives");
                });

            modelBuilder.Entity("MbCore.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("BookInfoLink")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("BookType")
                        .HasColumnType("INTEGER");

                    b.Property<DateOnly?>("EndDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("ImageLinkPrefix")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("ParishId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("RefId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateOnly?>("StartDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ParishId");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("MbCore.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("MbCore.Page", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("BookId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Folio")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ImageId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.ToTable("Pages");
                });

            modelBuilder.Entity("MbCore.Parish", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ArchiveId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("BookBaseUrl")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Church")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Place")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("RefId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ArchiveId");

                    b.ToTable("Parishes");
                });

            modelBuilder.Entity("AEM.Event", b =>
                {
                    b.HasOne("MbCore.Book", "Book")
                        .WithMany("Events")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");
                });

            modelBuilder.Entity("MbCore.Archive", b =>
                {
                    b.HasOne("MbCore.Country", "Country")
                        .WithMany("Archives")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");
                });

            modelBuilder.Entity("MbCore.Book", b =>
                {
                    b.HasOne("MbCore.Parish", "Parish")
                        .WithMany("Books")
                        .HasForeignKey("ParishId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Parish");
                });

            modelBuilder.Entity("MbCore.Page", b =>
                {
                    b.HasOne("MbCore.Book", "Book")
                        .WithMany("Pages")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");
                });

            modelBuilder.Entity("MbCore.Parish", b =>
                {
                    b.HasOne("MbCore.Archive", "Archive")
                        .WithMany("Parishes")
                        .HasForeignKey("ArchiveId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Archive");
                });

            modelBuilder.Entity("MbCore.Archive", b =>
                {
                    b.Navigation("Parishes");
                });

            modelBuilder.Entity("MbCore.Book", b =>
                {
                    b.Navigation("Events");

                    b.Navigation("Pages");
                });

            modelBuilder.Entity("MbCore.Country", b =>
                {
                    b.Navigation("Archives");
                });

            modelBuilder.Entity("MbCore.Parish", b =>
                {
                    b.Navigation("Books");
                });
#pragma warning restore 612, 618
        }
    }
}
