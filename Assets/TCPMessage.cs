using System.Data.Common;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class TCPMessage : MonoBehaviour
{
    public int PORT = 7778;
    
    public string Message = "";

    void Start()
    {
        StartServer();
    }

    private void OnGUI()
    {
        if(GUI.Button(new Rect(100, 300, 200, 100), "Send message TCP"))
        {
            SendMessage();
        }
    }

    public async void StartServer()
    {
        var endPoint = new IPEndPoint(IPAddress.Any, PORT);
        var server = new TcpListener(endPoint);

        server.Start();

        while(Application.isPlaying)
        {
            var socketClient = await server.AcceptTcpClientAsync();
            ReceiveMessage(socketClient);
            Debug.Log("Client connected " + socketClient);
        }

        server.Stop();
    }

    async void ReceiveMessage(TcpClient client)
    {
        var stream = client.GetStream();
        var buffer = new byte[256];

        while(Application.isPlaying && client.Connected)
        {
            await stream.ReadAsync(buffer);
            var message = Encoding.UTF8.GetString(buffer);
            Debug.Log("Message : " + message);
            await stream.FlushAsync();
        }

        Debug.Log("Client disconnected");
        client.Close();
    }


    async void SendMessage()
    {
        TcpClient client = new TcpClient();

        await client.ConnectAsync(IPAddress.Parse("127.0.0.1"), PORT);

        if(!client.Connected)
        {
            Debug.Log("Failed to connect");
            return;
        }

        Debug.Log("Client Connected");

        var networkStream = client.GetStream();

        byte[] data = Encoding.UTF8.GetBytes(Message);

        networkStream.Write(data, 0, data.Length);

        Debug.Log("Message sended");
        client.Close();
    }
}
