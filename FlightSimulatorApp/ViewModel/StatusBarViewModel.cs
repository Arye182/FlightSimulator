using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using FlightSimulatorApp.Model;
using System.Windows;

namespace FlightSimulatorApp.ViewModel
{
    class StatusBarViewModel : INotifyPropertyChanged
    {
        private FlightSimulatorModel model;

        public event PropertyChangedEventHandler PropertyChanged;


        public StatusBarViewModel()
        {
            this.model = (Application.Current as App).Model; ;
            model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }
        public void NotifyPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        
        public bool VM_ConnectionStatus
        {
            get { return model.ConnectionStatus; }
        }
        
        public string VM_WarningMessage
        {
            get { return model.WarningMessage; }
        }

        public double VM_Longitude 
        {
            get { return model.Longitude; }
        }

        public double  VM_Latitude
        {
            get { return model.Latitude; }
        }
    }
}
