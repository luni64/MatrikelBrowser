using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Security.Policy;

namespace AEM
{
    public class Page 
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Folio { get; set; }
        public required string ImageId { get; set; }
        public required Book Book { get; set; }

        [NotMapped]
        public string ImageURL => Book.ImageLinkPrefix + ImageId;
            

        public override string ToString() => ImageURL;
        }        
    }

