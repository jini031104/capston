using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class Server : MonoBehaviour{
    public int[] ClientPCoin => clientPCoin;
    int[] clientPCoin = new int[7] { 0, 0, 0, 0, 0, 0, 0 };

    Socket serverSocket = null;
    ArrayList Connections = new ArrayList();

    //Ŭ���̾�Ʈ�κ��� ������Ŷ Ŭ������ ��� ���´�.
    ArrayList Buffer = new ArrayList();
    ArrayList ByteBuffers = new ArrayList();

    public const int PortNumb = 8731; //12345;

    private void Start(){
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

    private void OnApplicationQuit(){
        //�ý��� ����Ǹ� ������ Ŭ���̾�Ʈ�� �����ϰڴ�.
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
            //Ŭ���̾�Ʈ ������ ����
            this.Connections.Add(newConnection);
            //
            this.ByteBuffers.Add(new ArrayList());
            Debug.Log("New Client Connected");
            Debug.Log("Connections.Count: " + Connections.Count);
            Debug.Log("listenList.Count: " + listenList.Count);

        }

        //������ ����� Ŭ���̾�Ʈ���� �ϳ��� �ִٸ�
        if (Connections.Count != 0){
            ArrayList cloneConnections = new ArrayList(this.Connections);
            //�ϳ� �̻��� ���� ���¸� Ȯ��
            //���� ��û�� ���Ե� ���ϸ� Select�� ��ȯ�� �� cloneConnections�� �����ϴ�.
            Socket.Select(cloneConnections, null, null, 1000);
            foreach (Socket client in cloneConnections){
                byte[] receivedBytes = new byte[512];
                byte[] buff = new byte[512];
                ArrayList buffer = (ArrayList)this.ByteBuffers[cloneConnections.IndexOf(client)];

                int n = client.Receive(buff);
                string data = Encoding.UTF8.GetString(buff, 0, n);
                Debug.Log("Server: " + data);
                clientPCoin[0] = (data[0] - '0');
                clientPCoin[1] = (data[1] - '0');
                clientPCoin[2] = (data[2] - '0');
                clientPCoin[3] = (data[3] - '0');
                clientPCoin[4] = (data[4] - '0');
                clientPCoin[5] = (data[5] - '0');
                clientPCoin[6] = (data[6] - '0');

                //Ŭ���̾�Ʈ�κ��� ���۵� ������ �ޱ�
                //int read = client.Receive(receivedBytes);
                //for (int i = 0; i < read; i++){
                //    buffer.Add(receivedBytes[i]);
                //}
                //
                //while (buffer.Count > 0){
                //    //��Ŷ�� ù��°�� ������ ��ü �������� ũ���� �װ� ������.
                //    int packetDataLength = (byte)buffer[0];
                //    if (packetDataLength < buffer.Count){
                //        ArrayList thisPacketBytes = new ArrayList(buffer);
                //        //������ �޺κ� �߶󳻱�
                //        thisPacketBytes.RemoveRange(packetDataLength, thisPacketBytes.Count - (packetDataLength + 1));
                //        //������ ���� ù�κ� �߶󳻱�
                //        thisPacketBytes.RemoveRange(0, 1);
                //        buffer.RemoveRange(0, packetDataLength + 1);
                //
                //        byte[] readBytes = (byte[])thisPacketBytes.ToArray(typeof(byte));
                //
                //        //SimplePacket readpacket = SimplePacket.FromByteArray(readBytes);
                //        //this.Buffer.Add(readpacket);
                //        //
                //        //Debug.LogWarning("Packet Receive From Client IP :[" + client.RemoteEndPoint.ToString() + "]"
                //        //    + readpacket.mouseX.ToString() + " / " + readpacket.mouseY.ToString());
                //    }
                //    else{
                //        break;
                //    }
                //}
            }
        }
    }
}
