//using Microsoft.Web.WebView2.Core;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace ArchiveBrowser.Views
{
//    public partial class ReportView : Window
//    {
//        Uri? sourceURi;
//        public ReportView(string reportFile)
//        {
//            InitializeComponent();
//            sourceURi = new Uri(reportFile);

//            webView.CoreWebView2InitializationCompleted += (sender, e) =>
//            {
//                if (e.IsSuccess)
//                {
//                    var settings = webView.CoreWebView2.Settings;
//                    settings.HiddenPdfToolbarItems = CoreWebView2PdfToolbarItems.Save;
//                    settings.AreDevToolsEnabled = false;
//                }
//            };

//            webView.NavigationStarting += (sender, e) =>
//            {
//                var t = sourceURi?.ToString();

//                if (e.Uri.ToLower() != sourceURi?.ToString()) // open external links in the browser
//                {
//                    Process.Start(new ProcessStartInfo("cmd", $"/c start {e.Uri}")
//                    {
//                        CreateNoWindow = true,
//                        WindowStyle = ProcessWindowStyle.Minimized
//                    });
//                    e.Cancel = true;
//                }
//            };

//            if (File.Exists(reportFile))
//            {
//                if (webView.Source != sourceURi)
//                    webView.Source = sourceURi;
//                else
//                    webView.Reload();
//            }
//        }
//    }
}
