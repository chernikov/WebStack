using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebStackServerApp
{
    public interface IHttpServer
    {
        void Start();
    }

    public class HttpServer : IHttpServer
    {
        private readonly TcpListener listener;

        public HttpServer(int port)
        {
            this.listener = new TcpListener(IPAddress.Parse("127.0.0.1"), port);
        }

        public void Start()
        {
            this.listener.Start();
            while (true)
            {
                var client = this.listener.AcceptTcpClient();
                var buffer = new byte[10240];
                var stream = client.GetStream();
                var length = stream.Read(buffer, 0, buffer.Length);
                var incomingMessage = Encoding.UTF8.GetString(buffer, 0, length);
                var result = "<h1>Hello, world!</h1>";
                stream.Write(
                    Encoding.UTF8.GetBytes(
                        "HTTP/1.0 200 OK" + Environment.NewLine
                        + "Content-Length: " + result.Length + Environment.NewLine
                        + "Content-Type: " + "text/plain" + Environment.NewLine
                        + Environment.NewLine
                        + result
                        + Environment.NewLine + Environment.NewLine));
                Console.WriteLine("Incoming message: {0}", incomingMessage);
            }
        }
    }
}
