using AEM;
using Interfaces;
using Newtonsoft.Json;
using Windows.Networking.Connectivity;

namespace notesUpgrader
{
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
                    foreach (var bm in oldInfo.Bookmarks)
                    {
                        Console.WriteLine(bm.Title);
                        switch (bm.bookmarkType)
                        {
                            case BookmarkType.birth:
                                var bbm = new BirthBookmark
                                {
                                    Title = bm.Title,
                                    X = bm.X,
                                    Y = bm.Y,
                                    W = bm.W,
                                    H = bm.H,
                                    Transkript = bm.Transkript,
                                    SheetNr = bm.SheetNr,
                                    Child = new Person
                                    {
                                        Name = bm.Person1,
                                        BirthDate = bm.EventDate,
                                        state = bm.Flag1 ? birthState.legitmate : birthState.illegitmate
                                    },
                                    Father = new Person
                                    {
                                        Name = bm.Father,
                                        //Occupation = bm.
                                    },
                                    Mother = new Person
                                    {
                                        Name = bm.Mother
                                        //Occupation = bm.
                                    }
                                };
                                newInfo.Bookmarks.Add(bbm);

                                break;

                            case BookmarkType.marriage:
                                var mbm = new MarriageBookmark
                                {
                                    Title = bm.Title,
                                    X = bm.X,
                                    Y = bm.Y,
                                    W = bm.W,
                                    H = bm.H,
                                    Transkript = bm.Transkript,
                                    SheetNr = bm.SheetNr,
                                    EventDate = bm.EventDate,

                                    Groom = new Person
                                    {
                                        Name = bm.Person1,
                                        BirthDate = bm.Date1,
                                        Occupation = bm.Status1,
                                    },

                                    Bride = new Person
                                    {
                                        Name = bm.Person2,
                                        BirthDate = bm.Date2,
                                        Occupation = bm.Status2,
                                    },
                                    GroomFather = new Person
                                    {
                                        Name = bm.Person3,
                                        Occupation = bm.Status3,
                                    },
                                    GroomMother = new Person
                                    {
                                        Name = bm.Person4,
                                        Occupation = bm.Status3,
                                    },
                                    BrideFather = new Person
                                    {
                                        Name = bm.Person5,
                                        Occupation = bm.Status4,
                                    },
                                    BrideMother = new Person
                                    {
                                        Name = bm.Person6,
                                    },
                                    Witnesses = new Person
                                    {
                                        Name = bm.Others,
                                    }
                                };
                                newInfo.Bookmarks.Add(mbm);
                                break;

                            case BookmarkType.misc:
                                var miscBM = new MiscBookmark
                                {
                                    Title = bm.Title,
                                    X = bm.X,
                                    Y = bm.Y,
                                    W = bm.W,
                                    H = bm.H,
                                    Transkript = bm.Transkript,
                                    SheetNr = bm.SheetNr,
                                    EventDate = bm.EventDate,
                                };
                                newInfo.Bookmarks.Add(miscBM);
                                break;

                            default:
                                var defBM = new BookmarkBase
                                {
                                    Title = bm.Title,
                                    X = bm.X,
                                    Y = bm.Y,
                                    W = bm.W,
                                    H = bm.H,
                                    Transkript = bm.Transkript,
                                    SheetNr = bm.SheetNr,
                                };
                                newInfo.Bookmarks.Add(defBM);
                                break;
                        }
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


