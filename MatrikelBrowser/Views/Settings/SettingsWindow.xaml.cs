using MatrikelBrowser.ViewModels;
using MahApps.Metro.Controls;

namespace MatrikelBrowser.Views
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class SettingsWindow : MetroWindow
    {
        public SettingsWindow(SettingsVM vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
