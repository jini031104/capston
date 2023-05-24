using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine.SceneManagement;

public class TestServer : MonoBehaviour{
    public static TestServer Instance;
    Socket serverSocket = null;
    //Socket clientSock;

    ArrayList Connections = new ArrayList();

    //Ŭ���̾�Ʈ�κ��� ������Ŷ Ŭ������ ��� ���´�.
    ArrayList Buffer = new ArrayList();
    ArrayList ByteBuffers = new ArrayList();

    public const int PortNumb = 12345;

    private void Start(){
        if (SceneManager.GetActiveScene().name == "GameStart")
            Debug.Log("aaaaaaaaaa");
        DontDestroyOnLoad(gameObject);
        Debug.Log("Server Start");
        this.serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        IPEndPoint ipLocal = new IPEndPoint(IPAddress.Any, PortNumb);   //IPAddress.Any : ��� ��Ʈ��ũ�κ��� ���� �غ��ϰڴ�.
        //���ε�
        this.serverSocket.Bind(ipLocal);    //Ŭ���̾�Ʈ�κ��� ���� ������ ������ ��������Ʈ�� ���� �ϰڴ�.
        //������
        Debug.Log("Start Listening..");
        this.serverSocket.Listen(100);  // backlog : Ŭ���̾�Ʈ�� �ִ� ��
    }

    void SocketClose(){
        //�����ݱ�
        if (this.serverSocket != null){
            this.serverSocket.Close();
        }
        this.serverSocket = null;

        //Ŭ���̾�Ʈ ����
        foreach (Socket client in this.Connections){
            client.Close();
        }
        this.Connections.Clear();
    }

    private void OnApplicationQuit(){   //�ý��� ����Ǹ� ������ Ŭ���̾�Ʈ�� �����ϰڴ�.
        SocketClose();
    }

    private void Update(){
        ArrayList listenList = new ArrayList();
        listenList.Add(this.serverSocket);
        
        Socket.Select(listenList, null, null, 1000);
        
        //<�����û>
        //���� �����û�� �ִٸ� ��������Ʈ�� 0�� �ƴϴ�.
        for (int i = 0; i < listenList.Count; i++){
            //Accept
            Socket newConnection = ((Socket)listenList[i]).Accept();
            //clientSock = ((Socket)listenList[i]).Accept();
            //Ŭ���̾�Ʈ ������ ����
            this.Connections.Add(newConnection);
            //this.Connections.Add(clientSock);
            //
            this.ByteBuffers.Add(new ArrayList());
        }

        //if (Connections.Count != 0){
        //    byte[] buff = new byte[512];
        //    int n = clientSock.Receive(buff);
        //    string data = Encoding.UTF8.GetString(buff, 0, n);
        //    Debug.Log("Server: " + data);
        //}

        //������ ����� Ŭ���̾�Ʈ���� �ϳ��� �ִٸ�
        if (Connections.Count != 0){
            ArrayList cloneConnections = new ArrayList(this.Connections);
            //�ϳ� �̻��� ���� ���¸� Ȯ��
            //���� ��û�� ���Ե� ���ϸ� Select�� ��ȯ�� �� cloneConnections�� �����ϴ�.
            Socket.Select(cloneConnections, null, null, 1000);
            foreach (Socket client in cloneConnections){
                //byte[] receivedBytes = new byte[512];
                byte[] buff = new byte[512];
                ArrayList buffer = (ArrayList)this.ByteBuffers[cloneConnections.IndexOf(client)];
        
                int n = client.Receive(buff);
                string data = Encoding.UTF8.GetString(buff, 0, n);
                Debug.Log("Server: " + data);

                client.Send(buff, 0, n, SocketFlags.None);
            }
        }
    }
}
