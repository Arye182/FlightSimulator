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
    public class MyJoystickViewModel : INotifyPropertyChanged
    {

        // data members
        
        double elevator = 0;
        double aileron = 0;
        double rudder = 0;
        double throttle = 0;
        double prev_elevator = 0;
        double prev_aileron = 0;
        double prev_rudder = 0;
        double prev_throttle = 0;
        double throttle_angel = -85;
        double aileron_angel = 0;
        public event PropertyChangedEventHandler PropertyChanged;
        private readonly FlightSimulatorModel model;
        

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
                if (Math.Abs(elevator - prev_elevator) < 0.01)
                {
                    return;
                }
                prev_elevator = value;
                if (model.ConnectionStatus)
                {
                    model.Elevator = value;
                }
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
                AileronAngle = (180 / Math.PI) * (Math.Asin(value));
                if (AileronAngle > 85)
                {
                    AileronAngle = 85;
                }
                if (AileronAngle < -85)
                {
                    AileronAngle = -85;
                }

                if (Math.Abs(aileron - prev_aileron) < 0.01)
                {
                    return;
                }
                prev_aileron = value;
                if (model.ConnectionStatus) {
                    model.Aileron = value;
                }
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
                if (Math.Abs(rudder - prev_rudder) < 0.01)
                {
                    return;
                }
                prev_rudder = value;
                if (model.ConnectionStatus)
                {
                    model.Rudder = value;
                }
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
                ThrottleAngle = 180 * value - 85;
                if (ThrottleAngle > 85)
                {
                    ThrottleAngle = 85;
                }
                if (ThrottleAngle < -85)
                {
                    ThrottleAngle = -85;
                }

                if (Math.Abs(throttle - prev_throttle) < 0.01)
                {
                    return;
                }
                prev_throttle = value;
                if (model.ConnectionStatus)
                {
                    model.Throttle = value;
                }
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
        public double ThrottleAngle
        {
            get
            {
                return throttle_angel;
            }

            private set
            {
                throttle_angel = value;
                OnPropertyChanged("ThrottleAngle");
            }
        }
        public double AileronAngle
        {
            get
            {
                return aileron_angel;
            }

            private set
            {
                aileron_angel = value;
                OnPropertyChanged("AileronAngle");
            }
        }

        // methods
        public MyJoystickViewModel()
        {
            this.model = (Application.Current as App).Model;
        }
        public void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        public void NotifyPropertyChanged(string propName)
        {
            model.SendControlInfo(propName);
        }
    }
}
