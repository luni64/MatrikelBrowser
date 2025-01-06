using MbCore;
using Interfaces;
using Newtonsoft.Json;

namespace notesUpgrader
{
    public class Bookmark// : IBookmark
    {
        public string Title { get; set; } = String.Empty;
        public string Person1 { get; set; } = String.Empty;
        public string Person2 { get; set; } = String.Empty;
        public string Person3 { get; set; } = String.Empty;
        public string Person4 { get; set; } = String.Empty;
        public string Person5 { get; set; } = String.Empty;
        public string Person6 { get; set; } = String.Empty;
        public string Father { get; set; } = String.Empty;
        public string Mother { get; set; } = String.Empty;
        public string Others { get; set; } = String.Empty;
        public string EventDate { get; set; } = String.Empty;
        public string Date1 { get; set; } = String.Empty;
        public string Date2 { get; set; } = String.Empty;
        public string Status1 { get; set; } = String.Empty;
        public string Status2 { get; set; } = String.Empty;
        public string Status3 { get; set; } = String.Empty;
        public string Status4 { get; set; } = String.Empty;
        public bool Flag1 { get; set; } = true;
        public bool Flag2 { get; set; }

        public string Transkript { get; set; } = String.Empty;
        public BookmarkType bookmarkType { get; set; }
        public int SheetNr { get; set; }

       // public Rectangle cutOut { get; set; }

        public int X { get; set; }
        public int Y { get; set; }
        public double W { get; set; } = 400;
        public double H { get; set; } = 200;

    }

    public class oldBookInfo// : IOldBookInfo
    {
        public List<Bookmark> Bookmarks { get; set; } = [];
        public string BookID { get; set; } = string.Empty;
        public string note { get; set; } = string.Empty;

        public oldBookInfo(List<Bookmark>? Bookmarks = null, string BookID = "", string note = "")
        {
            this.Bookmarks = Bookmarks/*?.ToList<IBookmark>()*/ ?? [];
            this.BookID = BookID;
            this.note = note;
        }

        public override string ToString()
        {
            return BookID;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            DirectoryInfo baseFolder = new(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "lunOptics", "aemBrowser"));
            baseFolder.Create();

            // FileInfo tectonicsFile = new("tectonics.json");
            FileInfo notesFile = new(Path.Combine(baseFolder.FullName, "notesout.json"));
            FileInfo outFile = new(Path.Combine(baseFolder.FullName, "out.json"));

            List<Parish> Parishes;

            List<BookInfo> newInfos = new List<BookInfo>();

            if (notesFile.Exists)
            {
                var json = File.ReadAllText(notesFile.FullName);
                var oldInfos = JsonConvert.DeserializeObject<List<oldBookInfo>>(json)!;

                foreach (var oldInfo in oldInfos)
                {
                    BookInfo newInfo = new()
                    {
                        BookID = oldInfo.BookID,
                        note = oldInfo.note,
                    };
                    foreach (var obm in oldInfo.Bookmarks)
                    {
                        Console.WriteLine(obm.Title);

                        var nbm = new BookmarkBase
                        {
                            Title = obm.Title,
                            X = obm.X,
                            Y = obm.Y,
                            W = obm.W,
                            H = obm.H,
                            EventDate = obm.Date1,
                            Transcript = obm.Transkript,
                            SheetNr = obm.SheetNr
                        };

                        //IBookmarkDetails details;

                        switch (obm.bookmarkType)
                        {
                            case BookmarkType.birth:
                                nbm.Details = new BirthDetails
                                {
                                    Child = new PersonOld
                                    {
                                        Name = obm.Person1,
                                        BirthDate = obm.EventDate,
                                        state = obm.Flag1 ? BirthState.legitmate : BirthState.illegitmate
                                    },
                                    Father = new PersonOld
                                    {
                                        Name = obm.Father,
                                        //Occupation = bm.
                                    },
                                    Mother = new PersonOld
                                    {
                                        Name = obm.Mother
                                        //Occupation = bm.
                                    }
                                }; 
                                break;

                            case BookmarkType.marriage:
                                nbm.Details = new MarriageDetails
                                {
                                    Groom = new PersonOld
                                    {
                                        Name = obm.Person1,
                                        BirthDate = obm.Date1,
                                        Occupation = obm.Status1,
                                    },

                                    Bride = new PersonOld
                                    {
                                        Name = obm.Person2,
                                        BirthDate = obm.Date2,
                                        Occupation = obm.Status2,
                                    },
                                    GroomFather = new PersonOld
                                    {
                                        Name = obm.Person3,
                                        Occupation = obm.Status3,
                                    },
                                    GroomMother = new PersonOld
                                    {
                                        Name = obm.Person4,
                                        Occupation = obm.Status3,
                                    },
                                    BrideFather = new PersonOld
                                    {
                                        Name = obm.Person5,
                                        Occupation = obm.Status4,
                                    },
                                    BrideMother = new PersonOld
                                    {
                                        Name = obm.Person6,
                                    },
                                    Witnesses = new PersonOld
                                    {
                                        Name = obm.Others,
                                    }
                                };                                
                                break;

                            default:
                            //case EventType.misc:
                                nbm.Details = new MiscDetails();                                
                                break;

                                //var defBM = new BookmarkBase
                                //{
                                //    Title = obm.Title,
                                //    X = obm.X,
                                //    Y = obm.Y,
                                //    W = obm.W,
                                //    H = obm.H,
                                //    Transkript = obm.Transkript,
                                //    SheetNr = obm.SheetNr,
                                //};
                                //newInfo.Bookmarks.Add(defBM);
                               // break;
                        }                                                                 
                        newInfo.Bookmarks.Add(nbm);
                    }
                    newInfos.Add(newInfo);
                }


                json = JsonConvert.SerializeObject(newInfos, Formatting.Indented, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto });
                FileInfo testFile = new(Path.Combine(baseFolder.FullName, "test.json"));
                File.WriteAllText(testFile.FullName, json);

                //json = File.ReadAllText(testFile.FullName);

                //var infos = JsonConvert.DeserializeObject<List<BookInfo>>(json, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto });

                //foreach (var book in infos)
                //{
                //    Console.WriteLine(book.BookID);
                //    foreach (var bookmark in book.Bookmarks)
                //    {

                //        //var y = (bookmark as BirthBookmark);

                //        //switch (bookmark)
                //        //{
                //        //    case BirthBookmark:
                //        //        Console.Write("    Birth: ");    
                //        //        var b = bookmark as BirthBookmark;
                //        //        Console.Write(bookmark);
                //        //        break;
                //        //    case MarriageBookmark:
                //        //        Console.Write("    Marriage: ");
                //        //        break;
                //        //    default:                            
                //        //        Console.WriteLine("    Default: ");
                //        //        break;
                //        //}
                //        Console.WriteLine(bookmark);
                //    }
                //}

            }

        }
    }
}


