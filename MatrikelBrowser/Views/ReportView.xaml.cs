using Microsoft.Web.WebView2.Core;
using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ArchiveBrowser.Views
{
    /// <summary>
    /// Interaction logic for ReportView.xaml
    /// </summary>
    public partial class ReportView : Window
    {
        Uri? sourceURi;
        public ReportView(string reportFile)
        {
            InitializeComponent();
            sourceURi = new Uri(reportFile);

            webView.CoreWebView2InitializationCompleted += (sender, e) =>
            {
                if (e.IsSuccess)
                {
                    var settings = webView.CoreWebView2.Settings;
                    settings.HiddenPdfToolbarItems = CoreWebView2PdfToolbarItems.Save;
                    settings.AreDevToolsEnabled = false;
                }
            };

            webView.NavigationStarting += (sender, e) =>
            {
                if (e.Uri != sourceURi?.ToString()) // open external links in the browser
                {
                    Process.Start(new ProcessStartInfo("cmd", $"/c start {e.Uri}")
                    {
                        CreateNoWindow = true,
                        WindowStyle = ProcessWindowStyle.Minimized
                    });
                    e.Cancel = true;
                }
            };

            if (File.Exists(reportFile))
            {
                if (webView.Source != sourceURi)
                    webView.Source = sourceURi;
                else                    
                    webView.Reload();
            }
        }
    }
}
