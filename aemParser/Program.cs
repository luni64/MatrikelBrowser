using Newtonsoft.Json;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace parser
{
    internal class Program
    {
        class Book2
        {
            public string ID { get; set; } = "";
            public string Title { get; set; } = "";
            public Guid BookInfoID { get; set; }
            public Guid DescriptionID { get; set; }
            public override string ToString() => $"{ID}-{Title}-{BookInfoID}";
        }

        class Parish2
        {
            public string ID { get; set; } = "";
            public string Place { get; set; } = "";
            public string Church { get; set; } = "";
            public int startYear { get; set; }
            public int endYear { get; set; }

            public List<Book2> Books { get; set; } = new List<Book2>();

            public override string ToString() => $"{ID}-{Place}-{Church}";
        }

        static void Main(string[] args)
        {
            DirectoryInfo baseDir = new(@"C:\Users\lutz\Tektonik");
            List<Parish2> Parishes = new();

            foreach (var file in baseDir.EnumerateFiles("ap*.txt"))
            {
                //    var doc = new HtmlDocument();
                //    doc.Load(file.FullName);
                //    var node = doc.DocumentNode.SelectSingleNode(@"//body");
                //    var canditates = node.Descendants().Where(x => x.HasClass("docPreviewLine"));
                //    foreach (var canditate in canditates)
                //    {
                //        var parishNode = canditate.ChildNodes.FirstOrDefault(c => c.NodeType == HtmlNodeType.Element && c.InnerText.StartsWith("CB"));
                //        if (parishNode != null)
                //        {
                //        }                        
                //    }
                //    //break;



                var mtxt = File.ReadAllLines(file.FullName);
                foreach (var line in mtxt.Where(l => l.Contains("Dokument CB")))
                {
                    MatchCollection col = Regex.Matches(line, @"\""[^\""]*\""");
                    var m = col.First(m => m.Value.Contains("Dokument")).Value.Split(" ", 2)[1];

                    Parish2 parish = new();
                    var parts = m.Split(" ", 2);
                    parish.ID = parts[0];

                    parts = parts[1].Split("-", 2);
                    parish.Place = parts[0];
                    parts = parts[1].Split(" - ", 2);
                    parish.Church = parts[0].TrimEnd('"');

                    try
                    {
                        var range = parts[1].Split("-", 2);
                        parish.startYear = int.Parse(range[0]);
                        parish.endYear = int.Parse(range[1].TrimEnd('"'));
                    }
                    catch { }
                    Parishes.Add(parish);
                }
            }

            foreach (var file in baseDir.EnumerateFiles("cb*.html"))
            {
                string CB = "", M = "", title = "", descId = "", dftViewerLink = "", bookInfoID = "";

                var txt = File.ReadAllLines(file.FullName);
                foreach (var line in txt)
                {
                    var match = Regex.Match(line, "title=\"CB[^\"]*");
                    if (match.Success)
                    {
                        CB = Regex.Match(match.Value, "CB\\w+").Value;
                        M = Regex.Match(match.Value, "M\\w+").Value;
                        title = Regex.Match(match.Value, "(?<= \\- ).*").Value;                                                
                        dftViewerLink = Regex.Match(line, @"(?<=href\="")http[^""]*").Value;
                        bookInfoID = Regex.Match(dftViewerLink, @"(?<=id=Rep_)[^_]*").Value;
                    }

                    match = Regex.Match(line, @"(?<=copyPermanentLink\(this,\\)[^\\]*");
                    if (match.Success)
                    {
                        descId = match.Value.Split("Vz_")[1];

                        var parish = Parishes.FirstOrDefault(s => s.ID == CB);
                        if (parish != null && !parish.Books.Any(b => b.ID == M))
                        {
                            parish.Books.Add(new Book2
                            {
                                ID = M,
                                Title = title,
                                BookInfoID = String.IsNullOrEmpty(bookInfoID) ? default(Guid) : new Guid(bookInfoID),
                                DescriptionID = String.IsNullOrEmpty(descId) ? default(Guid) : new Guid(descId),
                            }); ;

                        }
                    }
                }
            }
            foreach (var parishGroup in Parishes.GroupBy(p => p.Place[0]))
            {
                Console.WriteLine($"{parishGroup.Key}-----------------------------");
                foreach (var parish in parishGroup/*.Where(p=>p.ID=="CB107")*/)
                {
                    Console.WriteLine($"{parish.ID} | {parish.Place} | {parish.Church}");
                    foreach (var book in parish.Books.OrderBy(b => b.ID))
                    {
                        Console.WriteLine($"  {book}");
                    }
                }
            }

            string json = JsonConvert.SerializeObject(Parishes, new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
            });

            File.WriteAllText(Path.Combine(baseDir.FullName, "tectonics.json"), json);

            // WebClient client = new WebClient();

            // client.DownloadFile("https://digitales-archiv.erzbistum-muenchen.de/actaproweb/archive.jsf?id=Vz++++++DB0061AF-127D-4903-9045-9D91A4536B64", "test.html");

        }
    }
}