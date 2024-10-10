using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class UDPTest : MonoBehaviour
{

    private void Start()
    {
        ReceiveMessage();
    }

    private void OnGUI()
    {
        if(GUI.Button(new Rect(100, 100, 200, 100), "Send Messages"))
        {
            SendMessage();
        }
    }

    public int PORT = 7777;
    public string message = "Hello World!";

    public void SendMessage()
    {
        var udpClient = new UdpClient();
        var endPoint = new IPEndPoint(IPAddress.Broadcast, PORT);

        byte[] data = Encoding.UTF8.GetBytes(message);

        udpClient.Send(data, data.Length, endPoint);
        udpClient.Close();

        Debug.Log("Message send");
    }

    public async void ReceiveMessage()
    {
        var endPoint = new IPEndPoint(IPAddress.Any, PORT);
        var udpClient = new UdpClient(endPoint);

        Debug.Log("Waiting");

        while(Application.isPlaying)
        {
            var result = await udpClient.ReceiveAsync();

            string message = Encoding.UTF8.GetString(result.Buffer);

            Debug.Log($"Received message " + message);
        }

        udpClient.Close();
    }
}
