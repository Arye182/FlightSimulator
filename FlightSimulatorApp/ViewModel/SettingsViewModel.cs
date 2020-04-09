using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp.ViewModel
{
    public class SettingsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string VM_IP
        {
            get { return Properties.Settings.Default.IP; }
            set
            {
                Properties.Settings.Default.IP = value;
                OnPropertyChanged("VM_IP");
            }
        }

        public int VM_Port
        {
            get { return Properties.Settings.Default.Port; }
            set
            {
                Properties.Settings.Default.Port = value;
                OnPropertyChanged("VM_Port");
            }
        }

        public int VM_MainWindowWidth
        {
            get { return Properties.Settings.Default.MainWindowWidth; }
            set
            {
                Properties.Settings.Default.MainWindowWidth = value;
                OnPropertyChanged("VM_MainWindowWidth");
            }
        }

        public int VM_MainWindowHeigth
        {
            get { return Properties.Settings.Default.MainWindowHeight; }
            set
            {
                Properties.Settings.Default.MainWindowHeight = value;
                OnPropertyChanged("VM_MainWindowHeigth");
            }
        }

        public void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));

        }
    }
}
