using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class testClient : MonoBehaviour{
    Socket socket = null;
    string cmd = "abcd";

    // Start is called before the first frame update
    void Start(){
        DontDestroyOnLoad(gameObject);
        this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        var ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 12345);

        try{
            Debug.Log("Connecting to Server");
            socket.Connect(ep);
        }
        catch (SocketException e){
            Debug.Log("Connection Failed:" + e.Message);
        }
    }

    // Update is called once per frame
    void Update(){
        if (Input.GetMouseButtonDown(0) == true){
            byte[] buff = Encoding.UTF8.GetBytes(cmd);
            socket.Send(buff, SocketFlags.None);
        }
    }
}
