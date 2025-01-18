using Interfaces;
using System;
using System.Collections.Generic;

namespace MbCore
{
    public class MatrikulaBookInfo
    {
        public string BookREFID { get; set; } = string.Empty;
        public BookType Type { get; set; } = BookType.None;
        public string Title { get; set; } = string.Empty;
        //public string Date { get; set; } = string.Empty;
        public string StartYear { get; set; } = string.Empty;
        public string EndYear { get; set; } = string.Empty;
        public string InfoUrl { get; set; } = string.Empty;
        public override string ToString() => $"{Title} [{StartYear} - {EndYear}]";
    }

    public class MatParishInfo
    {
        public bool isOK = false;
        public string ParishREFID { get; set; } = string.Empty;
        public string ArchiveName { get; set; } = string.Empty;
        public string CountryName { get; set; } = string.Empty;
        public string ParishName { get; set; } = string.Empty;
        public string ChurchName { get; set; } = string.Empty;
        public string BookBaseUrl { get; set; } = string.Empty;
        public int totalNrOfBooks { get; set; }
        public int loadedNrOfBooks { get; set; }
        public IList<MatrikulaBookInfo> bookInfos { get; set; } = [];
    }

    public class MatCountryInfo
    {
        public string Name { get; set; } = string.Empty;
        public string InfoLink { get; set; }= string.Empty;

        public override string ToString()
        {
            return Name;
        }


    }
}




