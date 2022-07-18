

using System.Net.Sockets;

namespace WebStackServerApp;

public class Server
{
    TcpListener internalServer;
    public int Port { get; set; }

    public Server()
    {
        Port = 5000;
        internalServer = new TcpListener(Port);
    }


    public void Run()
    {
      
     
    }
}
