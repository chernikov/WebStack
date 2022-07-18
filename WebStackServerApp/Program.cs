//IHttpServer server = new HttpServer(9876);
//server.Start();

TcpListener server = null;
try
{
    // Set the TcpListener on port 13000.
    Int32 port = 13000;
    IPAddress localAddr = IPAddress.Parse("127.0.0.1");

    // TcpListener server = new TcpListener(port);
    server = new TcpListener(localAddr, port);

    // Start listening for client requests.
    server.Start();

    // Buffer for reading data
    Byte[] income = new Byte[10256];
    String data = null;

    // Enter the listening loop.
    while (true)
    {
        Console.Write("Waiting for a connection... ");

        // Perform a blocking call to accept requests.
        // You could also use server.AcceptSocket() here.
        TcpClient client = server.AcceptTcpClient();
        Console.WriteLine("Connected!");

        data = null;

        // Get a stream object for reading and writing
        NetworkStream stream = client.GetStream();
        var length = stream.Read(income, 0, income.Length);
        // Translate data bytes to a ASCII string.
        data = System.Text.Encoding.ASCII.GetString(income, 0, length);
        Console.WriteLine("Received: {0}", data);

        // Process the data sent by the client.

        var messageTemplate = @"HTTP/1.0 200 OK
Content-Length: {0}
Content-Type: text/html
Connection: Closed";

        var body = @"
<html>
<body>
<h1>Hello, World!</h1>
</body>
</html>";

        var message = string.Format(messageTemplate, body.Length);
        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(message);
        // Send back a response.
        stream.Write(bytes, 0, bytes.Length);
        Console.WriteLine("Sent: {0}", message);
        byte[] bytesBody = System.Text.Encoding.UTF8.GetBytes(body);
        stream.Write(bytesBody, 0, bytesBody.Length);
        Console.WriteLine("Sent: {0}", body);

        Thread.Sleep(2000);
        // Shutdown and end connection
        client.Close();
    }
}
catch (SocketException e)
{
    Console.WriteLine("SocketException: {0}", e);
}
finally
{
    // Stop listening for new clients.
    server.Stop();
}

Console.WriteLine("\nHit enter to continue...");
Console.Read();


