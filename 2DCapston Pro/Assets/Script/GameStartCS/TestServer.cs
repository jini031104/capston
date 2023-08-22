using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class testServer : MonoBehaviour{
    Socket serverSocket = null;
    Socket clientSocket = null;
    ArrayList Connections = new ArrayList();

    public const int PortNumb = 12345;
    byte[] buff = new byte[512];

    public bool clientDiceClick;
    string cmd = "aaaaaaa";

    // Start is called before the first frame update
    void Start(){
        DontDestroyOnLoad(gameObject);
        Debug.Log("Server Start");
        this.serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        //바인딩
        IPEndPoint ep = new IPEndPoint(IPAddress.Any, PortNumb);
        serverSocket.Bind(ep);
        //리스닝
        serverSocket.Listen(100);
        clientDiceClick = false;
    }

    // Update is called once per frame
    void Update(){
        ArrayList listenList = new ArrayList();
        listenList.Add(serverSocket);
        Socket.Select(listenList, null, null, 1000);

        for (int i = 0; i < listenList.Count; i++){
            clientSocket = ((Socket)listenList[i]).Accept();
            this.Connections.Add(clientSocket);
            Debug.Log("New Client Connected");
        }

        if (Connections.Count != 0){
            ArrayList cloneConnections = new ArrayList(this.Connections);
            Socket.Select(cloneConnections, null, null, 1000);
        
            foreach (Socket client in cloneConnections){
                //int n = clientSocket.Receive(buff);
                int n = client.Receive(buff);
                string data = Encoding.UTF8.GetString(buff, 0, n);
                
                Debug.Log("data: " + data);

                byte[] buff2 = Encoding.UTF8.GetBytes(cmd);
                clientSocket.Send(buff2, 0, n, SocketFlags.None);

                clientDiceClick = true;
            }
        }
    }
}
