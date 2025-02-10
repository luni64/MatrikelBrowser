using MatrikelBrowser.Views;
using MbCore;
using Org.BouncyCastle.Crypto.Macs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MatrikelBrowser.ViewModels
{
    public class SplashVM : BaseViewModel
    {
        RelayCommand? _closeWin;
        public RelayCommand cmdCloseWin => _closeWin ??= new RelayCommand(doCloseWin);
        void doCloseWin(object? o)
        {
            if(o is MatrikelBrowser.Views.Splash splash)
            {
                splash.Close();
            }
                
        }

                       
        public ImageSource SplashIconSource { get; } = BitmapFrame.Create(new Uri("icons8-books-64.ico", UriKind.RelativeOrAbsolute));

        private StringBuilder _initLog = new();
        public string InitLog
        {
            get => _initLog.ToString();
        }
                      
        public void AddLog(string message)
        {
            _initLog.AppendLine(message);
            OnPropertyChanged("InitLog");
            //await Task.Delay(100);
        }     

    }
}
