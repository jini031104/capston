using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class TestClient : MonoBehaviour{
    public static TestServer Instance;
    //local IP
    public string serverIp = "127.0.0.1";
    Socket clientSocket = null;

    // Start is called before the first frame update
    void Start(){
        DontDestroyOnLoad(gameObject);
        //클라이언트에서 사용할 소켓 준비
        this.clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        //클라이언트는 바인딩할 필요 없음

        //접속할 서버의 통신지점(목적지)
        IPAddress serverIPAdress = IPAddress.Parse(this.serverIp);
        IPEndPoint serverEndPoint = new IPEndPoint(serverIPAdress, Server.PortNumb);

        //서버로 연결 요청
        try{
            Debug.Log("Connecting to Server");
            this.clientSocket.Connect(serverEndPoint);
        }
        catch (SocketException e){
            Debug.Log("Connection Failed:" + e.Message);
        }
    }

    private void OnApplicationQuit(){
        if (this.clientSocket != null){
            this.clientSocket.Close();
            this.clientSocket = null;
        }
    }

    // Update is called once per frame
    void Update(){
        //마우스 왼쪽 클리할 때마다 패킷 클래스를 이용해서 위치정보를 서버에 전송.
        if (Input.GetMouseButtonDown(0) == true){
            string aaa = "abcd";
            Debug.Log(aaa);
            byte[] buff = Encoding.UTF8.GetBytes(aaa);
            clientSocket.Send(buff, SocketFlags.None);

            byte[] receivedBuff = new byte[512];
            int n = clientSocket.Receive(receivedBuff);

            string data = Encoding.UTF8.GetString(receivedBuff, 0, n);
            Debug.Log("Client: " + data);
        }
    }
}
