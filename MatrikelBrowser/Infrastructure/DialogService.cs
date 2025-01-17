using MatrikelBrowser.Views;
using MatrikelBrowser.ViewModels;
using System.Windows;
using System;

namespace MatrikelBrowser.Infrastructure
{
    internal class DialogService : IDialogService
    {
        public object? ShowDialog(object viewModel)
        {
            object? retVal = null;
            switch (viewModel)
            {
                case SettingsVM vm:
                    var settings = new SettingsWindow(vm);                    
                    retVal = settings.ShowDialog() ?? false;                    
                    break;

                case string errorMsg:
                    MessageBox.Show(errorMsg,"Error",MessageBoxButton.OK,MessageBoxImage.Error);
                    break;
            }
            return retVal;
        }

    }
}
