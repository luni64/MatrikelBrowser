﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable enable
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;

namespace RMDatabase.Models;

public partial class AddressLinkTable
{
    public long LinkId { get; set; }
    public long OwnerType { get; set; }
    public long? AddressId { get; set; }
    public long? OwnerId { get; set; }
    public long? AddressNum { get; set; }
    public string? Details { get; set; }
    public double UtcmodDate { get; set; } = DateTime.Now.toUTCModDate();
}

// The following (virtual) Tables are requiered to model a dicriminator based M:N relationship
// see: https://stackoverflow.com/a/77587113/3866165

public class AddressPerson : AddressLinkTable { }
public class AddressFamily : AddressLinkTable { }
public class RepositorySource : AddressLinkTable { }


