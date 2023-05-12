using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DicePiusOne : MonoBehaviour{
    public int PlayerDiceNumVall => playerDiceNumVall;
    public int EnemyDiceNumVall => enemyDiceNumVall;
    int playerDiceNumVall, enemyDiceNumVall, plusNum, count;

    public bool Click => click;
    bool playerTurn, click;

    // Start is called before the first frame update
    void Start(){
        click = false;
        count = 0;
    }

    // Update is called once per frame
    void Update(){
        plusNum = 0;
    }

    void OnMouseDown(){
        playerTurn = GameObject.Find("changeButton").GetComponent<ChangeButton>().PlayerTurn;
        plusNum = 1;

        if (count == 0){
            click = true;
            if (playerTurn){
                playerDiceNumVall = GameObject.Find("dice").GetComponent<DiceRot>().PlayerDiceNumVall;
                if (playerDiceNumVall < 5)  // 주사위 값이 6보다 작은 경우에만 가능하다.
                    playerDiceNumVall += plusNum;
                else if (playerDiceNumVall == 5)
                    Debug.Log("더이상 추가할 수 없습니다.");
                Debug.Log("playerDiceAfter: " + playerDiceNumVall);
            }
            else{
                enemyDiceNumVall = GameObject.Find("dice").GetComponent<DiceRot>().EnemyDiceNumVall;
                if (enemyDiceNumVall < 5)
                    enemyDiceNumVall += plusNum;
                else if (enemyDiceNumVall == 5)
                    Debug.Log("더이상 추가할 수 없습니다.");
                Debug.Log("enemyDiceAfter: " + enemyDiceNumVall);
            }
            count++;
        }
    }

    void OnMouseUp(){
        click = false;
    }
}
