using Interfaces;
using MbCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AEM
{
    public class Event
    {
        public int Id { get; internal set; }
        public BookmarkType EventType { get; set; }
        public int SheetNr { get; set; }

        // position
        public double X { get; set; }
        public double Y { get; set; }
        public double W { get; set; }
        public double H { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Transcript { get; set; } = string.Empty;
        public string Remarks { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
                                                               // BIRTH           / MARRIAGE       / DEATH
        public string Person1 { get; set; } = string.Empty;    // child           / groom          / deceased
        public string Person2 { get; set; } = string.Empty;    // child-father    / groom-father   / deceased-father
        public string Person3 { get; set; } = string.Empty;    // child-mother    / groom-mother   / deceased-mother
        public string Person4 { get; set; } = string.Empty;    //                 / bride
        public string Person5 { get; set; } = string.Empty;    //                 / bride-father 
        public string Person6 { get; set; } = string.Empty;    //                 / bride-mother 
        public string Person7 { get; set; } = string.Empty;    // child-godfather / Witnesses

        public string Date1 { get; set; } = string.Empty;      // child-birthday  / marriage-date   / death-date
        public string Date2 { get; set; } = string.Empty;      // child-batptDate /                 / deceased burial 
        public string Date3 { get; set; } = string.Empty;      //                 / groom-birthday      
        public string Date4 { get; set; } = string.Empty;      //                 / bride-birthday 

        public string Occupation1 { get; set; } = string.Empty;//                 / groom-occ       / deceased-occ
        public string Occupation2 { get; set; } = string.Empty;//                 / bride-occ       / deceased-occ
        public string Occupation3 { get; set; } = string.Empty;// father-occ      / groom-father-occ/ 

        // navigation
        virtual public Book Book { get; set; } = null!;

        public override string ToString() => Title;
        
    }
}
