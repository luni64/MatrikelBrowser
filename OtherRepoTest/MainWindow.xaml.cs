using HtmlAgilityPack;
using Microsoft.Web.WebView2.WinForms;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OtherRepoTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Web.WebView2.Wpf.IWebView2 wv = webView;

            string s = webView.Source.ToString();
           
            if(DataContext is MainVM vm)
            {
                vm.cmdTest.Execute(s);
            }

            //  if(webView is WebView2 wv)
            //    {

            //    }
        }

        void read(string path)
        {
            var htmlWeb = new HtmlWeb();
            var htmlDoc = htmlWeb.Load(path);

            string country = string.Empty;
            string diocese = string.Empty;
            string parish = string.Empty;

            var bc = htmlDoc.DocumentNode.SelectSingleNode("//ol[@class='breadcrumb']");
            if (bc != null)
            {
                // Find all <li> elements within the <ol> element
                var breadcrumbItems = bc.SelectNodes(".//li");

                if (breadcrumbItems != null)
                {
                    country = breadcrumbItems[1].InnerText.Trim();
                    diocese = breadcrumbItems[2].InnerText.Trim();
                    parish = breadcrumbItems[3].InnerText.Trim();

                    //foreach (var item in breadcrumbItems)
                    //{
                    //    // Get the text content of the <li> element
                    //    string text = item.InnerText.Trim();

                    //    // Check if the <li> contains a link (<a>)
                    //    var linkNode = item.SelectSingleNode(".//a[@href]");
                    //    string link = linkNode != null ? linkNode.GetAttributeValue("href", string.Empty) : "No link";

                    //    // Output the breadcrumb item
                    //    Trace.WriteLine($"Text: {text}, Link: {link}");
                    //}
                }


                // Finde die Tabelle
                var table = htmlDoc.DocumentNode.SelectSingleNode("//table[@class='table table-bordered w-100']");

                if (table != null)
                {
                    // Finde alle Zeilen (tr-Elemente) in der Tabelle
                    var rows = table.SelectNodes(".//tr");

                    foreach (var row in rows.Skip(1)) // Überspringe die Header-Zeile
                    {
                        var cells = row.SelectNodes("td");

                        if (cells != null && cells.Count > 3) // Überprüfe, ob genug Spalten vorhanden sind
                        {
                            var signatur = cells[1].InnerText.Trim();
                            var matrikeltyp = cells[2].InnerText.Trim();
                            var datum = cells[3].InnerText.Trim();

                            // Extrahiere den Link (falls vorhanden)
                            var linkNode = cells[0].SelectSingleNode(".//a[@href]");
                            var link = linkNode?.GetAttributeValue("href", string.Empty);


                            if (!string.IsNullOrEmpty(link))
                            {
                                var parts = link.Split('/', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                                link = "https://data.matricula-online.eu" + link; // Basis-URL hinzufügen

                                Trace.Write($"{country} | {diocese} | {parish} | ");
                                Trace.WriteLine($"Signatur: {signatur}, Matrikeltyp: {matrikeltyp}, Datum: {datum}");


                            }

                        }
                    }
                }
                else
                {
                    Console.WriteLine("Tabelle nicht gefunden.");
                }
            }

        }
    }

}
