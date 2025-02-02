﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MbCore
{
    public class Parish
    {
        public int Id { get; set; }
        public required string Name { get; set; } = string.Empty;
        public string RefId { get; set; } = string.Empty;
        public required string Place { get; set; }
        public string Church { get; set; } = string.Empty;
        public string Breadcrumb { get; set; } = string.Empty;

        virtual public Archive Archive { get; set; } = null!;
        virtual public List<Book> Books { get; set; } = [];

        [NotMapped]
        public bool hasBooks { get; set; }
        public override string ToString() => Name;
    }

}
