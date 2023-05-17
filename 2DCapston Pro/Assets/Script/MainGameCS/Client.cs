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
        //Ŭ���̾�Ʈ���� ����� ���� �غ�
        this.clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        //Ŭ���̾�Ʈ�� ���ε��� �ʿ� ����

        //������ ������ �������(������)
        IPAddress serverIPAdress = IPAddress.Parse(this.serverIp);
        IPEndPoint serverEndPoint = new IPEndPoint(serverIPAdress, Server.PortNumb);

        //������ ���� ��û
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
    //    prefSize[0] = (byte)sendData.Length;    //������ ���� �պκп� �� ������ ���̿� ���� ������ �ִµ� �̰��� 
    //    Client.Instance.clientSocket.Send(prefSize);    //���� ������.
    //    Client.Instance.clientSocket.Send(sendData);
    //
    //    Debug.Log("Send Packet from Client :" + packet.mouseX.ToString() + "/" + packet.mouseX.ToString());
    //
    //}
    
    // Update is called once per frame
    void Update(){
        pCoin = GameObject.Find("startButton").GetComponent<Calculate>().PCoin;
        //Debug.Log("�÷��̾� ����1:" + pCoin[0] + " ����2:" + pCoin[1] + " ����3:" + pCoin[2] + " ����4:" + pCoin[3] + " ����5:" + pCoin[4] + " ����6:" + pCoin[5] + " ����-:" + pCoin[6]);
        //string s = String.Join(", ", pCoin);
        //Debug.Log(s);
        var builder = new StringBuilder();
        Array.ForEach(pCoin, x =>  builder.Append(x));
        string s = builder.ToString();
        //Debug.Log(s);

        //���콺 ���� Ŭ���� ������ ��Ŷ Ŭ������ �̿��ؼ� ��ġ������ ������ ����.
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
