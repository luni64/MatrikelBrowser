﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable enable
using System;
using System.Collections.Generic;

namespace RMDatabase.Models;

public partial class CitationLinkTable
{
    public long LinkId { get; set; }

    public long? CitationId { get; set; }

    public long? OwnerType { get; set; }

    public long? OwnerId { get; set; }

    public long? SortOrder { get; set; }

    public string? Quality { get; set; }

    public long? IsPrivate { get; set; }

    public long? Flags { get; set; }

    public double? UtcmodDate { get; set; }
}