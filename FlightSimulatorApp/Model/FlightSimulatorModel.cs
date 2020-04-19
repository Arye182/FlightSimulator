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
        public const int ALTIMETER = 1;
        public const int ALTITUDE = 2;
        public const int HEADING = 3;
        public const int ROLL = 4;
        public const int GROUNDSPEED = 5;
        public const int PITCH = 6;
        public const int VERTICALSPEED = 7;
        public const int LATITUDE = 8;
        public const int LONGITUDE = 9;
    }

    public class FlightSimulatorModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private MySimulatorConnector connector;
        private Queue<KeyValuePair<string, string>> setCommands;

        private string simOutput;
        private double throttle;
        private double aileron;
        private double elevator;
        private double rudder;
        /*Dashboard*/
        private string altitude;
        private string roll;
        private string pitch;
        private string heading;
        private string altimeter;
        private string groundSpeed;
        private string verticalSpeed;
        private string airSpeed;

        /*statusBar*/
        private bool connectionStatus = false;
        private string warningMessage;
        private string latitude;
        private string longitude;

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

        public FlightSimulatorModel(MySimulatorConnector connector)
        {
            this.connector = connector;
            this.WarningMessage = "no message yet";
            this.connectionStatus = false;
            this.setCommands = new Queue<KeyValuePair<string, string>>();
        }

        public void NotifyPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
        /*dashboard proprties*/
        public string Altitude
        {
            get { return altitude; }
            set { altitude = value.IsNormalized() ? string.Format("0.000", Double.Parse(value)) : value; ; NotifyPropertyChanged("Altitude"); }
        }
        public string Roll
        {
            get { return roll; }
            set { roll = value.IsNormalized() ? string.Format("0.000", Double.Parse(value)) : value; ; NotifyPropertyChanged("Roll"); }
        }
        public string Pitch
        {
            get { return pitch; }
            set { pitch = value.IsNormalized() ? string.Format("0.000", Double.Parse(value)) : value; ; NotifyPropertyChanged("Pitch"); }
        }
        public string Altimeter
        {
            get { return altimeter; }
            set { altimeter = value.IsNormalized() ? string.Format("0.000", Double.Parse(value)) : value; ; NotifyPropertyChanged("Altimeter"); }
        }
        public string Heading
        {
            get { return heading; }
            set { heading = value.IsNormalized() ? string.Format("0.000", Double.Parse(value)) : value; ; NotifyPropertyChanged("Heading"); }
        }
        public string GroundSpeed
        {
            get { return groundSpeed; }
            set { groundSpeed = value.IsNormalized() ? string.Format("0.000", Double.Parse(value)) : value; ; NotifyPropertyChanged("GroundSpeed"); }
        }
        public string VerticalSpeed
        {
            get { return verticalSpeed; }
            set { verticalSpeed = value.IsNormalized() ? string.Format("0.000", Double.Parse(value)) : value; ; NotifyPropertyChanged("VerticalSpeed"); }
        }
        public string AirSpeed
        {
            get { return airSpeed; }
            set {airSpeed = value.IsNormalized() ? string.Format("0.000", Double.Parse(value)) : value; 
                NotifyPropertyChanged("AirSpeed"); }
        }

        /*StatusBar properties*/
        public string Latitude
        {
            get { return latitude; }
            set { if (value.IsNormalized())
                {
                    if ((Double.Parse(value) > 180) || (Double.Parse(value) < -180))
                    {
                        WarningMessage = "latitude value is illegal";
                    }
                    latitude = string.Format("0.000", Double.Parse(value));
                }
                else
                {
                    latitude = value; ;
                }
                NotifyPropertyChanged("Latitude");
            }  
        }
        public bool ConnectionStatus
        {
            get { return connectionStatus; }
            set { connectionStatus = value; NotifyPropertyChanged("ConnectionStatus"); }
        }

        public string WarningMessage
        {
            get { return warningMessage; }
            set
            {
                warningMessage = value; NotifyPropertyChanged("WarningMessage");
            }
        }

        public string Longitude
        {
            get { return longitude; }
            set {
                if (value.IsNormalized())
                {
                    if ((Double.Parse(value) > 90) || (Double.Parse(value) < -90))
                    {
                        WarningMessage = "longitude value is illegal";
                    }
                    longitude = String.Format("0.000", Double.Parse(value));
                }
                else
                {
                    longitude = value; ;
                }
                NotifyPropertyChanged("Latitude");
            }
        }

        /*joistick properties*/
        public double Elevator
        {
            get
            {
                return elevator;
            }
            set
            {
                elevator = value;
                SendControlInfo("Elevator");
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
                SendControlInfo("Aileron");
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
               SendControlInfo("Rudder");
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
                SendControlInfo("Throttle");
            }
        }

        public void Connect(string ip, int port)
        {
            try {
                this.connector.connect(ip, port);
            }
            catch
            {
                WarningMessage = "server is not connected";
            }
            if (connector.isConnected)
            {
                ConnectionStatus = true;
            }
        }
        public void Disconnect()
        {
            WarningMessage = "Disconnected";
            ConnectionStatus = false;
            InitProperties();
        }
        public void Start()
        {
                Thread t = new Thread(delegate ()
                {
                    while (connectionStatus)
                    {

                        simOutput  = InfoRequest();
                        if (simOutput != "Connection failure")
                        {
                            InterpretInfo(simOutput);
                        }
                        Thread.Sleep(250);
                    }
                    connector.disconnect();
                });
                t.Start();   
        }

        private string InfoRequest()
        {
            string output = "";
            try
            {
                output = connector.WriteCommand("get " + PropertiesSimulatorPath["airSpeed"] + "\n" +
                                "get " + PropertiesSimulatorPath["altimeter"] + "\n" +
                                "get " + PropertiesSimulatorPath["altitude"] + "\n" +
                                "get " + PropertiesSimulatorPath["heading"] + "\n" +
                                "get " + PropertiesSimulatorPath["roll"] + "\n" +
                                "get " + PropertiesSimulatorPath["groundSpeed"] + "\n" +
                                "get " + PropertiesSimulatorPath["pitch"] + "\n" +
                                "get " + PropertiesSimulatorPath["verticalSpeed"] + "\n" +
                                "get " + PropertiesSimulatorPath["longitude"] + "\n" +
                                "get " + PropertiesSimulatorPath["latitude"]
                                );
                while (setCommands.Any())
                {
                    if (connector.WriteCommand(setCommands.Dequeue().Value) == "ERR")
                    {
                        {
                            WarningMessage = "eror updating value in simulator";
                        }
                    }

                }
                Console.WriteLine(output);
                return output;
            }
            catch(TimeoutException e)
            {
                WarningMessage = "server is not responding...";
                return "Connection failure";
            }
            catch(Exception e)
            {
                WarningMessage = "connection failure";
                connector.disconnect();
                //connector.isConnected = false;
                ConnectionStatus = false;
                return "Connection failure";
            }
            
        }

        private void InterpretInfo(string info)
        {
            string[] values = info.Split('\n');
            WarningMessage = "";
            AirSpeed = values[PropertiesIndex.AIRSPEED];
            Altimeter = values[PropertiesIndex.ALTIMETER];
            Altitude = values[PropertiesIndex.ALTITUDE];
            Heading = values[PropertiesIndex.HEADING];
            Roll = values[PropertiesIndex.ROLL];
            GroundSpeed = values[PropertiesIndex.GROUNDSPEED];
            Pitch = values[PropertiesIndex.PITCH];
            VerticalSpeed = values[PropertiesIndex.VERTICALSPEED];
            Longitude = values[PropertiesIndex.LONGITUDE];
            Latitude = values[PropertiesIndex.LATITUDE];
        }

        public void SendControlInfo(string propName)
        {
            setCommands.Enqueue(new KeyValuePair<string, string>(propName, "set " + PropertiesSimulatorPath[propName] + " " + typeof(FlightSimulatorModel).GetProperty(propName).GetValue(this, null)));
        }

        private void InitProperties()
        {

            Altitude = "0.000";
            Roll = "0.000";
            Pitch = "0.000";
            Heading = "0.000";
            Altimeter = "0.000";
            GroundSpeed = "0.000";
            VerticalSpeed = "0.000";
            AirSpeed = "0.000";
        }
        
}
}
