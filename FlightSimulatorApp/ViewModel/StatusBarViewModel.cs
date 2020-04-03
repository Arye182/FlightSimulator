using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using FlightSimulatorApp.Model;
using System.Windows;
using FlightSimulatorApp;
using Microsoft.Maps.MapControl.WPF;

namespace FlightSimulatorApp.ViewModel
{
    public class StatusBarViewModel : INotifyPropertyChanged
    {
        private FlightSimulatorModel model;

        public event PropertyChangedEventHandler PropertyChanged;
        string connected_message = "Disconnected";
        string connnection_image = "/Views/Resources/disconnected.png";

        public StatusBarViewModel()
        {
            this.model = (Application.Current as App).Model;
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

        // connection status properties
        public bool VM_ConnectionStatus
        {
            get { return model.ConnectionStatus; }
        }

        public string VM_ConnectionImagePath
        {
            get { return this.connnection_image; }
            set { this.connnection_image = value; OnPropertyChanged("VM_ConnectionImagePath"); }
        }

        public string VM_ConnectionMessage
        {
            get { return this.connected_message; }
            set { this.connected_message = value; OnPropertyChanged("VM_ConnectionMessage"); }



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

        public Location VM_Location
        {
            get { return new Location(model.Latitude, model.Longitude); }
        }

        public void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
            if (propName == "VM_ConnectionStatus")
            {
                if (VM_ConnectionStatus)
                {
                    VM_ConnectionImagePath = "/Views/Resources/connected.png";
                    VM_ConnectionMessage = "Connected";
                }
                else
                {
                    VM_ConnectionImagePath = "/Views/Resources/disconnected.png";
                    VM_ConnectionMessage = "Disconnected";
                }
            }
        }
    }
}
