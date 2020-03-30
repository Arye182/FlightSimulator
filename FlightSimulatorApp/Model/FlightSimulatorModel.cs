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

        private ISimulatorConnector connector;
        volatile Boolean stop;
        // bbbbbb
        // nnnn
        private double throttle;
        private double aileron;
        private double elevator;
        private double rudder;
        private double latitude;
        private double longitude;
        private double altitude;
        private double roll;
        private double pitch;
        private double heading;
        private double altimeter;
        private double groungSpeed;
        private double verticalSpeed;
        private double airSpeed;
        private SortedDictionary<string, string> PropertiesSimulatorPath = new SortedDictionary<string, string>()
        {
            {"throttle", "/controls/engines/current-engine/throttle" },
            {"aileron", "/controls/flight/aileron" },
            {"elevator", "/controls/flight/elevator" },
            {"rudder", "/controls/flight/rudder" },
            {"latitude", "/position/latitude-deg" },
            {"longitude", "/position/longitude-deg" },
            {"altitude" , "/instrumentation/gps/indicated-altitude-ft"},
            {"roll", "/instrumentation/attitude-indicator/internal-roll-deg" },
            {"pitch", "/instrumentation/attitude-indicator/internal-pitch-deg" },
            {"heading", "/instrumentation/heading-indicator/indicated-heading-deg" },
            {"alimeter", "/instrumentation/altimeter/indicated-altitude-ft" },
            {"groungSpeed", "/instrumentation/gps/indicated-ground-speed-kt"},
            {"verticalSpeed", "/instrumentation/gps/indicated-vertical-speed" },
            {"airSpeed", "/instrumentation/airspeed-indicator/indicated-speed-kt" },
        }
        ;


        public FlightSimulatorModel(ISimulatorConnector connector)
        {
            this.connector = connector;
            this.stop = false;
        }

        #region Singleton
        // private static FlightSimulatorModel m_Instance = null;
        //public static FlightSimulatorModel Instance
        //{
        //  get {
        //     if (m_Instance == null)
        //      {
        //           m_Instance = new FlightSimulatorModel(new MySimulatorconnector());
        //       }
        //       return m_Instance;
        //    }
        //}
        #endregion



        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
        //dashboard proprties
        public double Altitude { get { return altitude; }
            set { altitude = value; NotifyPropertyChanged("Altitude"); } }
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
            this.connector.connect(ip, port);
        }
        public void disconnect()
        {
            this.stop = true;
            this.connector.disconnect();

        }
        public void start()
        {
            new Thread(delegate ()
            {
                while (!stop)
                {
                    infoRequest();
                    interpretInfo(this.connector.read());
                     //TODO handle err
                     Thread.Sleep(250);
                }
            }).Start();
        }

        private void infoRequest()
        {
            Console.WriteLine(this.PropertiesSimulatorPath["airSpeed"]);
            connector.write("get" + this.PropertiesSimulatorPath["airSpeed"] + "\n" +
                            "get" + this.PropertiesSimulatorPath["altimeter"] + "\n" +
                            "get" + this.PropertiesSimulatorPath["altitude"] + "\n" +
                            "get" + this.PropertiesSimulatorPath["heading"] + "\n" +
                            "get" + this.PropertiesSimulatorPath["roll"] + "\n" +
                            "get" + this.PropertiesSimulatorPath["groundSpeed"] + "\n" +
                            "get" + this.PropertiesSimulatorPath["pitch"] + "\n" +
                            "get" + this.PropertiesSimulatorPath["verticalSpeed"] + "\n"
                            );                
        }

        private void interpretInfo(string info) {
            string[] values = info.Split('\n');
            AirSpeed = Double.Parse(values[0]);
            Altimeter = Double.Parse(values[1]);
            Altitude = Double.Parse(values[2]);
            Heading = Double.Parse(values[3]);
            Roll = Double.Parse(values[4]);
            GroundSpeed = Double.Parse(values[5]);
            Pitch = Double.Parse(values[6]);




        }
    }
}
