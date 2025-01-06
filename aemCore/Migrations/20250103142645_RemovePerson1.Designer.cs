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
    [Migration("20250103142645_RemovePerson1")]
    partial class RemovePerson1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.0");

            modelBuilder.Entity("AEM.Tectonics.evnt", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("BookId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(21)
                        .HasColumnType("TEXT");

                    b.Property<double>("H")
                        .HasColumnType("REAL");

                    b.Property<string>("Notes")
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

                    b.HasDiscriminator().HasValue("evnt");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("AEM.Tectonics.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("BaptismDate")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("BirthDate")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("DeathDate")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("Living")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Occupation")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("State")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Persons");
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

            modelBuilder.Entity("AEM.Tectonics.BirthEvent", b =>
                {
                    b.HasBaseType("AEM.Tectonics.evnt");

                    b.Property<string>("ChildBaptizeDay")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ChildBirthday")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ChildName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Father")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("GodParent")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Mother")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasDiscriminator().HasValue("BirthEvent");
                });

            modelBuilder.Entity("AEM.Tectonics.MarriageEvent", b =>
                {
                    b.HasBaseType("AEM.Tectonics.evnt");

                    b.Property<int?>("BrideFatherId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("BrideId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("BrideMotherId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("GroomFatherId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("GroomId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("GroomMotherId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("WitnessesId")
                        .HasColumnType("INTEGER");

                    b.HasIndex("BrideFatherId");

                    b.HasIndex("BrideId");

                    b.HasIndex("BrideMotherId");

                    b.HasIndex("GroomFatherId");

                    b.HasIndex("GroomId");

                    b.HasIndex("GroomMotherId");

                    b.HasIndex("WitnessesId");

                    b.HasDiscriminator().HasValue("MarriageEvent");
                });

            modelBuilder.Entity("AEM.Tectonics.evnt", b =>
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

            modelBuilder.Entity("AEM.Tectonics.MarriageEvent", b =>
                {
                    b.HasOne("AEM.Tectonics.Person", "BrideFather")
                        .WithMany()
                        .HasForeignKey("BrideFatherId");

                    b.HasOne("AEM.Tectonics.Person", "Bride")
                        .WithMany()
                        .HasForeignKey("BrideId");

                    b.HasOne("AEM.Tectonics.Person", "BrideMother")
                        .WithMany()
                        .HasForeignKey("BrideMotherId");

                    b.HasOne("AEM.Tectonics.Person", "GroomFather")
                        .WithMany()
                        .HasForeignKey("GroomFatherId");

                    b.HasOne("AEM.Tectonics.Person", "Groom")
                        .WithMany()
                        .HasForeignKey("GroomId");

                    b.HasOne("AEM.Tectonics.Person", "GroomMother")
                        .WithMany()
                        .HasForeignKey("GroomMotherId");

                    b.HasOne("AEM.Tectonics.Person", "Witnesses")
                        .WithMany()
                        .HasForeignKey("WitnessesId");

                    b.Navigation("Bride");

                    b.Navigation("BrideFather");

                    b.Navigation("BrideMother");

                    b.Navigation("Groom");

                    b.Navigation("GroomFather");

                    b.Navigation("GroomMother");

                    b.Navigation("Witnesses");
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
