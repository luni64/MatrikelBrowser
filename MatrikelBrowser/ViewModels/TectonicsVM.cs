﻿using Interfaces;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AEM;

namespace ArchiveBrowser.ViewModels
{
    public class TectonicsVM : BaseViewModel
    {
        #region commands
        public RelayCommand cmdToogleFavorite => _cmdToogleFavorite ??= new RelayCommand(doToggleFavorite);
        void doToggleFavorite(object? param)
        {
            if (param is BookVM bookVM)
            {
                if (Favorites.Contains(bookVM))
                {
                    Favorites.Remove(bookVM);
                    model.Favorites.Remove(bookVM.ID);
                    bookVM.IsFavorite = false;
                }
                else
                {
                    Favorites.Add(bookVM);
                    model.Favorites.Add(bookVM.ID);
                    bookVM.IsFavorite = true;
                }
            }
        }
        #endregion

        #region properties
        public ObservableCollection<CountryVM> CountryVMs { get; } = [];
        //public List<LetterVM> LetterVMs { get; } = [];
        public ObservableCollection<BookVM> Favorites { get; } = [];
        public BookVM? selectedBook
        {
            get => _selectedBook;
            set
            {
                if (_selectedBook != value)
                {
                    if (_selectedBook != null)
                    {
                        _selectedBook.IsSelected = false;
                        _selectedBook.SubTitle = null;
                    }
                    SetProperty(ref _selectedBook, value);

                    if (_selectedBook != null)
                    {
                        _selectedBook.Intialize(); // lazy load pages information 
                        _selectedBook.IsSelected = true;
                    }
                }
            }
        }
        #endregion

        public TectonicsVM(aemCore model)  // aem core should get an interface
        {
            this.model = model;
            // setup all item viewmodels
            
            foreach(var Country in model.Countries.OrderBy(c=>c.Name))
            {
                CountryVM countryVM = new(Country.Name);
                foreach(var diocese in Country.Archives.OrderBy(d=>d.Name))
                {
                    DioceseVM dioceseVM = new(diocese.Name);
                    var ParishGroups = diocese.Parishes.Where(p => p.Books.Any()).GroupBy(p => p.Place[0]);
                    foreach (var ParishGroup in ParishGroups)
                    {
                        LetterVM letterVM = new(ParishGroup.Key.ToString());
                        foreach (var Parish in ParishGroup.OrderBy(p => p.Place))
                        {
                            ParishVM parishVM = new(Parish);
                            var BookGroups = Parish.Books.GroupBy(b => b.BookType);
                            foreach (var bookGroup in BookGroups.OrderBy(g => g.Key))
                            {
                                BookTypeVM typeVM = new(bookGroup.Key);
                                foreach (var book in bookGroup.OrderBy(b => b.RefId))
                                {
                                    typeVM.BookVMs.Add(new BookVM(book, parishVM));
                                }
                                parishVM.BookTypeVMs.Add(typeVM);
                            }
                            letterVM.ParishVMs.Add(parishVM);
                        }
                        dioceseVM.LetterVMs.Add(letterVM);
                    };
                    countryVM.DioceseVMs.Add( dioceseVM );
                }
                CountryVMs.Add(countryVM);
            }
            



            //var ParishGroups = model.Parishes./*Where(p => p.Books.Any()).*/GroupBy(p => p.Place[0]);
            //foreach (var ParishGroup in ParishGroups)
            //{
            //    LetterVM letterVM = new(ParishGroup.Key.ToString());
            //    foreach (var Parish in ParishGroup.OrderBy(p => p.Place))
            //    {
            //        ParishVM parishVM = new(Parish);
            //        var BookGroups = Parish.Books.GroupBy(b => b.Type);
            //        foreach (var bookGroup in BookGroups.OrderBy(g => g.Key))
            //        {
            //            BookTypeVM typeVM = new(bookGroup.Key);
            //            foreach (var book in bookGroup.OrderBy(b=>b.ID))
            //            {
            //                typeVM.BookVMs.Add(new BookVM(book, parishVM));
            //            }
            //            parishVM.BookTypeVMs.Add(typeVM);
            //        }
            //        letterVM.ParishVMs.Add(parishVM);
            //    }
            //    this.LetterVMs.Add(letterVM);
            //};

            //var bookVMs = LetterVMs.SelectMany(l => l.ParishVMs).SelectMany(p => p.BookTypeVMs).SelectMany(bt => bt.BookVMs);

            //foreach (var favID in model.Favorites)
            //{
            //    var bookVM = bookVMs.FirstOrDefault(b => b.ID == favID);
            //    if (bookVM != null)
            //    {
            //        Favorites.Add(bookVM);
            //        bookVM.IsFavorite= true;
            //    }
            //}
        }

        aemCore model;

        private BookVM? _selectedBook;
        RelayCommand? _cmdToogleFavorite;
    }
}
