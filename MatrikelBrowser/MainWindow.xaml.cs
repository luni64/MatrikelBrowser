﻿using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ArchiveBrowser.ViewModels;
using MahApps.Metro.Controls;



namespace ArchiveBrowser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //private void MainTreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        //{
        //    if (DataContext is MainViewModel dc && e.NewValue is BookVM bookVM)
        //    {
        //        dc.selectedBook = bookVM;
        //    }
        //}

        // prevent auto horizontal scrolling for wide entries
        private void TreeViewItem_RequestBringIntoView(object sender, RequestBringIntoViewEventArgs e)
        {
            e.Handled = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e) => FirstFlyout.IsOpen = true;
        private void Button_Click_1(object sender, RoutedEventArgs e) => NotesFlyout.IsOpen = true;
        
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var x = pageSelector.Focus();  // need to actively remove focus from Notes textbox to get changes before saving
            if(DataContext is MainViewModel dc)
            {
                dc.cmdSave.Execute(null);
            }
        }

        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (DataContext is MainViewModel dc)
            {
                dc.cmdSave.Execute(null);
            }
        }
    }
}
