using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceMinusOne : MonoBehaviour{
    public int PlayerDiceNumVall => playerDiceNumVall;
    public int EnemyDiceNumVall => enemyDiceNumVall;
    int playerDiceNumVall, enemyDiceNumVall, minusNum, count;

    public bool Click => click;
    bool playerTurn, click;

    // Start is called before the first frame update
    void Start(){
        count = 0;
        click = false;
    }

    // Update is called once per frame
    void Update(){
        minusNum = 0;
    }

    void OnMouseDown(){
        playerTurn = GameObject.Find("changeButton").GetComponent<ChangeButton>().PlayerTurn;
        minusNum = -1;

        if (count == 0){
            click = true;
            if (playerTurn){
                playerDiceNumVall = GameObject.Find("dice").GetComponent<DiceRot>().PlayerDiceNumVall;
                if (playerDiceNumVall >= 1)     // 주사위 값이 1보다 큰 경우에만 가능하다.
                    playerDiceNumVall += minusNum;
                else if (playerDiceNumVall == 0)
                    Debug.Log("더이상 뺄 수 없습니다.");
                Debug.Log("playerDice" + playerDiceNumVall);
            }
            else{
                enemyDiceNumVall = GameObject.Find("dice").GetComponent<DiceRot>().EnemyDiceNumVall;
                if (enemyDiceNumVall >= 1)
                    enemyDiceNumVall += minusNum;
                else if (enemyDiceNumVall == 0)
                    Debug.Log("더이상 뺄 수 없습니다.");
                Debug.Log("enemyDice" + enemyDiceNumVall);
            }
            count++;
        }
    }

    void OnMouseUp(){
        click = false;
    }
}
