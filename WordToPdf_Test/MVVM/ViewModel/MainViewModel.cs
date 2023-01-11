using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WordToPdf_Test.Core;

namespace WordToPdf_Test.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPopertyChanged();
            }
        }

        // Vms
        public ConvertViewModel ConvertVM { get; set; }
        public SummarizeViewModel SummarizeVM { get; set; }
        public SettingsViewModel SettingsVM { get; set; }

        // RelayCommand
        public RelayCommand MoveWindowCommand { get; set; }
        public RelayCommand ShutDownWindowCommand { get; set; }
        public RelayCommand MaximizeWindowCommand { get; set; }
        public RelayCommand MinimizeWindowCommand { get; set; }

        public RelayCommand ShowConvertVM { get; set; }
        public RelayCommand ShowSummarizeVM { get; set; }
        public RelayCommand ShowSettingsVM { get; set; }

        public MainViewModel()
        {
            // ViewModels
            ConvertVM = new ConvertViewModel();
            SummarizeVM = new SummarizeViewModel();
            SettingsVM = new SettingsViewModel();
            CurrentView = ConvertVM;

            // RelayCommands
            ShowConvertVM = new RelayCommand(o => { CurrentView = ConvertVM; });
            ShowSummarizeVM = new RelayCommand(o => { CurrentView = SummarizeVM; });
            ShowSettingsVM = new RelayCommand(o => { CurrentView = SettingsVM; });

            // Titelbar Commands (Move, Shutdown, min & maximized Window)
            Application.Current.MainWindow.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;

            MoveWindowCommand = new RelayCommand(o => { Application.Current.MainWindow.DragMove(); });
            ShutDownWindowCommand = new RelayCommand(o => { Application.Current.Shutdown(); });
            
            MaximizeWindowCommand = new RelayCommand(o => {
                if (Application.Current.MainWindow.WindowState == WindowState.Maximized)
                    Application.Current.MainWindow.WindowState = WindowState.Normal;
                else
                    Application.Current.MainWindow.WindowState = WindowState.Maximized;
            });

            MinimizeWindowCommand = new RelayCommand(o => { Application.Current.MainWindow.WindowState = WindowState.Minimized; });
        }
    }
}
