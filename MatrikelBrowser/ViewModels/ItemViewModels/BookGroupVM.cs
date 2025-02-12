﻿using Interfaces;
using MbCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MatrikelBrowser.ViewModels
{
    public class BookGroupVM : ItemVM
    {
        public string Name { get; } = "";
        public List<BookVM> BookVMs { get; } = new();

        public BookVM? SelectedBook
        {
            get => _selectedBook;
            set
            {
                if (_selectedBook != value)
                {                    
                    SetProperty(ref _selectedBook, value);
                    (parent as ParishVM)!.SelectedBookGroup = this;                    
                }
            }
        }

        public BookGroupVM(IGrouping<BookType, MbCore.Book>? books = null, ParishVM? parent = null) : base(parent)
        {
            if (books != null)
            {
                this.Name = Enum.GetName(typeof(BookType), books.Key) ?? "?";
                              

                foreach (var book in books.OrderBy(b => b.GetStartYear()))
                {   
                    BookVMs.Add(new BookVM(book, this));
                }
            }

            Indent = 5;
        }

        private BookVM? _selectedBook;
    }
}
