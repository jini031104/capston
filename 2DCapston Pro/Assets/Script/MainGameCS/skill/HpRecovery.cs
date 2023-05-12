using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpRecovery : MonoBehaviour{
    public int PlayerHP => playerHP;
    public int EnemyHP => enemyHP;
    int playerHP, enemyHP, playerCount, enemyCount;

    public bool CardClick => cardClick;
    bool playerTurn, cardClick;

    // Start is called before the first frame update
    void Start(){
        cardClick = false;
        playerCount = 0;
        enemyCount = 0;
    }

    // Update is called once per frame
    void Update(){
        playerTurn = GameObject.Find("changeButton").GetComponent<ChangeButton>().PlayerTurn;
        if(playerTurn)
            enemyCount = 0;
        else
            playerCount = 0;
    }

    void OnMouseDown(){
        cardClick = true;
        if (playerTurn){
            playerHP = GameObject.Find("startButton").GetComponent<Calculate>().PlayerHP;
            // HP가 30 미만일 때만 회복 가능.
            if (playerHP < 30 && playerCount == 0)
                playerHP += 2;
            if (playerHP > 30)
                playerHP = 30;
            playerCount++;
        }
        else{
            enemyHP = GameObject.Find("startButton").GetComponent<Calculate>().EnemyHP;
            if (enemyHP < 30 && enemyCount == 0)
                enemyHP += 2;
            if (enemyHP > 30)
                enemyHP = 30;
            enemyCount++;
        }
    }

    void OnMouseUp(){
        cardClick = false;
    }
}
