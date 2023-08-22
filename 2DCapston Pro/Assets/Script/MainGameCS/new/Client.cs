using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class Client : MonoBehaviour{
    int[] pCoin = new int[7] { 0, 0, 0, 0, 0, 0, 0 };

    //local IP
    public string serverIp = "127.0.0.1";
    Socket clientSocket = null;

    // Start is called before the first frame update
    void Start(){
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

    private void OnApplicationQuit()
    {
        if (this.clientSocket != null)
        {
            this.clientSocket.Close();
            this.clientSocket = null;
        }
    }

    //public static void Send(SimplePacket packet){
    //    if (Client.Instance.clientSocket == null){
    //        return;
    //    }
    //    byte[] sendData = SimplePacket.ToByteArray(packet);
    //    byte[] prefSize = new byte[1];
    //    prefSize[0] = (byte)sendData.Length;    //버퍼의 가장 앞부분에 이 버퍼의 길이에 대한 정보가 있는데 이것을 
    //    Client.Instance.clientSocket.Send(prefSize);    //먼저 보낸다.
    //    Client.Instance.clientSocket.Send(sendData);
    //
    //    Debug.Log("Send Packet from Client :" + packet.mouseX.ToString() + "/" + packet.mouseX.ToString());
    //
    //}
    
    // Update is called once per frame
    void Update(){
        pCoin = GameObject.Find("startButton").GetComponent<Calculate>().PCoin;
        //Debug.Log("플레이어 코인1:" + pCoin[0] + " 코인2:" + pCoin[1] + " 코인3:" + pCoin[2] + " 코인4:" + pCoin[3] + " 코인5:" + pCoin[4] + " 코인6:" + pCoin[5] + " 코인-:" + pCoin[6]);
        //string s = String.Join(", ", pCoin);
        //Debug.Log(s);
        var builder = new StringBuilder();
        Array.ForEach(pCoin, x =>  builder.Append(x));
        string s = builder.ToString();
        //Debug.Log(s);

        //마우스 왼쪽 클리할 때마다 패킷 클래스를 이용해서 위치정보를 서버에 전송.
        if (Input.GetMouseButtonDown(0) == true){
            //string aaa = "abcd";
            //Debug.Log(aaa);
            //byte[] buff = Encoding.UTF8.GetBytes(aaa);
            byte[] buff = Encoding.UTF8.GetBytes(s);
            clientSocket.Send(buff, SocketFlags.None);
            //SimplePacket newPacket = new SimplePacket();
            //newPacket.mouseX = Input.mousePosition.x;
            //newPacket.mouseY = Input.mousePosition.y;
            //Client.Send(newPacket);
        }
    }
}
