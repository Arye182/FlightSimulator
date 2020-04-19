namespace FlightSimulatorApp.Model
{
    public interface ISimulatorConnector
    {
        void Connect(string ip, int port);
        void Write(string command);
        string Read();
        void Disconnect();
        string WriteCommand(string command);
    }
}
