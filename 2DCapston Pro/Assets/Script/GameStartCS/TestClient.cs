using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class testClient : MonoBehaviour{
    Socket clientSocket = null;
    //string cmd = "abcd";
    int playerDiceVal, i = 0;

    bool diceClick, serverAccep;

    // Start is called before the first frame update
    void Start(){
        DontDestroyOnLoad(gameObject);
        this.clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        var ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 12345);
        serverAccep = false;

        try{
            Debug.Log("Connecting to Server");
            clientSocket.Connect(ep);
            serverAccep = true;
        }
        catch (SocketException e){
            Debug.Log("Connection Failed:" + e.Message);
        }
        playerDiceVal = 0;
        diceClick = false;
    }

    // Update is called once per frame
    void Update(){
        playerDiceVal = GameStartDice.playerDiceVal;
        diceClick = GameObject.Find("playerDice").GetComponent<GameStartDice>().DiceClick;

        string str = playerDiceVal.ToString();

        if (serverAccep && i < 10){
            Debug.Log("aaaaa");
            byte[] buff = Encoding.UTF8.GetBytes(str);
            clientSocket.Send(buff, SocketFlags.None);

            byte[] receiveBuff = new byte[512];
            int n = clientSocket.Receive(receiveBuff);
            i++;

            //string data = Encoding.UTF8.GetString(receiveBuff, 0, n);
            //Debug.Log("data: " + data);
        }
    }

    void GameStartScece(){
    }
}
