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
            get
            {
                if (VM_ConnectionStatus == true)
                {
                    return "/Views/Resources/connected.png";
                }
                else
                {
                    return "/Views/Resources/disconnected.png";
                }
            }
        }

        public string VM_ConnectionMessage
        {
            get
            {
                if (VM_ConnectionStatus)
                {
                    return "Connected";
                }
                else
                {
                    return "Disconnected";
                }
            }
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


    }
}
