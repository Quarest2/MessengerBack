using System;
using System.Net.Sockets;

namespace serverChat
{
	public class Server
	{
		public static List<Client> Clients = new List<Client>();

		public static void NewClient(Socket handle)
		{
			try
			{
				Client newClient = new Client(handle);
				Clients.Add(newClient);
				Console.WriteLine("New client connected: {0}", handle.RemoteEndPoint);
			}
			catch (Exception exp)
			{
				Console.WriteLine("Error with addNewClient: {0}.",exp.Message);
			}
		}

		public static void EndClient(Client client)
		{
			try
			{
				Clients.Remove(client);
				Console.WriteLine("User {0} has been disconected.", client.UserName);
			}
			catch (Exception exp)
			{
				Console.WriteLine("Error with endClient: {0}.", exp.Message);
			}
		}

		public static void UpdateAllChats()
		{
			try
			{
				int countUsers = Clients.Count;
				for (int i = 0; i < countUsers; i++)
				{
					Clients[i].UpdateChat();
				}
			}
			catch (Exception exp)
			{
				Console.WriteLine("Error with UpdateAllChats: {0}",exp.Message);
			}
		}
	}
}

