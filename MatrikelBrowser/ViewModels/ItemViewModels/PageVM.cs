using Interfaces;
using MbCore;
using Microsoft.VisualBasic.ApplicationServices;
using static System.Reflection.Metadata.BlobBuilder;
using System.IO;
using System.Diagnostics;
using System;
using System.Net;
using System.Windows;


namespace MatrikelBrowser.ViewModels
{
    public class PageVM : ItemVM
    {
        public RelayCommand cmdCopyViewerLink => _cmdCopyViewerLink ?? new RelayCommand(doCopyViewerLink);
        void doCopyViewerLink(object? o)
        {
            Clipboard.SetText(ViewerUrl);
        }

        public RelayCommand cmdCopyImageLink => _cmdCopyImageLink ?? new RelayCommand(doCopyImageLink);
        void doCopyImageLink(object? o)
        {
            Clipboard.SetText(ImageUrl.ToString());
        }

        public RelayCommand cmdCopyImageFile => _cmdCopyImageFile ?? new RelayCommand(doCopyImageFile);
        void doCopyImageFile(object? o)
        {
            Clipboard.SetText(ImageFilename);
        }

        public int SheetNr => model.getSheetNr();
        public Uri ImageUrl { get; }
        public string ViewerUrl { get; }
        public string ImageFilename => model.GetOrCreateImage();


        public PageVM(Page model, BookVM parent) : base(parent)
        {
            this.model = model;

            //SheetNr = model.Book.Pages.IndexOf(model);
            ImageUrl = new Uri(model.ImageURL);
            ViewerUrl = model.toViewerUrl();

            // ImageFilename =  marriageModel.GetLocalFilename();
        }

        private Page model;
        private RelayCommand? _cmdCopyViewerLink;
        private RelayCommand? _cmdCopyImageLink;
        private RelayCommand? _cmdCopyImageFile;
    }
}
