﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable enable
using System;
using System.Collections.Generic;

namespace RMDatabase.Models;

public partial class ChildTable
{
    public long RecId { get; set; }

    public long? ChildId { get; set; }

    public long? FamilyId { get; set; }

    public long? RelFather { get; set; }

    public long? RelMother { get; set; }

    public long? ChildOrder { get; set; }

    public long? IsPrivate { get; set; }

    public long? ProofFather { get; set; }

    public long? ProofMother { get; set; }

    public string? Note { get; set; }

    public double? UtcmodDate { get; set; }
}