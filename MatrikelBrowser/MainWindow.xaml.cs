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
        }

        private void TreeViewItem_RequestBringIntoView(object sender, RequestBringIntoViewEventArgs e) => e.Handled = true;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //FirstFlyout.IsOpen = true;
            AnimateColumnWidth(grid, 2, 0, 0.2);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) => NotesFlyout.IsOpen = true;

        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (DataContext is MainViewModel dc)
            {
                dc.cmdSave.Execute(null);
            }
        }


        private void AnimateColumnWidth(Grid grid, int columnIndex, double targetWidth, double durationSeconds)
        {
            if (grid == null || columnIndex >= grid.ColumnDefinitions.Count) return;

            var column = grid.ColumnDefinitions[columnIndex];
            double currentWidth = column.ActualWidth;

            // Create the animation
            var animation = new GridLengthAnimation2()
            {
                From = new GridLength(currentWidth),
                To = new GridLength(targetWidth),
                
                Duration = TimeSpan.FromSeconds(durationSeconds),
                AutoReverse = true // This will reverse back to the original width
            };

            

            // Create and start the storyboard
            Storyboard storyboard = new Storyboard();
            Storyboard.SetTarget(animation, column);
            Storyboard.SetTargetProperty(animation, new PropertyPath("Width"));

            storyboard.Children.Add(animation);
            storyboard.Begin();
        }
    }

    public class GridLengthAnimation2 : AnimationTimeline
    {
       
        public GridLength From
        {
            get { return (GridLength)GetValue(FromProperty); }
            set { SetValue(FromProperty, value); }
        }

        public static readonly DependencyProperty FromProperty =
          DependencyProperty.Register("From", typeof(GridLength), typeof(GridLengthAnimation2));

        public GridLength To
        {
            get { return (GridLength)GetValue(ToProperty); }
            set { SetValue(ToProperty, value); }
        }

        public static readonly DependencyProperty ToProperty =
            DependencyProperty.Register("To", typeof(GridLength), typeof(GridLengthAnimation2));

        public override Type TargetPropertyType
        {
            get { return typeof(GridLength); }
        }

        protected override Freezable CreateInstanceCore()
        {
            return new GridLengthAnimation2();
        }

        public override object GetCurrentValue(object defaultOriginValue, object defaultDestinationValue, AnimationClock animationClock)
        {
            double fromValue = this.From.Value;
            double toValue = this.To.Value;

            if (fromValue > toValue)
            {
                return new GridLength((1 - animationClock.CurrentProgress.Value) * (fromValue - toValue) + toValue, this.To.IsStar ? GridUnitType.Star : GridUnitType.Pixel);
            }
            else
            {
                return new GridLength((animationClock.CurrentProgress.Value) * (toValue - fromValue) + fromValue, this.To.IsStar ? GridUnitType.Star : GridUnitType.Pixel);
            }
        }
    }
}

