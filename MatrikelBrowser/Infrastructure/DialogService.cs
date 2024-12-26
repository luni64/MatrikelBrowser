using MatrikelBrowser.Views;
using MatrikelBrowser.ViewModels;

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
            }
            return retVal;
        }

    }
}
