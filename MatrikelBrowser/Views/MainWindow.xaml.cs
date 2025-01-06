using MatrikelBrowser.ViewModels;
using MahApps.Metro.Controls;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Media;
using System.Diagnostics;
using iText.Layout.Properties;
using System.Windows.Media.Animation;
using System;
using MahApps.Metro.IconPacks;
using System.IO.Pipes;
using Microsoft.EntityFrameworkCore.Metadata.Internal;



namespace MatrikelBrowser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            nextColArrow = rightArrow;
            nextRowArrow = upArrow;
            TecButton.ButtonContent = nextColArrow;
            NotesButton.ButtonContent = upArrow;
        }
        private void TreeViewItem_RequestBringIntoView(object sender, RequestBringIntoViewEventArgs e) => e.Handled = true;



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //FirstFlyout.IsOpen = true;
            if (grid.ColumnDefinitions[2].ActualWidth == 0)
            {
                AnimateColumnWidth(2, 450, 0.25);
                nextColArrow = rightArrow;
            }

            else
            {
                AnimateColumnWidth(2, 0, 0.25);
                nextColArrow = leftArrow;
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (grid.RowDefinitions[2].ActualHeight == 0)
            {
                AnimateRowHeight(rowIndex: 2, 250, 0.25);
                nextRowArrow = downArrow;
            }

            else
            {
                AnimateRowHeight(rowIndex: 2, 0, 0.25);
                nextRowArrow = upArrow;
            }
        }

        private void AnimateRowHeight(int rowIndex, double targetHeight, double durationSeconds)
        {
            var row = grid.RowDefinitions[rowIndex];
            double currentHeight = row.ActualHeight;
            var animation = new GridLengthAnimation
            {
                From = new GridLength(currentHeight),
                To = new GridLength(targetHeight),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut },
                Duration = TimeSpan.FromSeconds(durationSeconds),
                AutoReverse = false // This will reverse back to the original width
            };

            animation.AnimationCompleted += rowAnimationCompleted;

            // Create and start the storyboard
            Storyboard storyboard = new Storyboard();
            Storyboard.SetTarget(animation, row);
            Storyboard.SetTargetProperty(animation, new PropertyPath("Height"));

            storyboard.Children.Add(animation);
            storyboard.Begin();
        }


        private void AnimateColumnWidth(int columnIndex, double targetWidth, double durationSeconds)
        {
            var column = grid.ColumnDefinitions[columnIndex];
            double currentWidth = column.ActualWidth;

            var animation = new GridLengthAnimation
            {
                From = new GridLength(currentWidth),
                To = new GridLength(targetWidth),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut },
                Duration = TimeSpan.FromSeconds(durationSeconds),
                AutoReverse = false // This will reverse back to the original width
            };

            animation.AnimationCompleted += columnAnimatonCompleted;

            // Create and start the storyboard
            Storyboard storyboard = new Storyboard();
            Storyboard.SetTarget(animation, column);
            Storyboard.SetTargetProperty(animation, new PropertyPath("Width"));

            storyboard.Children.Add(animation);
            storyboard.Begin();
        }
        private void rowAnimationCompleted()
        {
            NotesButton.ButtonContent = nextRowArrow;
        }

        private void columnAnimatonCompleted()
        {
            TecButton.ButtonContent = nextColArrow;
        }
        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (DataContext is MainViewModel dc)
            {
                dc.cmdSave.Execute(null);
            }
        }

        private object nextColArrow;
        private object nextRowArrow;
        private readonly object leftArrow = new PackIconMaterialDesign() { Kind = PackIconMaterialDesignKind.KeyboardArrowLeftRound, Width = 20, Height = 20, Margin = new Thickness(8, -5, 0, 0), Foreground = Brushes.White };
        private readonly object rightArrow = new PackIconMaterialDesign() { Kind = PackIconMaterialDesignKind.KeyboardArrowRightRound, Width = 20, Height = 20, Margin = new Thickness(8, -5, 0, 0), Foreground = Brushes.White };
        private readonly object upArrow = new PackIconMaterialDesign() { Kind = PackIconMaterialDesignKind.KeyboardArrowLeftRound, Width = 20, Height = 20, Margin = new Thickness(8, -5, 0, 0), Foreground = Brushes.White };
        private readonly object downArrow = new PackIconMaterialDesign() { Kind = PackIconMaterialDesignKind.KeyboardArrowRightRound, Width = 20, Height = 20, Margin = new Thickness(8, -5, 0, 0), Foreground = Brushes.White };


    }
}

