﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable enable
using System;
using System.Collections.Generic;

namespace RMDatabase.Models;

public partial class Place
{
    public long PlaceId { get; set; }
    public long PlaceType { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Abbrev { get; set; } = string.Empty;
    public string Normalized { get; set; } = string.Empty;
    public long Latitude { get; set; }
    public long Longitude { get; set; }
    public long LatLongExact { get; set; }
    public long MasterId { get; set; }
    public string Note { get; set; } = string.Empty;
    public string Reverse { get; set; } = string.Empty; 
    public long FsId { get; set; }
    public long AnId { get; set; }
    public double UtcmodDate { get; set; }

    public virtual ICollection<PlaceWebTag> WebTags { get; set; } = [];

    public override string ToString() => Name;
}