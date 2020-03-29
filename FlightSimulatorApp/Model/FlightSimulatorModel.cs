using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace FlightSimulatorApp.Model
{
    class FlightSimulatorModel : IFlightSimulatorModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ISimulatorCommunicator communicator;
        volatile Boolean stop;
        // bbbbbb
        // nnnn
        private double altitude;
        private double roll;
        private double pitch;
        private double heading;
        private double altimeter;
        private double groungSpeed;
        private double verticalSpeed;
        private double airSpeed;
        
        public FlightSimulatorModel(ISimulatorCommunicator communicator)
        {
            this.communicator = communicator;
            this.stop = false;
        }

        #region Singleton
       // private static FlightSimulatorModel m_Instance = null;
        //public static FlightSimulatorModel Instance
        //{
          //  get {
           //     if (m_Instance == null)
          //      {
         //           m_Instance = new FlightSimulatorModel(new MySimulatorCommunicator());
         //       }
         //       return m_Instance;
        //    }
        //}
        #endregion

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged!= null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
        //dashboard proprties
        public double Altitude { get { return altitude; } 
                                 set { altitude = value;  NotifyPropertyChanged("Altitude"); } }
        public double Roll { get { return roll; }
                             set { roll = value; NotifyPropertyChanged("Roll"); } }
        public double Pitch { get { return pitch; } 
                              set { pitch = value; NotifyPropertyChanged("Pitch"); } }
        public double Altimeter { get { return altimeter; } 
                                  set { altimeter = value; NotifyPropertyChanged("Altimeter"); } }
        public double Heading { get { return heading; } 
                                set { heading = value; NotifyPropertyChanged("Heading"); } }
        public double GroundSpeed { get { return groungSpeed; }
                                    set { groungSpeed = value; NotifyPropertyChanged("GroungSpeed"); } }
        public double VerticalSpeed { get { return verticalSpeed; }
                                      set { verticalSpeed = value; NotifyPropertyChanged("VerticalSpeed"); } }
        public double AirSpeed { get { return airSpeed; } 
                                 set { airSpeed = value; NotifyPropertyChanged("AirSpeed"); } }




        public void connect(string ip, int port)
        {
            this.communicator.connect(ip, port);
        }
        public void disconnect()
        {
            this.stop = true;
            this.communicator.disconnect();
            
        }
        public void start()
        {
            new Thread(delegate ()
            {
                while(!stop)
                {
                    communicator.write("");
                    Altitude = Double.Parse(communicator.read());
                    communicator.write("");
                    Roll = Double.Parse(communicator.read());
                    communicator.write("");
                    Pitch = Double.Parse(communicator.read());
                    communicator.write("");
                    Altimeter = Double.Parse(communicator.read());
                    communicator.write("");
                    Heading = Double.Parse(communicator.read());
                    communicator.write("");
                    GroundSpeed = Double.Parse(communicator.read());
                    communicator.write("");
                    VerticalSpeed = Double.Parse(communicator.read());
                    communicator.write("");
                    AirSpeed = Double.Parse(communicator.read());
                    //TODO handle err
                    Thread.Sleep(250);
                }
            }).Start();
        }
    }
}
