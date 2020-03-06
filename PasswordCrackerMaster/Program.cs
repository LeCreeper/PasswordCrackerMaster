using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace PasswordCrackerMaster
{
    class Program
    {
        static void Main(string[] args)
        {
            #region UDPServer

            //This program is designed to be used with a master-slave architecture, with this computer being the host or master computer. 

            //UdpClients
            //This is the UdpClient that I use for receiving data!
            //NOTE: The port used should not also be used for sending data!
            UdpClient udpMasterServer = new UdpClient(7899);
            
            //This is the UdpClient that I use for sending data!
            //NOTE!: should be set to the receivers IP and a different port than the one you receive on!
            UdpClient udpSlaveClient = new UdpClient("192.168.24.186",7999);

            //IP's
            //The slaveIP is the ip of the computer you are SENDING data to!
            IPAddress slaveIp = IPAddress.Parse("192.168.24.186");

            //masterIp is the ip of this computer!
            IPAddress masterIp = IPAddress.Parse("192.168.24.178");

            //EndPoints
            //NOTE!: The endpoint consists of an IP and a PORT, where the IP is the ip of the person SENDING data to you.
            //And the port is DIFFERENT than the port this computer uses to SEND data. 
            IPEndPoint IpEndPoint = new IPEndPoint(slaveIp, 7899);
            

            try
            {
                Console.WriteLine("Server is waiting...");

                while (true)
                {
                    //Sending Data
                    string test = "yes!";

                    Console.WriteLine("Ready to send...\n");
                    Byte[] sendData = Encoding.ASCII.GetBytes("Data is: " + test);
                    
                    udpSlaveClient.Send(sendData, sendData.Length);
                    Console.WriteLine("Data sent is: " + sendData + "\n");

                    Thread.Sleep(2000);

                    //Receiving Data
                    Console.WriteLine("Ready to receive...\n");
                    Byte[] receivedRawData = udpMasterServer.Receive(ref IpEndPoint);
                    string receivedStringData = Encoding.ASCII.GetString(receivedRawData);

                    Console.WriteLine("The data reads: " + receivedStringData + "\n");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

            }

            #endregion


        }
    }
}
