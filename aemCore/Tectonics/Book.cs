﻿using Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace AEM
{
    public class Book  
    {
        public int Id { get; set; }
        public string RefId { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string BookInfoLink { get; set; } = string.Empty;
        public BookType BookType { get; set; } = BookType.None;
        public string PageLinkPrefix { get; set; } = string.Empty;
        required public ParishDTO Parish { get; set; }
        required public ICollection<Page> Pages { get; set; }

        public override string ToString() => Title;
        
        public void LoadPageInfo()
        {
            if (!Pages.Any()) // only load if necessary
            {
                loadPages?.Invoke();
            }

            //if (hasInfo) return;  // did we already load the page info? -> no need to parse again
            //var bookFolder = new DirectoryInfo(Path.Combine(baseFolder!.FullName, "books", RefId));
            //pagesFolder = new(Path.Combine(bookFolder.FullName, "pages"));
            //loadBookInfo(bookFolder);
            //hasInfo = true;
        }
        [NotMapped]
        public Action loadPages { get; set; }
    }
}
