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

            //UdpClients
            UdpClient udpMasterServer = new UdpClient(7899);
            
            UdpClient udpSlaveClient = new UdpClient("192.168.24.186",7999);

            //IP's
            IPAddress slaveIp = IPAddress.Parse("192.168.24.186");
            IPAddress ownIp = IPAddress.Parse("192.168.24.178");

            //EndPoints
            IPEndPoint IpEndPoint = new IPEndPoint(slaveIp, 7899);
            

            try
            {
                Console.WriteLine("Server is waiting...");

                while (true)
                {
                    //Sending Data
                    string test = "yes!";

                    Console.WriteLine("Ready to send...");
                    Byte[] sendData = Encoding.ASCII.GetBytes("Data is: " + test);
                    Console.WriteLine("Data is: " + test);
                    udpSlaveClient.Send(sendData, sendData.Length);
                    Console.WriteLine("Data is sent!");

                    Thread.Sleep(2000);

                    //Receiving Data
                    Console.WriteLine("Ready to receive...");
                    Byte[] receivedRawData = udpMasterServer.Receive(ref IpEndPoint);
                    string receivedStringData = Encoding.ASCII.GetString(receivedRawData);

                    Console.WriteLine("The data reads: " + receivedStringData);
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
