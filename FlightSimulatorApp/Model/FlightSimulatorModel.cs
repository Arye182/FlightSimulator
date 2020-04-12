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
        public const int LATITUDE = 8;
        public const int LONGITUDE = 9;
    }

    


    public class FlightSimulatorModel
    {

        

        public event PropertyChangedEventHandler PropertyChanged;

        private ISimulatorConnector connector;
        volatile Boolean stop;
        
        private double throttle;
        private double aileron;
        private double elevator;
        private double rudder;
        

        //Dashboard
        private double altitude;
        private double roll;
        private double pitch;
        private double heading;
        private double altimeter;
        private double groungSpeed;
        private double verticalSpeed;
        private string airSpeed;

        //statusBar
        private bool connectionStatus = false;
        private string warningMessage;
        private double latitude;
        private double longitude;

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

        //constructor
        public FlightSimulatorModel(ISimulatorConnector connector)
        {
            this.connector = connector;
            this.stop = false;
            this.WarningMessage = "no message yet";
            this.connectionStatus = false;
        }


    



        public void NotifyPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));

            }
        }
        //dashboard proprties
        public double Altitude
        {
            get { return altitude; }
            set { altitude = value; NotifyPropertyChanged("Altitude"); }
        }
        public double Roll
        {
            get { return roll; }
            set { roll = value; NotifyPropertyChanged("Roll"); }
        }
        public double Pitch
        {
            get { return pitch; }
            set { pitch = value; NotifyPropertyChanged("Pitch"); }
        }
        public double Altimeter
        {
            get { return altimeter; }
            set { altimeter = value; NotifyPropertyChanged("Altimeter"); }
        }
        public double Heading
        {
            get { return heading; }
            set { heading = value; NotifyPropertyChanged("Heading"); }
        }
        public double GroundSpeed
        {
            get { return groungSpeed; }
            set { groungSpeed = value; NotifyPropertyChanged("GroungSpeed"); }
        }
        public double VerticalSpeed
        {
            get { return verticalSpeed; }
            set { verticalSpeed = value; NotifyPropertyChanged("VerticalSpeed"); }
        }
        public string AirSpeed
        {
            get { return airSpeed; }
            set { airSpeed = value; NotifyPropertyChanged("AirSpeed"); }
        }

        //StatusBar properties
        public double Latitude
        {
            get { return latitude; }
            set { latitude = value; NotifyPropertyChanged("Latitude"); }
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






        public void connect(string ip, int port)
        {
            this.connector.connect(ip, port);
            ConnectionStatus = true;
        }
        public void disconnect()
        {
            
            this.connector.disconnect();
            WarningMessage = "Disconnected";
            ConnectionStatus = false;
        }
        public void start()
        {
            if (this.ConnectionStatus)
            {
                Thread t = new Thread(delegate ()
                {
                    while (connectionStatus)
                    {
                        
                        
                        interpretInfo(infoRequest());
                        //TODO handle err
                        Thread.Sleep(250);
                    }
                    
                });
                t.Start();
               // t.Join();
               

            }
        }

        private string infoRequest()
        {
            string output = "";
            try
            {
                output =  connector.WriteCommand("get " + PropertiesSimulatorPath["airSpeed"] + "\n" +
                                "get " + PropertiesSimulatorPath["altimeter"] + "\n" +
                                "get " + PropertiesSimulatorPath["altitude"] + "\n" +
                                "get " + PropertiesSimulatorPath["heading"] + "\n" +
                                "get " + PropertiesSimulatorPath["roll"] + "\n" +
                                "get " + PropertiesSimulatorPath["groundSpeed"] + "\n" +
                                "get " + PropertiesSimulatorPath["pitch"] + "\n" +
                                "get " + PropertiesSimulatorPath["verticalSpeed"] + "\n" +
                                "get " + PropertiesSimulatorPath["longitude"] + "\n" +
                                "get " + PropertiesSimulatorPath["latitude"] + "\n"
                                );
            } catch(System.NullReferenceException)
            {
                connect("127.0.0.1", 5402);
            }
            return output;
        }

        private void interpretInfo(string info)
        {
            Console.WriteLine(info);
            string[] values = info.Split('\n');
            WarningMessage = "";
            AirSpeed = values[PropertiesIndex.AIRSPEED];
            try
            {
                Altimeter = Double.Parse(values[PropertiesIndex.ALTIMETER]);
            }
            catch (Exception e)
            {
                WarningMessage = "eror getting updated altimeter value";
            }
            try
            {
                Altitude = Double.Parse(values[PropertiesIndex.ALTITUDE]);
            }
            catch (Exception e)
            {
                WarningMessage = "eror getting updated altitude value";
            }
            try
            {
                Heading = Double.Parse(values[PropertiesIndex.HEADING]);
            }
            catch (Exception e)
            {
                WarningMessage = "eror getting updated heading value";
            }
            try
            {
                Roll = Double.Parse(values[PropertiesIndex.ROLL]);
            }
            catch (Exception e)
            {
                WarningMessage = "eror getting updated roll value";
            }
            try
            {
                GroundSpeed = Double.Parse(values[PropertiesIndex.GROUNGSPEED]);
            }
            catch (Exception e)
            {
                WarningMessage = "eror getting updated groundSpeed value";
            }
            try
            {
                Pitch = Double.Parse(values[PropertiesIndex.PITCH]);
            }
            catch (Exception e)
            {
                WarningMessage = "eror getting updated pitch value";
            }
            try
            {
                VerticalSpeed = Double.Parse(values[PropertiesIndex.VERTICALSPEED]);
            }
            catch (Exception e)
            {
                WarningMessage = "eror getting updated verticalSpeed value";
            }
            try
            {
                Longitude = Double.Parse(values[PropertiesIndex.LONGITUDE]);
            }
            catch (Exception e)
            {
                WarningMessage = "eror getting updated longitude value";
            }
            try
            {
                Latitude = Double.Parse(values[PropertiesIndex.LATITUDE]);
            }
            catch (Exception e)
            {
                WarningMessage = "eror getting updated latitude value";
            }
            
            Console.WriteLine(warningMessage);
            //this.stop = true;




        }

        public void SendControlInfo(string propName)
        {
            if (!connectionStatus)
            {
                return;
            }
            string output ="";
            Task send = Task.Run(() => {
             
             output = connector.WriteCommand("set " + PropertiesSimulatorPath[propName] + " " + typeof(FlightSimulatorModel).GetProperty(propName).GetValue(this, null) + "\n");
                });
            send.Wait();
            if (output == "ERR")
            {
                WarningMessage = "eror writing to the simulator";
            }
           /* if (connectionStatus)
            {

                
                send.Wait();
                //Console.WriteLine("set " + PropertiesSimulatorPath[propName] + " " + typeof(FlightSimulatorModel).GetProperty(propName) + "\n");
                connector.write("set " + PropertiesSimulatorPath[propName] + " " + typeof(FlightSimulatorModel).GetProperty(propName).GetValue(this, null) + "\n");
                
                //Console.WriteLine(typeof(FlightSimulatorModel).GetProperty(propName).GetValue(this, null));
                
                
                    string output = connector.read();
                    if (output == "ERR")
                    {
                        warningMessage = "eror responding to " + propName;
                    }
                    Console.WriteLine(output);
                
               
            }
            else
            {
                warningMessage = "please connect";
            }*/

        }
}
}
