using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ArchiveBrowser
{
    /// <summary>
    /// Interaktionslogik für Bookmark.xaml
    /// </summary>
    public partial class Bookmark : UserControl
    {
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(Bookmark), new PropertyMetadata(""));

        public double W
        {
            get { return (double)GetValue(WProperty); }
            set { SetValue(WProperty, value); }
        }
        public static readonly DependencyProperty WProperty =
            DependencyProperty.Register("W", typeof(double), typeof(Bookmark), new PropertyMetadata(0.0));

        public double H
        {
            get { return (double)GetValue(HProperty); }
            set { SetValue(HProperty, value); }
        }
        public static readonly DependencyProperty HProperty =
            DependencyProperty.Register("H", typeof(double), typeof(Bookmark), new PropertyMetadata(0.0));

        public Bookmark()
        {
            InitializeComponent();

            Canvas.SetLeft(this, 0);
            Canvas.SetTop(this, 0);
        }

        #region Moving --------------------------------------------------

        Point? oldMousePosition;

        private void imgArr_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            oldMousePosition = e.GetPosition(Parent as FrameworkElement);

            bookmarkRect.CaptureMouse();
            e.Handled = true;
        }

        private void imgArr_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (oldMousePosition != null && e.LeftButton == MouseButtonState.Pressed)
            {
                var newMousePosition = e.GetPosition(Parent as UIElement);

                var deltaMousePosition = newMousePosition - oldMousePosition.Value;
                oldMousePosition = newMousePosition;

                double curX = Canvas.GetLeft(this);
                double curY = Canvas.GetTop(this);

                Canvas.SetLeft(this, curX + deltaMousePosition.X);
                Canvas.SetTop(this, curY + deltaMousePosition.Y);

                flip(newMousePosition.X);
            }

            e.Handled = true;

        }

        public void flip(double xPos)
        {
            bool left = xPos < 1500;

            var st = bookmarkRect.RenderTransform as TransformGroup;
            var y = (ScaleTransform)st!.Children.First(tr => tr is ScaleTransform);
            y.ScaleX = left ? 1 : -1;
        }

        private void imgArr_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {

            oldMousePosition = null;
            bookmarkRect.ReleaseMouseCapture();
            e.Handled = true;
        }

        #endregion

        #region Scaling ---------------------------------------
        Point? oldScalerPosition;

        private void Scaler_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            oldScalerPosition = e.GetPosition(Parent as FrameworkElement);

            Scaler.CaptureMouse();
            e.Handled = true;
        }

        private void Scaler_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            oldScalerPosition = null;
            Scaler.ReleaseMouseCapture();
            e.Handled = true;
        }

        private void Scaler_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (oldScalerPosition != null && e.LeftButton == MouseButtonState.Pressed)
            {
                Point newScalerPosition = e.GetPosition(Parent as UIElement);
                Vector delta = newScalerPosition - oldScalerPosition.Value;
                oldScalerPosition = newScalerPosition;

                W = Math.Max(txt.ActualWidth, bookmarkRect.Width + delta.X);
                H = Math.Max(70, bookmarkRect.Height + delta.Y);
            }

            e.Handled = true;
        }

        #endregion
    }
}

