using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Threading;

namespace FlightSimulatorApp.Model
{
    public class MySimulatorConnector : ISimulatorConnector
    {

        private static Mutex mut = new Mutex();
        TcpClient my_client;
        public int conectionAttempts;
        public bool isConnected = false;
        public void connect(string ip, int port) { 
             
                Console.WriteLine("connecting to ip: {0}, port: {1}", ip, port.ToString());
                my_client = new TcpClient(ip, port);
                isConnected = true;
                //write_stream = my_client.GetStream();
                //write_stream.Flush();
                //read_stream = my_client.GetStream();
                //read_stream.Flush();
                
                //Console.WriteLine("Connected!");

        }
        public void write(string command)
        {
            //mut.WaitOne();
            byte[] buffer = Encoding.ASCII.GetBytes(command+"\n");
            
            try {
                NetworkStream stream = this.my_client.GetStream();
                stream.Flush();
                stream.Write(buffer, 0, buffer.Length);
                Console.WriteLine("enter write scope");
            }  catch(Exception ex) {
                
            }
        }
        public string read()
        {
            my_client.SendTimeout = 1000;
            string incomingInfo;
            byte[] buffer = new byte[1024];
            NetworkStream stream = this.my_client.GetStream();
            stream.Read(buffer, 0, buffer.Length);
            stream.Flush();
            incomingInfo = Encoding.ASCII.GetString(buffer, 0, buffer.Length);
               
                //stream.Close();
            

            // mut.ReleaseMutex();
            return incomingInfo.Substring(0,incomingInfo.IndexOf('\n')+1);
        }
        public void disconnect()
        {
            if (isConnected)
            {
                Console.WriteLine("disconnect");
                this.my_client.Close();
            }
        }

        public string WriteCommand(string command)
        {
            Console.WriteLine(command);
            string[] commands = command.Split('\n');
            //Console.WriteLine(commands.ToString());
            string output = "";
            foreach (var word in commands)
            {
                
                Console.WriteLine(word);
                mut.WaitOne();
                write(word);
                output += read();
                Console.WriteLine(output);
                mut.ReleaseMutex();
            }
            Console.WriteLine("reach end of for scope");
            return output;
        }
    }
}
