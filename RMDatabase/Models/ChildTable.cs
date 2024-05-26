﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable enable
using System;
using System.Collections.Generic;

namespace RMDatabase.Models;

public enum RelationShip
{
    Birth = 0,
    Adopted = 1,
    Step = 2,
    Foster = 3,
    Related = 4,
    Guardian = 5,
    Sealed,
    Unknown
}

public partial class ChildTable
{
    public long RecId { get; set; }

    public long ChildId { get; set; }

    public long FamilyId { get; set; }

    public RelationShip RelFather { get; set; }

    public RelationShip RelMother { get; set; }

    public long ChildOrder { get; set; }

    public bool IsPrivate { get; set; } = false;

    public long ProofFather { get; set; }

    public long ProofMother { get; set; }

    public string Note { get; set; } = string.Empty;

    public double UtcmodDate { get; set; } = DateTime.Now.toUTCModDate();

    public virtual Person person { get; set; }
    public virtual Family family { get; set; }     
}