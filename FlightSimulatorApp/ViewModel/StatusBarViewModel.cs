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

    // this is the view model for status bar, map.
    public class StatusBarViewModel : INotifyPropertyChanged
    {
        private readonly FlightSimulatorModel model;
        public event PropertyChangedEventHandler PropertyChanged;
        private string connected_message = "Disconnected";
        private Uri connection_image = new Uri(@"/Views/Resources/disconnected.png", UriKind.RelativeOrAbsolute);
        private double longtitude;
        private double latitude;
        private string warning_message;
        private string loc = "0,0";
        private MapMode map_mode = new RoadMode();

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
            if (propName == "VM_ConnectionStatus")
            {
                if (model.ConnectionStatus == true)
                {
                    VM_ConnectionImagePath = new Uri(@"/Views/Resources/connected.png", UriKind.RelativeOrAbsolute);
                    VM_ConnectionMessage = "Connected";
                }
                else
                {
                    VM_ConnectionImagePath = new Uri(@"/Views/Resources/disconnected.png", UriKind.RelativeOrAbsolute);
                    VM_ConnectionMessage = "Disconnected";
                }
            }
            if (propName == "VM_Longitude")
            {
                VM_Longitude = model.Longitude;
                VM_Location =  model.Longitude.ToString()+ "," + model.Latitude.ToString(); ;
            }
            if (propName == "VM_Latitude")
            {
                VM_Latitude = model.Latitude;
                VM_Location = model.Longitude.ToString()+ "," +  model.Latitude.ToString();
            }

            if (propName == "VM_WarningMessage")
            {
                VM_WarningMessage = model.WarningMessage;
            }


        }

        // connection status properties
        public bool VM_ConnectionStatus
        {
            get { return model.ConnectionStatus; }

        }

        public Uri VM_ConnectionImagePath
        {
            get 
            {
                return this.connection_image;
            }
            set
            {
                this.connection_image = value;
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
            get { return this.warning_message; }
            set
            {
                this.warning_message = value;
                OnPropertyChanged("VM_WarningMessage");
            }

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

        public string VM_Location
        {
            get { return this.loc; }
            set
            {
                this.loc = value;
                OnPropertyChanged("VM_Location");
            }
        }

        public string VM_Heading
        {
            get { return model.Heading; }
        }

        public string VM_MapModeString 
        {
            get
            {
                return Properties.Settings.Default.MapMode;
            }
            set
            {
                Properties.Settings.Default.MapMode = value;
                if (value == "Aerial")
                {
                    VM_MapMode = new AerialMode();
                } else if (value == "Roads")
                {
                    VM_MapMode = new RoadMode();

                } else if (value == "AerialWithLabels")
                {
                    VM_MapMode = new MercatorMode();
                }
                OnPropertyChanged("VM_MapMode");
            }
        }

        public MapMode VM_MapMode
        {
            get
            {
                return this.map_mode;
            }
            set
            {
                this.map_mode = value;
            }
        } 

        public void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));

        }
    }
}
