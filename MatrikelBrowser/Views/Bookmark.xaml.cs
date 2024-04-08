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

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(Bookmark), new PropertyMetadata(""));

        public int X
        {
            get { return (int)GetValue(XProperty); }
            set { SetValue(XProperty, value); }
        }

        // Using a DependencyProperty as the backing store for X.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty XProperty =
            DependencyProperty.Register("X", typeof(int), typeof(Bookmark), new PropertyMetadata(0));


        Point? oldMousePosition;

        public Bookmark()
        {
            InitializeComponent();

            Canvas.SetLeft(this, 0);
            Canvas.SetTop(this, 0);
        }

        private void imgArr_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            oldMousePosition = e.GetPosition(Parent as FrameworkElement);

            img.CaptureMouse();
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
                //bool left = newMousePosition.X < 1500;

                //var st = img.RenderTransform as TransformGroup;
                //var y = (ScaleTransform)st!.Children.First(tr => tr is ScaleTransform);
                //y.ScaleX = left ? 1 : -1;
            }

            e.Handled = true;

        }

        public void flip(double xPos)
        {
            bool left = xPos < 1500;

            var st = img.RenderTransform as TransformGroup;
            var y = (ScaleTransform)st!.Children.First(tr => tr is ScaleTransform);
            y.ScaleX = left ? 1 : -1;
        }

        private void imgArr_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {

            oldMousePosition = null;
            img.ReleaseMouseCapture();
            e.Handled = true;
        }

        private void txt_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is TextBox tb)
            {
                tb.IsEnabled = true;
                e.Handled = true;
            }
        }

        private void txt_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void txt_PreviewMouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {

        }
    }
}

