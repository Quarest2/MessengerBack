using System.Net;
using System.Net.Sockets;

namespace serverChat;
class Program
{
    
    private static Thread serverThread;

    static void Main(string[] args)
    {
        serverThread = new Thread(startServer);
        serverThread.IsBackground = true;
        serverThread.Start();
        while (true)
        {
            handlerCommands(Console.ReadLine());
        }
    }

    private static void handlerCommands(string cmd)
    {
        cmd = cmd.ToLower();
        if (cmd.Contains("/getusers"))
        {
            int countUsers = Server.Clients.Count;
            for (int i = 0; i < countUsers; i++)
            {
                Console.WriteLine("[{0}]: {1}",i, Server.Clients[i].UserName);
            }
        }
    }
    
    private static void startServer()
    {
        byte[] adress = { 127, 0, 0, 1 };
        IPAddress ipAdress = new IPAddress(adress);
        IPEndPoint ipEndPoint = new IPEndPoint(ipAdress,9933);
        Socket socket = new Socket(ipAdress.AddressFamily,SocketType.Stream,ProtocolType.Tcp);
        socket.Bind(ipEndPoint);
        socket.Listen(1000);
        Console.WriteLine("Server has been started on IP: {0}", ipEndPoint);
        while (true)
        {
            try
            {
                Socket user = socket.Accept();
                Server.NewClient(user);
            }
            catch (Exception exp)
            {
                Console.WriteLine("Error witn StartServer: {0}",exp.Message);
            }
        }
    }
}