﻿// <auto-generated />
using MbCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MbCore.Migrations
{
    [DbContext(typeof(MatrikelBrowserCTX))]
    [Migration("20241219055252_VariousRenames")]
    partial class VariousRenames
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.0");

            modelBuilder.Entity("AEM.ArchiveDTO", b =>
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

            modelBuilder.Entity("AEM.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("BookInfoLink")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("BookType")
                        .HasColumnType("INTEGER");

                    b.Property<string>("PageLinkPrefix")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("ParishId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("RefId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ParishId");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("AEM.CountryDTO", b =>
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

            modelBuilder.Entity("AEM.Page", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("BookId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Folio")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ImageLink")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.ToTable("Pages");
                });

            modelBuilder.Entity("AEM.ParishDTO", b =>
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

            modelBuilder.Entity("AEM.ArchiveDTO", b =>
                {
                    b.HasOne("AEM.CountryDTO", "Country")
                        .WithMany("Archives")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");
                });

            modelBuilder.Entity("AEM.Book", b =>
                {
                    b.HasOne("AEM.ParishDTO", "Parish")
                        .WithMany("Books")
                        .HasForeignKey("ParishId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Parish");
                });

            modelBuilder.Entity("AEM.Page", b =>
                {
                    b.HasOne("AEM.Book", "Book")
                        .WithMany("Pages")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");
                });

            modelBuilder.Entity("AEM.ParishDTO", b =>
                {
                    b.HasOne("AEM.ArchiveDTO", "Archive")
                        .WithMany("Parishes")
                        .HasForeignKey("ArchiveId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Archive");
                });

            modelBuilder.Entity("AEM.ArchiveDTO", b =>
                {
                    b.Navigation("Parishes");
                });

            modelBuilder.Entity("AEM.Book", b =>
                {
                    b.Navigation("Pages");
                });

            modelBuilder.Entity("AEM.CountryDTO", b =>
                {
                    b.Navigation("Archives");
                });

            modelBuilder.Entity("AEM.ParishDTO", b =>
                {
                    b.Navigation("Books");
                });
#pragma warning restore 612, 618
        }
    }
}
