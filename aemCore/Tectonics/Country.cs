﻿using Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AEM
{
    public class Country //: ICountry
    {
        public string Name { get; set; }
        public List<ArchiveDTO> Archives { get; }

        public override string ToString() => $"{Name}";

        public Country(string Name, List<ArchiveDTO> Dioceses)
        {
            this.Name = Name;
            this.Archives = Dioceses;//?.ToList<IDiocese>() ?? [];
        }
    }
}