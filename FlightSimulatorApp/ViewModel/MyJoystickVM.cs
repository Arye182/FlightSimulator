using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightSimulatorApp.Model;
using FlightSimulatorApp;
using System.Windows;

namespace FlightSimulatorApp.ViewModel
{
    public class MyJoystickVM : INotifyPropertyChanged
    {

        // data members
        double elevator=0;
        double aileron=0;
        double rudder=0;
        double throttle=0;
        public event PropertyChangedEventHandler PropertyChanged;
        private FlightSimulatorModel model;

        // properties
        public double VM_Elevator
        {
            get
            {
                return elevator;
            }
            set
            {
                elevator = value;
                OnPropertyChanged("ElevatorString");
                OnPropertyChanged("Elevator");
            }
        }
        public double VM_Aileron
        {
            get
            {
                return aileron;
            }
            set
            {
                aileron = value;
                OnPropertyChanged("AileronString");
                OnPropertyChanged("Aileron");
                
            }
        }
        public double VM_Rudder
        {
            get
            {
                return rudder;
            }
            set
            {
                rudder = value;
                OnPropertyChanged("RudderString");
                OnPropertyChanged("Rudder");
            }
        }
        public double VM_Throttle
        {
            get
            {
                return throttle;
            }
            set
            {
                throttle = value;
                OnPropertyChanged("ThrottleString");
                OnPropertyChanged("Throttle");
            }
        }



        public string ElevatorString
        {
            get
            {
                return VarStringFactoring(elevator);
            }
        }

        public string AileronString
        {
            get
            {
                return VarStringFactoring(aileron);
            }
        }

        public string RudderString
        {
            get
            {
                return VarStringFactoring(rudder);
            }
        }

        public string ThrottleString
        {
            get
            {
                return VarStringFactoring(throttle);
            }
        }

        public string VarStringFactoring(double var)
        {
            double intermediate = Math.Truncate(var * 100) / 100;
            return String.Format("{0:N2}", intermediate);
        }



        // methods
        public MyJoystickVM()
        {
            this.model = (Application.Current as App).Model;
            //PropertyChanged += NotifyPropertyChanged;
        }
        public void OnPropertyChanged(string propName)
        {f
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

    }
}
