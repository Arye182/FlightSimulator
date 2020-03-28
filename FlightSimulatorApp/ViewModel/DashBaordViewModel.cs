using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightSimulatorApp.Model;
namespace FlightSimulatorApp.ViewModel
{
    class DashBaordViewModel : INotifyPropertyChanged
    {
        private IFlightSimulatorModel model;

        public event PropertyChangedEventHandler PropertyChanged;


        public DashBaordViewModel(IFlightSimulatorModel model)
        {
            this.model = model;
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


        public double VM_Altitude
        {
            get { return model.Altitude; }
        }
        public double VM_Roll
        {
            get { return model.Roll; }
        }
        public double VM_Pitch
        {
            get { return model.Pitch; }
        }
        public double VM_Altimeter
        {
            get { return model.Altimeter; }
        }
        public double VM_Heading
        {
            get { return model.Heading; }
        }
        public double VM_GroundSpeed
        {
            get { return model.GroundSpeed; }
        }
        public double VM_VerticalSpeed
        {
            get { return model.VerticalSpeed; }
        }
        public double VM_AirSpeed
        {
            get { return model.AirSpeed; }
        }

    }
}
