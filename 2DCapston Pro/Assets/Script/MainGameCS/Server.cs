using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;

public class Server : MonoBehaviour{
    Socket serverSocket = null;
    ArrayList Connections = new ArrayList();

    //Ŭ���̾�Ʈ�κ��� ������Ŷ Ŭ������ ��� ���´�.
    ArrayList Buffer = new ArrayList();
    ArrayList ByteBuffers = new ArrayList();

    public const int PortNumb = 12345;

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
        }

        //������ ����� Ŭ���̾�Ʈ���� �ϳ��� �ִٸ�
        //if (Connections.Count != 0){
        //    ArrayList cloneConnections = new ArrayList(this.Connections);
        //    Socket.Select(cloneConnections, null, null, 1000);
        //    foreach (Socket client in cloneConnections){
        //        byte[] receivedBytes = new byte[512];
        //        ArrayList buffer = (ArrayList)this.ByteBuffers[cloneConnections.IndexOf(client)];
        //
        //        //Ŭ���̾�Ʈ�κ��� ���۵� ������ ���
        //        int read = client.Receive(receivedBytes);
        //        for (int i = 0; i < read; i++){
        //            buffer.Add(receivedBytes[i]);
        //        }
        //
        //        while (buffer.Count > 0){
        //            //��Ŷ�� ù��°�� ������ ��ü �������� ũ���� �װ� ������.
        //            int packetDataLength = (byte)buffer[0];
        //            if (packetDataLength < buffer.Count){
        //                ArrayList thisPacketBytes = new ArrayList(buffer);
        //                //������ �޺κ� �߶󳻱�
        //                thisPacketBytes.RemoveRange(packetDataLength, thisPacketBytes.Count - (packetDataLength + 1));
        //                //������ ���� ù�κ� �߶󳻱�
        //                thisPacketBytes.RemoveRange(0, 1);
        //                buffer.RemoveRange(0, packetDataLength + 1);
        //
        //                byte[] readBytes = (byte[])thisPacketBytes.ToArray(typeof(byte));
        //
        //                SimplePacket readpacket = SimplePacket.FromByteArray(readBytes);
        //                this.Buffer.Add(readpacket);
        //
        //                Debug.LogWarning("Packet Receive From Client IP :[" + client.RemoteEndPoint.ToString() + "]"
        //                    + readpacket.mouseX.ToString() + " / " + readpacket.mouseY.ToString());
        //            }
        //            else{
        //                break;
        //            }
        //        }
        //    }
        //}
    }
}
