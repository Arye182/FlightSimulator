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
                if (prev_elevator == 0)
                {
                    prev_elevator = value;
                } else
                {
                    prev_elevator = elevator;
                }
                
                elevator = value;
                OnPropertyChanged("ElevatorString");
                OnPropertyChanged("Elevator");

                if (elevator - prev_elevator >= 0.1)
                {
                    model.Elevator = value;
                }
                
                //NotifyPropertyChanged("Elevator");
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

                if (prev_aileron == 0)
                {
                    prev_aileron = value;
                }
                else
                {
                    prev_aileron = aileron;
                }


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
                OnPropertyChanged("AileronString");
                OnPropertyChanged("Aileron");

                if (aileron - prev_aileron >= 0.1)
                {
                    model.Aileron = value;
                }
                
                //NotifyPropertyChanged("Aileron");
                
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
                if (prev_rudder == 0)
                {
                    prev_rudder = value;
                }
                else
                {
                    prev_rudder = rudder;
                }

                rudder = value;
                OnPropertyChanged("RudderString");
                OnPropertyChanged("Rudder");

                if (rudder - prev_rudder >= 0.1)
                {
                    model.Rudder = value;
                }
                //NotifyPropertyChanged("Rudder");

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
                if (prev_throttle == 0)
                {
                    prev_throttle = value;
                }
                else
                {
                    prev_throttle = throttle;
                }
                throttle = value;
                // arccos(x- 0.5Pi) - 0.5Pi
                // Angle = (180 / Math.PI ) * (Math.Acos(value - 0.5 * Math.PI) - (0.5*Math.PI)) ;
                ThrottleAngle = 180 * value - 85;
                if (ThrottleAngle > 85)
                {
                    ThrottleAngle = 85;
                }
                if (ThrottleAngle < -85)
                {
                    ThrottleAngle = -85;
                }

                OnPropertyChanged("ThrottleString");
                OnPropertyChanged("Throttle");

                if (throttle - prev_throttle >= 0.1)
                {
                    model.Throttle = value;
                }
                //NotifyPropertyChanged("Throttle");
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
