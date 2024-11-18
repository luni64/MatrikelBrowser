using AEM;
using Interfaces;
using Org.BouncyCastle.Crypto;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Media.Media3D;


namespace ArchiveBrowser.ViewModels
{

    public interface IDetailsVM
    {
        IBookmarkDetails detailsModel { get; }
        BookmarkVM parent { get; }
    };

    public class BookmarkVM : BaseViewModel
    {
        public RelayCommand cmdDelSelf { get; }
        private void doDelSelf(object? o)
        {
            parent.bookmarkVMs.Remove(this);
        }

        public RelayCommand cmdSaveDetails { get; }
        private void doSaveDetails(object? param)
        {
            var d = SelectedViewModel.detailsModel;
            model.Details = d;
        }

        private BookmarkType _bookmarkType;
        public BookmarkType bookmarkType
        {
            get => _bookmarkType;
            set
            {
                _bookmarkType = value;
                SelectedViewModel = detailViewmodels[bookmarkType]!;
                OnPropertyChanged();

            }
        }

        private IDetailsVM _selectedViewModel;
        public IDetailsVM SelectedViewModel
        {
            get => _selectedViewModel;
            set => SetProperty(ref _selectedViewModel, value);
        }



        private bool _isLocked = true;
        public bool isLocked
        {
            get => _isLocked;
            set => SetProperty(ref _isLocked, value);
        }

        public string Title
        {
            get => model.Title;
            set
            {
                if (value != model.Title)
                {
                    model.Title = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Transcript
        {
            get => model.Transcript;
            set
            {
                if (value != model.Transcript)
                {
                    model.Transcript = value;
                    OnPropertyChanged();
                }
            }
        }
        public int SheetNr
        {
            get => model.SheetNr;
            set => model.SheetNr = value;
        }
        public int X
        {
            get => model.X;
            set => model.X = value;
        }
        public int Y
        {
            get => model.Y;
            set => model.Y = value;
        }
        public double W
        {
            get => model.W;
            set => model.W = value;
        }
        public double H
        {
            get => model.H;
            set => model.H = value;
        }


        public string ID { get; set; } = Guid.Empty.ToString();
        public PageVM? Page { get; set; }

        private BookVM parent;

        public BookmarkVM(IBookmarkBase model, BookVM parent)
        {
            cmdSaveDetails = new RelayCommand(doSaveDetails);
            cmdDelSelf = new RelayCommand(doDelSelf);

            this.parent = parent;
            this.model = model;
            detailViewmodels.Add(BookmarkType.birth, new BirthDetailsVM(this));
            detailViewmodels.Add(BookmarkType.marriage, new MarriageDetailVM(this));
            detailViewmodels.Add(BookmarkType.death, new DeathDetailsVM(this));
            detailViewmodels.Add(BookmarkType.misc, new MiscBookmarkVM(this));

            switch (model.Details)
            {
                case BirthDetails: bookmarkType = BookmarkType.birth; break;
                case MarriageDetails: bookmarkType = BookmarkType.marriage; break;
                case DeathDetails: bookmarkType = BookmarkType.death; break;
                case MiscDetails: bookmarkType = BookmarkType.misc; break;
            }

            
        }

        public IBookmarkBase model { get; }

        Dictionary<BookmarkType, IDetailsVM?> detailViewmodels = new();
    }
}

