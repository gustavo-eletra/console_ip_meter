using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;

public class SocketServer
{
    TcpClient clientSocket;
    string server;
    int port;

    public void Connect()
    {
        clientSocket.Connect(server, port);
    }

    public void SendString(string message)
    {
        byte[] data = System.Text.Encoding.ASCII.GetBytes(message);
        NetworkStream stream = this.clientSocket.GetStream();
        stream.Write(data, 0, data.Length);
        Console.WriteLine("Sent: {0}", message, port);
    }

    public void Close()
    {
        clientSocket.Close();
    }

    public string Receive()
    {
        NetworkStream stream = this.clientSocket.GetStream();
        byte[] data = new byte[512];
        Int32 r_bytes = stream.Read(data, 0, data.Length);
        string s = System.Text.Encoding.ASCII.GetString(data, 0, r_bytes);
        return s;
    }

    public SocketServer(string server, int port)
    {
        this.clientSocket = new TcpClient(server, port);
        this.port = port;
        this.server = server;
    }
}

public class ESPComms
{
    public static void Main(string[] args)
    {
        SocketServer server = new SocketServer("q", 8888);
        server.Connect();
        string? message = "";
        while(true)
        {
            message = Console.ReadLine();
            server.SendString(message);
            Console.Write(server.Receive());
        }
    }
}