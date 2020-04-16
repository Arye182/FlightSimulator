using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightSimulatorApp.Model;
using FlightSimulatorApp;
using System.Windows;

namespace FlightSimulatorApp.ViewModel
{
    public class DashBaordViewModel : INotifyPropertyChanged
    {
        private FlightSimulatorModel model;

        public event PropertyChangedEventHandler PropertyChanged;


        public DashBaordViewModel()
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


        public string VM_Altitude
        {
            get { return model.Altitude; }
        }
        public string VM_Roll
        {
            get { return model.Roll; }
        }
        public string VM_Pitch
        {
            get { return model.Pitch; }
        }
        public string VM_Altimeter
        {
            get { return model.Altimeter; }
        }
        public string VM_Heading
        {
            get { return model.Heading; }
        }
        public string VM_GroundSpeed
        {
            get { return  model.GroundSpeed;}
}
        public string VM_VerticalSpeed
        {
            get { return model.VerticalSpeed; }
        }
        public string VM_AirSpeed
        {
            get { return model.AirSpeed; }
        }

    }
}
