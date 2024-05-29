﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable enable
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;


namespace RMDatabase.Models;

public partial class rmContext : DbContext
{
    public rmContext(DbContextOptions<rmContext> options)
        : base(options)
    {
    }

    #region Models

    public virtual DbSet<AddressLinkTable> AddressLinkTables { get; set; }
    public virtual DbSet<Address> AddressTable { get; set; }
    public virtual DbSet<AncestryTable> AncestryTables { get; set; }
    public virtual DbSet<ChildInfo> ChildTable { get; set; }
    public virtual DbSet<CitationLinkTable> CitationLinkTables { get; set; }
    public virtual DbSet<Citation> Citations { get; set; }
    public virtual DbSet<ConfigTable> ConfigTables { get; set; }
    public virtual DbSet<Event> Events { get; set; }
    public virtual DbSet<ExclusionTable> ExclusionTables { get; set; }
    public virtual DbSet<FactType> FactTypeTables { get; set; }
    public virtual DbSet<FamilySearchTable> FamilySearchTables { get; set; }
    public virtual DbSet<Family> Families { get; set; }
    public virtual DbSet<Fantable> Fantables { get; set; }
    public virtual DbSet<FantypeTable> FantypeTables { get; set; }
    public virtual DbSet<GroupEntry> GroupEntries { get; set; }
    public virtual DbSet<MediaLinkTable> MediaLinkTables { get; set; }
    public virtual DbSet<Medium> MultimediaTables { get; set; }
    public virtual DbSet<Name> Names { get; set; }
    public virtual DbSet<Payload> PayloadTables { get; set; }
    public virtual DbSet<Person> Persons { get; set; }
    public virtual DbSet<Place> Places { get; set; }
    public virtual DbSet<RoleTable> RoleTables { get; set; }
    public virtual DbSet<Source> Sources { get; set; }
    public virtual DbSet<SourceTemplateTable> SourceTemplateTables { get; set; }
    public virtual DbSet<TagTable> TagTables { get; set; }
    public virtual DbSet<TaskLinkTable> TaskLinkTables { get; set; }
    public virtual DbSet<Task> TaskTables { get; set; }
    public virtual DbSet<WebTag> WebTags { get; set; }
    public virtual DbSet<WitnessTable> WitnessTables { get; set; }

    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AddressLinkTable>(entity =>
        {
            entity.ToTable("AddressLinkTable");
            entity.HasKey(e => e.LinkId);
            entity.HasDiscriminator<long>(e => e.OwnerType).HasValue(-1);  // dummy value, won't compile without value

            entity.Property(e => e.LinkId).HasColumnName("LinkID").ValueGeneratedNever();
            entity.Property(e => e.AddressId).HasColumnName("AddressID");
            entity.Property(e => e.OwnerId).HasColumnName("OwnerID");

            entity.Property(e => e.ChangeDate).HasColumnName("UTCModDate").HasColumnType("FLOAT").HasConversion(e => e.toDateTime(), e => e.toUTCModDate());
        }); // dicriminator based M:N relationship  https://stackoverflow.com/a/77587113/3866165
        modelBuilder.Entity<AddressPerson>(entity =>
        {
            entity.ToTable("AddressLinkTable").HasDiscriminator<long>(e => e.OwnerType).HasValue(0);
        });
        modelBuilder.Entity<AddressFamily>(entity =>
        {
            entity.ToTable("AddressLinkTable")
            .HasDiscriminator<long>(e => e.OwnerType).HasValue(1);
        });
        modelBuilder.Entity<RepositorySource>(entity =>
        {
            entity.ToTable("AddressLinkTable").HasDiscriminator<long>(e => e.OwnerType).HasValue(3);


        });

        modelBuilder.Entity<Address>(entity =>
        {
            entity.ToTable("AddressTable");
            entity.HasKey(e => e.AddressId);

            entity.Property(e => e.AddressId).ValueGeneratedNever().HasColumnName("AddressID");
            entity.Property(e => e.Url).HasColumnName("URL");
            entity.Property(e => e.ChangeDate).HasColumnName("UTCModDate").HasColumnType("FLOAT").HasConversion(e => e.toUTCModDate(), e => e.toDateTime());

            entity.HasIndex(e => e.Name, "idxAddressName");
        });

        modelBuilder.Entity<AncestryTable>(entity =>
        {
            entity.ToTable("AncestryTable");
            entity.HasKey(e => e.LinkId);

            entity.Property(e => e.LinkId).HasColumnName("LinkID").ValueGeneratedOnAdd();
            entity.Property(e => e.AnDate).HasColumnType("FLOAT").HasColumnName("anDate");
            entity.Property(e => e.AnId).HasColumnName("anID");
            entity.Property(e => e.AnVersion).HasColumnName("anVersion");
            entity.Property(e => e.RmId).HasColumnName("rmID");
            entity.Property(e => e.ChangeDate).HasColumnName("UTCModDate").HasColumnType("FLOAT").HasConversion(e => e.toUTCModDate(), e => e.toDateTime());

            entity.HasIndex(e => e.RmId, "idxLinkAncestryRmId");
            entity.HasIndex(e => e.AnId, "idxLinkAncestryanID");
        });

        modelBuilder.Entity<ChildInfo>(entity =>
        {
            entity.ToTable("ChildTable");
            entity.HasKey(e => e.RecId);

            entity.Property(e => e.RecId).HasColumnName("RecID").ValueGeneratedOnAdd();
            entity.Property(e => e.ChildId).HasColumnName("ChildID");
            entity.Property(e => e.FamilyId).HasColumnName("FamilyID");
            entity.Property(e => e.RelFather).HasConversion<long>();
            entity.Property(e => e.RelMother).HasConversion<long>();
            entity.Property(e => e.IsPrivate).HasConversion<long>();
            entity.Property(e => e.ChangeDate).HasColumnName("UTCModDate").HasColumnType("FLOAT").HasConversion(e => e.toUTCModDate(), e => e.toDateTime());

            entity.HasOne(ct => ct.Family).WithMany(ct => ct.ChildInfos).HasForeignKey(ct => ct.FamilyId);
            entity.HasOne(ct => ct.Child).WithMany(p => p.ParentRelations).HasForeignKey(ct => ct.ChildId);
            //entity.HasOne(ct=>ct.Child).WithMany().HasForeignKey(ct=>ct.ChildId)

            entity.HasIndex(e => e.FamilyId, "idxChildFamilyID");
            entity.HasIndex(e => e.ChildId, "idxChildID");
            entity.HasIndex(e => e.ChildOrder, "idxChildOrder");

        });

        modelBuilder.Entity<Citation>(entity =>
        {
            entity.ToTable("CitationTable");
            entity.HasKey(e => e.CitationId);

            entity.Property(e => e.CitationId).HasColumnName("CitationID").ValueGeneratedOnAdd();
            entity.Property(e => e.SourceId).HasColumnName("SourceID");
            entity.Property(e => e.ChangeDate).HasColumnName("UTCModDate").HasColumnType("FLOAT").HasConversion(e => e.toUTCModDate(), e => e.toDateTime());

            entity.HasOne(ct => ct.Source).WithMany(s => s.Citations).HasForeignKey(ct => ct.SourceId);
            entity.HasOne(c => c.CitationDetails).WithMany().HasForeignKey(e => e.CitationId);
            entity.HasMany(e => e.WebTags).WithOne().HasForeignKey(e => e.OwnerId);

            entity.HasMany(p => p.Media).WithMany().UsingEntity<MediaCitationLink>(
                l => l.HasOne<Medium>().WithMany().HasForeignKey(e => e.MediaId),
                r => r.HasOne<Citation>().WithMany().HasForeignKey(e => e.OwnerId));

            entity.HasIndex(e => e.CitationName, "idxCitationName");
            entity.HasIndex(e => e.SourceId, "idxCitationSourceID");
        });

        modelBuilder.Entity<CitationLinkTable>(entity =>
        {
            entity.ToTable("CitationLinkTable");
            entity.HasKey(e => e.LinkId);
            entity.HasDiscriminator<long>(e => e.OwnerType).HasValue(-1);

            entity.Property(e => e.LinkId).HasColumnName("LinkID").ValueGeneratedOnAdd();
            entity.Property(e => e.CitationId).HasColumnName("CitationID");
            entity.Property(e => e.OwnerId).HasColumnName("OwnerID");
            entity.Property(e => e.ChangeDate).HasColumnName("UTCModDate").HasColumnType("FLOAT").HasConversion(e => e.toUTCModDate(), e => e.toDateTime());

            entity.HasIndex(e => e.OwnerId, "idxCitationLinkOwnerID");
        });
        modelBuilder.Entity<PersonCitationLink>(entity =>
        {
            entity.ToTable("CitationLinkTable").HasDiscriminator<long>(e => e.OwnerType).HasValue(0);
        });
        modelBuilder.Entity<FamilyCitationLink>(entity =>
        {
            entity.ToTable("CitationLinkTable").HasDiscriminator<long>(e => e.OwnerType).HasValue(1);
        });
        modelBuilder.Entity<EventCitationLink>(entity =>
        {
            entity.ToTable("CitationLinkTable").HasDiscriminator<long>(e => e.OwnerType).HasValue(2);
        });
        modelBuilder.Entity<NameCitationLink>(entity =>
        {
            entity.ToTable("CitationLinkTable").HasDiscriminator<long>(e => e.OwnerType).HasValue(7);
        });

        modelBuilder.Entity<ConfigTable>(entity =>
        {
            entity.ToTable("ConfigTable");
            entity.HasKey(e => e.RecId);
            entity.Property(e => e.RecId).HasColumnName("RecID").ValueGeneratedOnAdd();
            entity.Property(e => e.ChangeDate).HasColumnName("UTCModDate").HasColumnType("FLOAT").HasConversion(e => e.toUTCModDate(), e => e.toDateTime());

            entity.HasIndex(e => e.RecType, "idxRecType");
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.ToTable("EventTable");
            entity.HasKey(e => e.EventId);
            entity.Property(e => e.EventId).HasColumnName("EventID").ValueGeneratedOnAdd();

            entity.HasDiscriminator<long>(e => e.OwnerType).HasValue(-1);

            entity.Property(e => e.FamilyId).HasColumnName("FamilyID");
            entity.Property(e => e.OwnerId).HasColumnName("OwnerID");
            entity.Property(e => e.PlaceId).HasColumnName("PlaceID");
            entity.Property(e => e.SiteId).HasColumnName("SiteID");
            entity.Property(e => e.SortDate).HasColumnType("BIGINT");
            entity.Property(e => e.ChangeDate).HasColumnName("UTCModDate").HasColumnType("FLOAT").HasConversion(e => e.toUTCModDate(), e => e.toDateTime());
            entity.Property(e => e.Date).HasConversion(rmd => rmd.toRmDatestring(), ds => new rmSharp.RMDate(ds));

            entity.HasOne(e => e.FactType).WithMany().HasForeignKey(f => f.EventType);
            entity.HasOne(e => e.Place).WithMany().HasForeignKey(p => p.PlaceId);

            entity.HasMany(p => p.Citations).WithMany().UsingEntity<EventCitationLink>(
               l => l.HasOne<Citation>().WithMany().HasForeignKey(e => e.CitationId),
               r => r.HasOne<Event>().WithMany().HasForeignKey(e => e.OwnerId));

            entity.HasMany(p => p.Media).WithMany().UsingEntity<MediaPersonLink>(
              l => l.HasOne<Medium>().WithMany().HasForeignKey(e => e.MediaId),
              r => r.HasOne<Event>().WithMany().HasForeignKey(e => e.OwnerId));

            entity.HasMany(p => p.Tasks).WithMany().UsingEntity<TaskEvent>(
                l => l.HasOne<Task>().WithMany().HasForeignKey(e => e.TaskId),
                r => r.HasOne<Event>().WithMany().HasForeignKey(e => e.OwnerId));

            entity.HasIndex(e => new { e.OwnerId, e.SortDate }, "idxOwnerDate");
            entity.HasIndex(e => new { e.OwnerId, e.EventType }, "idxOwnerEvent");
        });
        modelBuilder.Entity<PersonEvent>(entity =>
        {
            entity.ToTable("EventTable");
            entity.HasDiscriminator<long>(e => e.OwnerType).HasValue(0);
            //entity.HasMany(p => p.Persons).WithMany(x => x.Events);
        });
        modelBuilder.Entity<FamilyEvent>(entity =>
        {
            entity.ToTable("EventTable");
            entity.HasDiscriminator<long>(e => e.OwnerType).HasValue(1);
        });

        modelBuilder.Entity<ExclusionTable>(entity =>
        {
            entity.ToTable("ExclusionTable");
            entity.HasKey(e => e.RecId);
            entity.Property(e => e.RecId).HasColumnName("RecID").ValueGeneratedOnAdd();

            entity.Property(e => e.Id1).HasColumnName("ID1");
            entity.Property(e => e.Id2).HasColumnName("ID2");
            entity.Property(e => e.ChangeDate).HasColumnName("UTCModDate").HasColumnType("FLOAT").HasConversion(e => e.toUTCModDate(), e => e.toDateTime());

            entity.HasIndex(e => new { e.ExclusionType, e.Id1, e.Id2 }, "idxExclusionIndex").IsUnique();
        });

        modelBuilder.Entity<FactType>(entity =>
        {
            entity.ToTable("FactTypeTable");
            entity.HasKey(e => e.FactTypeId);
            entity.Property(e => e.FactTypeId).HasColumnName("FactTypeID").ValueGeneratedOnAdd();

            entity.Property(e => e.ChangeDate).HasColumnName("UTCModDate").HasColumnType("FLOAT").HasConversion(e => e.toUTCModDate(), e => e.toDateTime());

            entity.HasMany(f => f.Events).WithOne(x => x.FactType).HasForeignKey(f => f.EventType);

            entity.HasIndex(e => e.Abbrev, "idxFactTypeAbbrev");
            entity.HasIndex(e => e.GedcomTag, "idxFactTypeGedcomTag");
            entity.HasIndex(e => e.Name, "idxFactTypeName");
        });

        modelBuilder.Entity<FamilySearchTable>(entity =>
        {
            entity.ToTable("FamilySearchTable");
            entity.HasKey(e => e.LinkId);
            entity.Property(e => e.LinkId).HasColumnName("LinkID").ValueGeneratedOnAdd();

            entity.Property(e => e.FsDate).HasColumnType("FLOAT").HasColumnName("fsDate");
            entity.Property(e => e.FsId).HasColumnName("fsID");
            entity.Property(e => e.FsVersion).HasColumnName("fsVersion");
            entity.Property(e => e.RmId).HasColumnName("rmID");
            entity.Property(e => e.ChangeDate).HasColumnName("UTCModDate").HasColumnType("FLOAT").HasConversion(e => e.toUTCModDate(), e => e.toDateTime());

            entity.HasIndex(e => e.RmId, "idxLinkRmId");
            entity.HasIndex(e => e.FsId, "idxLinkfsID");
        });

        modelBuilder.Entity<Family>(entity =>
        {
            entity.ToTable("FamilyTable");
            entity.HasKey(e => e.FamilyId);

            entity.Property(e => e.FamilyId).HasColumnName("FamilyID").ValueGeneratedOnAdd();
            entity.Property(e => e.ChildId).HasColumnName("ChildID");
            entity.Property(e => e.HusbandId).HasColumnName("FatherID");
            entity.Property(e => e.WifeId).HasColumnName("MotherID");
            entity.Property(e => e.IsPrivate).HasConversion<long>();
            entity.Property(e => e.ChangeDate).HasColumnName("UTCModDate").HasColumnType("FLOAT").HasConversion(v => v.toUTCModDate(), v => v.toDateTime());

            entity.HasOne(f => f.Wife).WithOne().HasForeignKey<Family>(p => p.WifeId);
            entity.HasOne(f => f.Husband).WithOne().HasForeignKey<Family>(p => p.HusbandId);

            entity.HasOne(f => f.Husband).WithMany("_familiesM").HasForeignKey(f => f.HusbandId);
            entity.HasOne(f => f.Wife).WithMany("_familiesF").HasForeignKey(f => f.WifeId);

            entity.HasMany(f => f.Events).WithOne().HasForeignKey(e => e.OwnerId);

            entity.HasMany(p => p.Citations).WithMany().UsingEntity<FamilyCitationLink>(
             l => l.HasOne<Citation>().WithMany().HasForeignKey(e => e.CitationId),
             r => r.HasOne<Family>().WithMany().HasForeignKey(e => e.OwnerId));

            entity.HasMany(p => p.Tasks).WithMany().UsingEntity<TaskFamily>(
            l => l.HasOne<Task>().WithMany().HasForeignKey(e => e.TaskId),
            r => r.HasOne<Family>().WithMany().HasForeignKey(e => e.OwnerId));

            //entity.HasMany(e => e.Children).WithMany().UsingEntity<ChildTable>(
            //    r => r.HasOne<Person>().WithMany().HasForeignKey(e => e.ChildId),
            //    l => l.HasOne<Family>().WithMany().HasForeignKey(e => e.FamilyId));

            entity.HasIndex(e => e.HusbandId, "idxFamilyFatherID");
            entity.HasIndex(e => e.WifeId, "idxFamilyMotherID");

        });

        modelBuilder.Entity<Fantable>(entity =>
        {
            entity.ToTable("FANTable");
            entity.HasKey(e => e.FanId);

            entity.Property(e => e.FanId).HasColumnName("FanID").ValueGeneratedOnAdd();
            entity.Property(e => e.FanTypeId).HasColumnName("FanTypeID");
            entity.Property(e => e.Id1).HasColumnName("ID1");
            entity.Property(e => e.Id2).HasColumnName("ID2");
            entity.Property(e => e.PlaceId).HasColumnName("PlaceID");
            entity.Property(e => e.SiteId).HasColumnName("SiteID");
            entity.Property(e => e.SortDate).HasColumnType("BIGINT");
            entity.Property(e => e.ChangeDate).HasColumnName("UTCModDate").HasColumnType("FLOAT").HasConversion(e => e.toUTCModDate(), e => e.toDateTime());

            entity.HasIndex(e => e.Id1, "idxFanId1");
            entity.HasIndex(e => e.Id2, "idxFanId2");
        });

        modelBuilder.Entity<FantypeTable>(entity =>
        {
            entity.ToTable("FANTypeTable");
            entity.HasKey(e => e.FantypeId);

            entity.Property(e => e.FantypeId).HasColumnName("FANTypeID").ValueGeneratedOnAdd();
            entity.Property(e => e.ChangeDate).HasColumnName("UTCModDate").HasColumnType("FLOAT").HasConversion(e => e.toUTCModDate(), e => e.toDateTime());
            entity.HasIndex(e => e.Name, "idxFANTypeName");
        });

        modelBuilder.Entity<GroupEntry>(entity =>
        {
            entity.ToTable("GroupTable");
            entity.HasKey(e => e.RecId);

            entity.Property(e => e.RecId).HasColumnName("RecID").ValueGeneratedOnAdd();
            entity.Property(e => e.EndId).HasColumnName("EndID");
            entity.Property(e => e.GroupId).HasColumnName("GroupID");
            entity.Property(e => e.StartId).HasColumnName("StartID");
            entity.Property(e => e.ChangeDate).HasColumnName("UTCModDate").HasColumnType("FLOAT").HasConversion(e => e.toUTCModDate(), e => e.toDateTime());
        });

        modelBuilder.Entity<MediaLinkTable>(entity =>
        {
            entity.ToTable("MediaLinkTable");
            entity.HasKey(e => e.LinkId);
            entity.Property(e => e.LinkId).HasColumnName("LinkID").ValueGeneratedOnAdd();

            entity.HasDiscriminator<long>(e => e.OwnerType).HasValue(-1);

            entity.Property(e => e.MediaId).HasColumnName("MediaID");
            entity.Property(e => e.OwnerId).HasColumnName("OwnerID");
            entity.Property(e => e.ChangeDate).HasColumnName("UTCModDate").HasColumnType("FLOAT").HasConversion(e => e.toUTCModDate(), e => e.toDateTime()); entity.HasIndex(e => e.OwnerId, "idxMediaOwnerID");
        });
        modelBuilder.Entity<MediaPersonLink>(entity => entity.HasDiscriminator<long>(e => e.OwnerType).HasValue(0));
        modelBuilder.Entity<MediaEventLink>(entity => entity.HasDiscriminator<long>(e => e.OwnerType).HasValue(2));
        modelBuilder.Entity<MediaSourceLink>(entity => entity.HasDiscriminator<long>(e => e.OwnerType).HasValue(3));
        modelBuilder.Entity<MediaCitationLink>(entity => entity.HasDiscriminator<long>(e => e.OwnerType).HasValue(4));

        modelBuilder.Entity<Medium>(entity =>
        {
            entity.ToTable("MultimediaTable");
            entity.HasKey(e => e.MediaId);
            entity.Property(e => e.MediaId).HasColumnName("MediaID").ValueGeneratedOnAdd();

            entity.Property(e => e.SortDate).HasColumnType("BIGINT");
            entity.Property(e => e.Url).HasColumnName("URL");
            entity.Property(e => e.ChangeDate).HasColumnName("UTCModDate").HasColumnType("FLOAT").HasConversion(e => e.toUTCModDate(), e => e.toDateTime());
            entity.HasIndex(e => e.MediaFile, "idxMediaFile");
            entity.HasIndex(e => e.Url, "idxMediaURL");
        });

        modelBuilder.Entity<Name>(entity =>
        {
            entity.ToTable("NameTable");
            entity.HasKey(e => e.NameId);
            entity.Property(e => e.NameId).HasColumnName("NameID").ValueGeneratedOnAdd();

            entity.Property(e => e.GivenMp).HasColumnName("GivenMP");
            entity.Property(e => e.NicknameMp).HasColumnName("NicknameMP");
            entity.Property(e => e.OwnerId).HasColumnName("OwnerID");
            entity.Property(e => e.SortDate).HasColumnType("BIGINT");
            entity.Property(e => e.SurnameMp).HasColumnName("SurnameMP");
            entity.Property(e => e.ChangeDate).HasColumnName("UTCModDate").HasColumnType("FLOAT").HasConversion(e => e.toUTCModDate(), e => e.toDateTime());
            entity.Property(e => e.NameType).HasConversion<long>();
            entity.Property(e => e.IsPrimary).HasConversion<long>();
            entity.Property(e => e.IsPrivate).HasConversion<long>();

            entity.HasMany(p => p.Citations).WithMany().UsingEntity<PersonCitationLink>(
                l => l.HasOne<Citation>().WithMany().HasForeignKey(e => e.CitationId),
                r => r.HasOne<Name>().WithMany().HasForeignKey(e => e.OwnerId));

            entity.HasMany(p => p.Tasks).WithMany().UsingEntity<TaskName>(
             l => l.HasOne<Task>().WithMany().HasForeignKey(e => e.TaskId),
             r => r.HasOne<Name>().WithMany().HasForeignKey(e => e.OwnerId));

            entity.HasIndex(e => e.Given, "idxGiven");
            entity.HasIndex(e => e.GivenMp, "idxGivenMP");
            entity.HasIndex(e => e.OwnerId, "idxNameOwnerID");
            entity.HasIndex(e => e.IsPrimary, "idxNamePrimary");
            entity.HasIndex(e => e.Surname, "idxSurname");
            entity.HasIndex(e => new { e.Surname, e.Given, e.BirthYear, e.DeathYear }, "idxSurnameGiven");
            entity.HasIndex(e => new { e.SurnameMp, e.GivenMp, e.BirthYear, e.DeathYear }, "idxSurnameGivenMP");
            entity.HasIndex(e => e.SurnameMp, "idxSurnameMP");

        });

        modelBuilder.Entity<Payload>(entity =>
        {
            entity.ToTable("PayloadTable");
            entity.HasKey(e => e.RecId);
            entity.Property(e => e.RecId).HasColumnName("RecID").ValueGeneratedOnAdd();

            entity.HasDiscriminator<long>(e => e.OwnerType).HasValue(-1);  // dummy value, won't compile without value

            entity.Property(e => e.OwnerId).HasColumnName("OwnerID");
            entity.Property(e => e.ChangeDate).HasColumnName("UTCModDate").HasColumnType("FLOAT").HasConversion(e => e.toUTCModDate(), e => e.toDateTime());

            entity.HasIndex(e => e.RecType, "idxPayloadType");
        });
        modelBuilder.Entity<PayloadGroup2>(entity =>
        {
            entity.HasDiscriminator<long>(e => e.OwnerType).HasValue(20);
            entity.HasMany(e => e.Entries).WithOne().HasForeignKey(e => e.GroupId).HasPrincipalKey(e => e.OwnerId);
        });
        modelBuilder.Entity<PayloadSearch>(entity => entity.HasDiscriminator<long>(e => e.OwnerType).HasValue(8));

        modelBuilder.Entity<Person>(entity =>
            {
                entity.ToTable("PersonTable");
                entity.HasKey(e => e.PersonId);
                entity.Ignore(e => e.ChildRelations);
                entity.Ignore(e => e.Children);

                entity.Property(e => e.PersonId).HasColumnName("PersonID").ValueGeneratedOnAdd();
                entity.Property(e => e.SpouseId).HasColumnName("SpouseID");
                entity.Property(e => e.UniqueId).HasColumnName("UniqueID");
                entity.Property(e => e.ChangeDate).HasColumnName("UTCModDate").HasColumnType("FLOAT").HasConversion(v => v.toUTCModDate(), v => v.toDateTime());


                entity.Property(e => e.IsPrivate).HasConversion<long>();
                entity.Property(e => e.Living).HasConversion<long>();
                entity.Property(e => e.Sex).HasConversion<long>();

                entity.HasMany(e => e.WebTags).WithOne().HasForeignKey(e => e.OwnerId);

                entity.HasMany(p => p.Addresses).WithMany().UsingEntity<AddressPerson>(
                l => l.HasOne<Address>().WithMany().HasForeignKey(e => e.AddressId),
                        r => r.HasOne<Person>().WithMany().HasForeignKey(e => e.OwnerId));

                entity.HasMany(p => p.Citations).WithMany().UsingEntity<PersonCitationLink>(
                    l => l.HasOne<Citation>().WithMany().HasForeignKey(e => e.CitationId),
                    r => r.HasOne<Person>().WithMany().HasForeignKey(e => e.OwnerId));

                entity.HasMany(p => p.Media).WithMany().UsingEntity<MediaPersonLink>(
                   l => l.HasOne<Medium>().WithMany().HasForeignKey(e => e.MediaId),
                   r => r.HasOne<Person>().WithMany().HasForeignKey(e => e.OwnerId));

                entity.HasMany(p => p.Tasks).WithMany().UsingEntity<TaskPerson>(
                    l => l.HasOne<Task>().WithMany().HasForeignKey(e => e.TaskId),
                    r => r.HasOne<Person>().WithMany().HasForeignKey(e => e.OwnerId));


                entity.HasMany(p => p.Events).WithOne().HasForeignKey(e => e.OwnerId);
                //tity.HasMany(p => p.Events).WithMany(e=>e.Persons).UsingEntity()


                //l => l.HasOne<Event>().WithMany().HasForeignKey(e => e.OwnerId),
                //r => r.HasOne<Person>().WithMany().HasForeignKey(e => e.OwnerId));

                entity.HasMany(p => p.Names).WithOne().HasForeignKey(n => n.OwnerId);

            });

        modelBuilder.Entity<Place>(entity =>
        {
            entity.ToTable("PlaceTable");
            entity.HasKey(e => e.PlaceId);
            entity.Property(e => e.PlaceId).ValueGeneratedOnAdd();

            entity.Property(e => e.PlaceId).HasColumnName("PlaceID");
            entity.Property(e => e.AnId).HasColumnName("anID");
            entity.Property(e => e.FsId).HasColumnName("fsID");
            entity.Property(e => e.MasterId).HasColumnName("MasterID");
            entity.Property(e => e.ChangeDate).HasColumnName("UTCModDate").HasColumnType("FLOAT").HasConversion(e => e.toUTCModDate(), e => e.toDateTime());

            entity.HasMany(e => e.WebTags).WithOne().HasForeignKey(e => e.OwnerId);

            entity.HasMany(p => p.Tasks).WithMany().UsingEntity<TaskPlace>(
               l => l.HasOne<Task>().WithMany().HasForeignKey(e => e.TaskId),
               r => r.HasOne<Place>().WithMany().HasForeignKey(e => e.OwnerId));

            entity.HasIndex(e => e.Abbrev, "idxPlaceAbbrev");
            entity.HasIndex(e => e.Name, "idxPlaceName");
            entity.HasIndex(e => e.Reverse, "idxReversePlaceName");
        });

        modelBuilder.Entity<RoleTable>(entity =>
        {
            entity.ToTable("RoleTable");
            entity.HasKey(e => e.RoleId);
            entity.Property(e => e.RoleId).HasColumnName("RoleID").ValueGeneratedOnAdd();

            entity.Property(e => e.ChangeDate).HasColumnName("UTCModDate").HasColumnType("FLOAT").HasConversion(e => e.toUTCModDate(), e => e.toDateTime()); entity.HasIndex(e => e.EventType, "idxRoleEventType");
        });

        modelBuilder.Entity<Source>(entity =>
        {
            entity.HasKey(e => e.SourceId);
            entity.ToTable("SourceTable");

            entity.Property(e => e.SourceId).HasColumnName("SourceID").ValueGeneratedOnAdd();
            entity.Property(e => e.TemplateId).HasColumnName("TemplateID");
            entity.Property(e => e.ChangeDate).HasColumnName("UTCModDate").HasColumnType("FLOAT").HasConversion(e => e.toUTCModDate(), e => e.toDateTime());

            entity.HasMany(e => e.WebTags).WithOne().HasForeignKey(e => e.OwnerId);

            entity.HasMany(s => s.Repositories).WithMany().UsingEntity<RepositorySource>(
                l => l.HasOne<Address>().WithMany().HasForeignKey(e => e.AddressId),
                r => r.HasOne<Source>().WithMany().HasForeignKey(e => e.OwnerId));

            entity.HasMany(p => p.Media).WithMany().UsingEntity<MediaSourceLink>(
             l => l.HasOne<Medium>().WithMany().HasForeignKey(e => e.MediaId),
             r => r.HasOne<Source>().WithMany().HasForeignKey(e => e.OwnerId));

            entity.HasIndex(e => e.Name, "idxSourceName");
        });

        modelBuilder.Entity<SourceTemplateTable>(entity =>
        {
            entity.ToTable("SourceTemplateTable");
            entity.HasKey(e => e.TemplateId);
            entity.Property(e => e.TemplateId).HasColumnName("TemplateID").ValueGeneratedOnAdd();

            entity.Property(e => e.ChangeDate).HasColumnName("UTCModDate").HasColumnType("FLOAT").HasConversion(e => e.toUTCModDate(), e => e.toDateTime());

            entity.HasIndex(e => e.Name, "idxSourceTemplateName");
        });

        modelBuilder.Entity<TagTable>(entity =>
        {
            entity.ToTable("TagTable");
            entity.HasKey(e => e.TagId);
            entity.Property(e => e.TagId).HasColumnName("TagID").ValueGeneratedOnAdd();

            entity.Property(e => e.ChangeDate).HasColumnName("UTCModDate").HasColumnType("FLOAT").HasConversion(e => e.toUTCModDate(), e => e.toDateTime());
            entity.HasIndex(e => e.TagType, "idxTagType");
        });

        modelBuilder.Entity<TaskLinkTable>(entity =>
        {
            entity.ToTable("TaskLinkTable");
            entity.HasKey(e => e.LinkId);
            entity.Property(e => e.LinkId).HasColumnName("LinkID").ValueGeneratedOnAdd();

            entity.HasDiscriminator<long>(e => e.OwnerType).HasValue(-1);

            entity.Property(e => e.OwnerId).HasColumnName("OwnerID");
            entity.Property(e => e.TaskId).HasColumnName("TaskID");
            entity.Property(e => e.ChangeDate).HasColumnName("UTCModDate").HasColumnType("FLOAT").HasConversion(e => e.toUTCModDate(), e => e.toDateTime());


            entity.HasIndex(e => e.OwnerId, "idxTaskOwnerID");
        });
        modelBuilder.Entity<TaskPerson>(e =>
        {
            e.HasDiscriminator<long>(e => e.OwnerType).HasValue(0);
        });
        modelBuilder.Entity<TaskFamily>(e =>
        {
            e.HasDiscriminator<long>(e => e.OwnerType).HasValue(1);
        });
        modelBuilder.Entity<TaskEvent>(e =>
        {
            e.HasDiscriminator<long>(e => e.OwnerType).HasValue(2);
        });
        modelBuilder.Entity<TaskPlace>(e =>
        {
            e.HasDiscriminator<long>(e => e.OwnerType).HasValue(5);
        });
        modelBuilder.Entity<TaskName>(e =>
        {
            e.HasDiscriminator<long>(e => e.OwnerType).HasValue(7);
        });
        modelBuilder.Entity<TaskFolder>(e =>
        {
            e.HasDiscriminator<long>(e => e.OwnerType).HasValue(18);
        });
        modelBuilder.Entity<TaskAssociation>(e =>
        {
            e.HasDiscriminator<long>(e => e.OwnerType).HasValue(19);
        });

        modelBuilder.Entity<Task>(entity =>
        {
            entity.ToTable("TaskTable");
            entity.HasKey(e => e.TaskId);
            entity.Property(e => e.TaskId).HasColumnName("TaskID").ValueGeneratedOnAdd();

            entity.Property(e => e.SortDate1).HasColumnType("BIGINT");
            entity.Property(e => e.SortDate2).HasColumnType("BIGINT");
            entity.Property(e => e.SortDate3).HasColumnType("BIGINT");
            entity.Property(e => e.ChangeDate).HasColumnName("UTCModDate").HasColumnType("FLOAT").HasConversion(e => e.toUTCModDate(), e => e.toDateTime());

            entity.HasIndex(e => e.Name, "idxTaskName");
        });

        modelBuilder.Entity<WebTag>(entity =>
        {
            entity.ToTable("URLTable");
            entity.HasKey(e => e.LinkId);
            entity.Property(e => e.LinkId).HasColumnName("LinkID").ValueGeneratedOnAdd();

            entity.HasDiscriminator<long>(e => e.OwnerType).HasValue(-1);

            entity.Property(e => e.OwnerId).HasColumnName("OwnerID");
            entity.Property(e => e.Url).HasColumnName("URL");
            entity.Property(e => e.ChangeDate).HasColumnName("UTCModDate").HasColumnType("FLOAT").HasConversion(e => e.toUTCModDate(), e => e.toDateTime());

        });
        modelBuilder.Entity<PersonWebTag>(entity => entity.HasDiscriminator<long>(e => e.OwnerType).HasValue(0));
        modelBuilder.Entity<SourceWebTag>(entity => entity.HasDiscriminator<long>(e => e.OwnerType).HasValue(3));
        modelBuilder.Entity<CitationWebTag>(entity => entity.HasDiscriminator<long>(e => e.OwnerType).HasValue(4));
        modelBuilder.Entity<PlaceWebTag>(entity => entity.HasDiscriminator<long>(e => e.OwnerType).HasValue(5));

        modelBuilder.Entity<WitnessTable>(entity =>
        {
            entity.ToTable("WitnessTable");
            entity.HasKey(e => e.WitnessId);
            entity.Property(e => e.WitnessId).HasColumnName("WitnessID").ValueGeneratedOnAdd();

            entity.Property(e => e.EventId).HasColumnName("EventID");
            entity.Property(e => e.PersonId).HasColumnName("PersonID");
            entity.Property(e => e.ChangeDate).HasColumnName("UTCModDate").HasColumnType("FLOAT").HasConversion(e => e.toUTCModDate(), e => e.toDateTime());
            
            entity.HasIndex(e => e.EventId, "idxWitnessEventID");
            entity.HasIndex(e => e.PersonId, "idxWitnessPersonID");
        });

        //OnModelCreatingPartial(modelBuilder);
    }

}