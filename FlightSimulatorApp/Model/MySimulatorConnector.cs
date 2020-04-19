using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace FlightSimulatorApp.Model
{
    public class MySimulatorConnector : ISimulatorConnector
    {

        private static readonly Mutex mut = new Mutex();
        TcpClient my_client;
        public int conectionAttempts;
        public bool isConnected = false;
        public void Connect(string ip, int port)
        {
            my_client = new TcpClient(ip, port);
            isConnected = true;

        }
        public void Write(string command)
        {
            byte[] buffer = Encoding.ASCII.GetBytes(command + "\n");

            try
            {
                NetworkStream stream = this.my_client.GetStream();
                stream.Flush();
                stream.Write(buffer, 0, buffer.Length);
                Console.WriteLine("enter write scope");
            }
            catch
            {

            }
        }
        public string Read()
        {
            my_client.ReceiveTimeout = 10000;
            string incomingInfo;
            byte[] buffer = new byte[1024];
            NetworkStream stream = this.my_client.GetStream();
            try
            {
                stream.Read(buffer, 0, buffer.Length);
            }
            catch (System.IO.IOException e)
            {
                Console.WriteLine(e.Message);
                throw new TimeoutException();
            }
            stream.Flush();
            incomingInfo = Encoding.ASCII.GetString(buffer, 0, buffer.Length);
            return incomingInfo.Substring(0, incomingInfo.IndexOf('\n') + 1);
        }
        public void Disconnect()
        {
            if (isConnected)
            {
                Console.WriteLine("disconnect");
                this.my_client.Close();
                isConnected = false;
            }
        }

        public string WriteCommand(string command)
        {
            Console.WriteLine(command);
            string[] commands = command.Split('\n');
            string output = "";
            foreach (var word in commands)
            {
                Console.WriteLine(word);
                mut.WaitOne();
                Write(word);
                output += Read();
                Console.WriteLine(output);
                mut.ReleaseMutex();
            }
            Console.WriteLine("reach end of for scope");
            return output;
        }
    }
}
