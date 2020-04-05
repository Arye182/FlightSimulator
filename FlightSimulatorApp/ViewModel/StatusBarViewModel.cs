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
using System.Globalization;
using System.Windows.Media.Imaging;
using System.Windows.Data;

namespace FlightSimulatorApp.ViewModel
{
    public class StatusBarViewModel : INotifyPropertyChanged
    {
        private FlightSimulatorModel model;

        public event PropertyChangedEventHandler PropertyChanged;
        private string connected_message = "Disconnected";
        private string connnection_image = "/Views/Resources/disconnected.png";
        private double longtitude;
        private double latitude;
        private Location loc;

        public StatusBarViewModel()
        {
            this.model = (Application.Current as App).Model;
            this.loc = new Location();
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
            if (propName == "VM_ConnectionStatus")
            {
                if (model.ConnectionStatus == true)
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
            if (propName == "VM_Longitude")
            {
                VM_Longitude = model.Longitude;
                this.loc.Longitude = model.Longitude;
            }
            if (propName == "VM_Latitude")
            {
                VM_Latitude = model.Latitude;
                this.loc.Latitude = model.Latitude;
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
                return this.connnection_image;
            }
            set
            {
                this.connnection_image = value;
                OnPropertyChanged("VM_ConnectionImagePath");
            }
            
        }

        public string VM_ConnectionMessage
        {
            get
            {
                return this.connected_message;


            }

            set
            {
                this.connected_message = value;
                OnPropertyChanged("VM_ConnectionMessage");
            }

        }

        public string VM_WarningMessage
        {
            get { return model.WarningMessage; }

        }

        public double VM_Longitude 
        {
            get { return this.longtitude;  }
            set
            {
                this.longtitude = value;
                OnPropertyChanged("VM_Longitude");
            }
        }

        public double  VM_Latitude
        {
            get { return this.latitude; }
            set
            {
                this.latitude = value;
                OnPropertyChanged("VM_Latitude");
            }
        }

        public Location VM_Location
        {
            get { return this.loc; }
            set
            {
                this.loc = value;
                OnPropertyChanged("VM_Location");
            }
        }

        public void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));

        }
    }


}
