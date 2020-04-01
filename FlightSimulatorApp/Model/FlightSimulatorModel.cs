using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace FlightSimulatorApp.Model
{
    static class PropertiesIndex
    {
        public const int AIRSPEED = 0;
        public const string AIRSPEEDPATH = "/instrumentation/airspeed-indicator/indicated-speed-kt";
        public const int ALTIMETER = 1;
        public const int ALTITUDE = 2;
        public const int HEADING = 3;
        public const int ROLL = 4;
        public const int GROUNGSPEED = 5;
        public const int PITCH = 6;
        public const int VERTICALSPEED = 7;
    }



    public class FlightSimulatorModel : IFlightSimulatorModel
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

        private bool connectionStatus;
        private string warningMessage;

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
            {"altimeter", "/instrumentation/altimeter/indicated-altitude-ft" },
            {"groundSpeed", "/instrumentation/gps/indicated-ground-speed-kt"},
            {"verticalSpeed", "/instrumentation/gps/indicated-vertical-speed" },
            {"airSpeed", "/instrumentation/airspeed-indicator/indicated-speed-kt" },
            {"Elevator","/controls/flight/elevator" },
            {"Aileron","/controls/flight/aileron" },
            {"Rudder","/controls/flight/rudder" },
            {"Throttle", "/controls/engines/current-engine/throttle"},
        };


        public FlightSimulatorModel(ISimulatorConnector connector)
        {
            this.connector = connector;
            this.stop = false;
            this.WarningMessage = "no message yet";
        }


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
        //#endregion



        public void NotifyPropertyChanged(string propName)
        {
 
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

        public double Latitude { get { return latitude; }
                                 set { latitude = value; NotifyPropertyChanged("Latitude"); } }
        public bool ConnectionStatus
        {
            get { return connectionStatus; }
            set { connectionStatus = value; NotifyPropertyChanged("ConnectionStatus"); }
        }

        public string WarningMessage
        {
            get { return warningMessage; }
            set { warningMessage = value; NotifyPropertyChanged("WorningStatus"); }
        }

        public double Longitude
        {
            get { return longitude; }
            set { longitude = value; NotifyPropertyChanged("Longitude"); }
        }

        //joistick properties
        public double Elevator
        {
            get
            {
                return elevator;
            }
            set
            {
                elevator = value;
                
                
            }
        }
        public double Aileron
        {
            get
            {
                return aileron;
            }
            set
            {
                aileron = value;
                

            }
        }
        public double Rudder
        {
            get
            {
                return rudder;
            }
            set
            {
                rudder = value;
            }
        }
        public double Throttle
        {
            get
            {
                return throttle;
            }
            set
            {
                throttle = value;
            }
        }






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
            Thread t = new Thread(delegate ()
            {
                while (!stop)
                {
                    infoRequest();
                    interpretInfo(this.connector.read());
                    //TODO handle err
                    Thread.Sleep(250);
                }
                disconnect();
            });
            t.Start();
            //t.Join();

        }

        private void infoRequest()
        {
            Console.WriteLine(PropertiesSimulatorPath["roll"]);
            connector.write("get " + PropertiesSimulatorPath["airSpeed"] + "\n" +
                            "get " + PropertiesSimulatorPath["altimeter"] + "\n" +
                            "get " + PropertiesSimulatorPath["altitude"] + "\n" +
                            "get " + PropertiesSimulatorPath["heading"] + "\n" +
                            "get " + PropertiesSimulatorPath["roll"] + "\n" +
                            "get " + PropertiesSimulatorPath["groundSpeed"] + "\n" +
                            "get " + PropertiesSimulatorPath["pitch"] + "\n" +
                            "get " + PropertiesSimulatorPath["verticalSpeed"] + "\n"
                            );
        }

        private void interpretInfo(string info) {
            Console.WriteLine(info);
            string[] values = info.Split('\n');
            warningMessage = "";
            try
            {
                AirSpeed = Double.Parse(values[PropertiesIndex.AIRSPEED]);
            }
            catch (Exception e)
            {
                warningMessage = "eror getting updated airspeed value";
            }
            try {
                Altimeter = Double.Parse(values[PropertiesIndex.ALTIMETER]);
            }
            catch (Exception e)
            {
                warningMessage = "eror getting updated altimeter value";
            }
            try
            {
                Altitude = Double.Parse(values[PropertiesIndex.ALTITUDE]);
            }
            catch (Exception e)
            {
                warningMessage = "eror getting updated altitude value";
            }
            try
            {
                Heading = Double.Parse(values[PropertiesIndex.HEADING]);
            }
            catch (Exception e)
            {
                warningMessage = "eror getting updated heading value";
            }
            try
            {
                Roll = Double.Parse(values[PropertiesIndex.ROLL]);
            }
            catch (Exception e)
            {
                warningMessage = "eror getting updated roll value";
            }
            try {
                GroundSpeed = Double.Parse(values[PropertiesIndex.GROUNGSPEED]);
            }
            catch (Exception e)
            {
                warningMessage = "eror getting updated groundSpeed value";
            }
            try
            {
                Pitch = Double.Parse(values[PropertiesIndex.PITCH]);
            }
            catch (Exception e)
            {
                warningMessage = "eror getting updated pitch value";
            }
            try
            { 
                VerticalSpeed = Double.Parse(values[PropertiesIndex.VERTICALSPEED]);
            }
            catch(Exception e)
            { 
                WarningMessage = "eror getting updated verticalSpeed value";
            }
            Console.WriteLine(warningMessage);
            //this.stop = true;




        }

       public void SendControlInfo(string propName)
        {
            connector.write("set " + PropertiesSimulatorPath[propName] + " " + typeof(FlightSimulatorModel).GetProperty(propName).ToString());
            Console.WriteLine(typeof(FlightSimulatorModel).GetProperty(propName).GetValue(this, null));
            if (connector.read() == "ERR")
            {
                warningMessage = "eror responding to " + propName;
            }
            Console.WriteLine(connector.read());
        }
    }
}
