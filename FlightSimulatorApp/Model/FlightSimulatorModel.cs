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

        private double altitude;
        private double roll;
        private double pitch;
        private double headinge;
        private double altimeter;
        private double groungSpeed;
        private double verticalSpeed;
        private double airSpeed;

        public FlightSimulatorModel(ISimulatorCommunicator communicator)
        {
            this.communicator = communicator;
            this.stop = false;
        }

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged!= null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
        //dashboard proprties
        public double Altitude { get { throw new NotImplementedException(); } 
                                 set { altitude = value;  NotifyPropertyChanged("Altitude"); } }
        public double Roll { get { throw new NotImplementedException(); }
                             set { roll = value; NotifyPropertyChanged("Roll"); } }
        public double Pitch { get { throw new NotImplementedException(); } set { pitch = value; NotifyPropertyChanged("Pitch"); } }
        public double Altimeter { get { throw new NotImplementedException(); } set { altimeter = value; NotifyPropertyChanged("Altimeter"); } }
        public double Heading { get { throw new NotImplementedException(); } set { } }
        public double GroundSpeed { get { throw new NotImplementedException(); } set { } }
        public double VerticalSpeed { get { throw new NotImplementedException(); } set { } }
        public double AirSpeed { get { throw new NotImplementedException(); } set { } }




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
