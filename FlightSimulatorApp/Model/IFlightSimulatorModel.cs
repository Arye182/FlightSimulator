using System.ComponentModel;
namespace FlightSimulatorApp.Model
{
    public interface IFlightSimulatorModel : INotifyPropertyChanged
    {
        void Connect(string ip, int port);
        void Disconnect();
        void Start();
        //dashboard properties
        double Altitude { get; set; }
        double Roll { get; set; }
        double Pitch { get; set; }
        double Altimeter { get; set; }
        double Heading { get; set; }
        double GroundSpeed { get; set; }
        double VerticalSpeed { get; set; }
        double AirSpeed { get; set; }
    }

}

