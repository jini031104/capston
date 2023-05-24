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

    //클라이언트로부터 받은패킷 클래스를 담아 놓는다.
    ArrayList Buffer = new ArrayList();
    ArrayList ByteBuffers = new ArrayList();

    public const int PortNumb = 12345;

    private void Start(){
        if (SceneManager.GetActiveScene().name == "GameStart")
            Debug.Log("aaaaaaaaaa");
        DontDestroyOnLoad(gameObject);
        Debug.Log("Server Start");
        this.serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        IPEndPoint ipLocal = new IPEndPoint(IPAddress.Any, PortNumb);   //IPAddress.Any : 모든 네트워크로부터 들을 준비하겠다.
        //바인딩
        this.serverSocket.Bind(ipLocal);    //클라이언트로부터 받은 소켓을 로컬의 엔드포인트로 연결 하겠다.
        //리스닝
        Debug.Log("Start Listening..");
        this.serverSocket.Listen(100);  // backlog : 클라이언트의 최대 수
    }

    void SocketClose(){
        //서버닫기
        if (this.serverSocket != null){
            this.serverSocket.Close();
        }
        this.serverSocket = null;

        //클라이언트 끊기
        foreach (Socket client in this.Connections){
            client.Close();
        }
        this.Connections.Clear();
    }

    private void OnApplicationQuit(){   //시스템 종료되면 서버도 클라이언트도 정릴하겠다.
        SocketClose();
    }

    private void Update(){
        ArrayList listenList = new ArrayList();
        listenList.Add(this.serverSocket);
        
        Socket.Select(listenList, null, null, 1000);
        
        //<연결요청>
        //받은 연결요청이 있다면 리슨리스트는 0이 아니다.
        for (int i = 0; i < listenList.Count; i++){
            //Accept
            Socket newConnection = ((Socket)listenList[i]).Accept();
            //clientSock = ((Socket)listenList[i]).Accept();
            //클라이언트 소켓을 저장
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

        //서버와 연결된 클라이언트들이 하나라도 있다면
        if (Connections.Count != 0){
            ArrayList cloneConnections = new ArrayList(this.Connections);
            //하나 이상의 소켓 상태를 확인
            //연결 요청이 포함된 소켓만 Select가 반환된 후 cloneConnections에 남습니다.
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
