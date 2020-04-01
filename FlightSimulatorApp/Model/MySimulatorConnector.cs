﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace FlightSimulatorApp.Model
{
    public class MySimulatorConnector : ISimulatorConnector
    {
        TcpClient my_client;
        private NetworkStream write_stream = null;
        private NetworkStream read_stream = null;
        public void connect(string ip, int port) { 
            try
            {
                Console.WriteLine("connecting to ip: {0}, port: {1}", ip, port.ToString());
                my_client = new TcpClient(ip, port);
                write_stream = my_client.GetStream();
                write_stream.Flush();
                read_stream = my_client.GetStream();
                read_stream.Flush();
                //isConnected = true;
                Console.WriteLine("Connected!");
            } catch (Exception ex)
            {
                Console.WriteLine("EROR - failed to connect please try again");
                Console.WriteLine(ex.Message);

            }
        }
        public void write(string command)
        {
            byte[] buffer = Encoding.ASCII.GetBytes(command+"\r\n");
            this.write_stream.Write(buffer, 0, buffer.Length);
        }
        public string read()
        {
            string incomingInfo;
            byte[] buffer = new byte[1024];
            try
            {
                this.read_stream.Read(buffer, 0, 1024);
                incomingInfo = Encoding.ASCII.GetString(buffer, 0, buffer.Length);
                
            }
            catch (Exception ex)
            {
                incomingInfo = "eror reading from server";

            }
            return incomingInfo;
            
        }
        public void disconnect()
        {
            Console.WriteLine("disconnect");
            this.write_stream.Close();
            this.read_stream.Close();
        }
    }
}
