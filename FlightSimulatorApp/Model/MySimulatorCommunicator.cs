using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace FlightSimulatorApp.Model
{
    class MySimulatorCommunicator : ISimulatorCommunicator
    {
        TcpClient my_client;
        private NetworkStream client_stream = null;
        public void connect(string ip, int port) { 
            try
            {
                Console.WriteLine("connecting to ip: {0}, port: {1}", ip, port.ToString());
                my_client = new TcpClient(ip, port);
                client_stream = my_client.GetStream();
                client_stream.Flush();
                //isConnected = true;
                Console.WriteLine("Connected!");
            } catch (Exception ex)
            {
                Console.WriteLine("EROR - failed to connect please try again");
                // test
                // test
            }
        }
        public void write(string command)
        {
            byte[] buffer = Encoding.ASCII.GetBytes(command);
            client_stream.Write(buffer, 0, buffer.Length);
        }
        public string read() {
            return "err";
        }
        public void disconnect() { }
    }
}
