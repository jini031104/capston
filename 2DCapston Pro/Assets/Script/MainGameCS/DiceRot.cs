using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRot : MonoBehaviour{
    Vector3 diceScale;

    int[] diceVall = new int[] { 1, 2, 3, 4, 5, 6 };
    int[,] rotationVal = new int[,] { { 40, 135, 270},
                                        {40, 45, 90 },
                                        {130, 45, 0 }};
    int rVall, dVall;
    int cleanPlayerDiceNum, cleanEnemyDiceNum;

    public int PlayerDiceNumVall => playerDiceNumVall;
    public int EnemyDiceNumVall => enemyDiceNumVall;
    int playerDiceNumVall, enemyDiceNumVall;
    int diceRotIndex, diceNumVall;

    bool diceSmall = false;

    public bool CoinMakeOk => coinMakeOk;
    public bool EnemyCoinMakeOk => enemyCoinMakeOk;
    bool coinMakeOk, enemyCoinMakeOk;

    bool playerTurn, diceStart;
    bool diceChang, reOK;
    bool coinDeletes;

    int attackResult, coinDelete;

    string[] deleteCoinName = new string[] { "clonCoin1(Clone)", "clonCoin2(Clone)", "clonCoin3(Clone)", "clonCoin4(Clone)", "clonCoin5(Clone)", "clonCoin6(Clone)", "clonCoin-(Clone)" };
    string[] deleteEnemyCoinName = new string[] { "eClon1(Clone)", "eClon2(Clone)", "eClon3(Clone)", "eClon4(Clone)", "eClon5(Clone)", "eClon6(Clone)", "eClon-(Clone)" };

    // Start is called before the first frame update
    void Start(){
        diceScale = this.gameObject.transform.localScale;
        dVall = diceVall.GetLength(0);
        rVall = rotationVal.GetLength(0);
        attackResult = GameObject.Find("changeButton").GetComponent<ChangeButton>().AttackResult;

        if (attackResult == 0)
            Debug.Log("Player!");
        else if (attackResult == 1)
            Debug.Log("Enemy!");

        coinDeletes = false;
        coinMakeOk = false;
        enemyCoinMakeOk = false;
        diceStart = true;
        coinDelete = 0;
    }

    // Update is called once per frame
    void Update(){
        playerTurn = GameObject.Find("changeButton").GetComponent<ChangeButton>().PlayerTurn;

        SmallDice();

        if(coinDeletes){
            for(int i=0; i<7; i++){
                Destroy(GameObject.Find(deleteCoinName[i]));
                Destroy(GameObject.Find(deleteEnemyCoinName[i]));
            }
        }
    }

    void OnMouseDown(){
        if (!diceStart)
            Debug.Log("더이상 주사위를 굴릴 수 없습니다.");

        if (diceStart){
            coinDelete++;
            if (coinDelete % 2 == 1 && 1 < coinDelete){
                coinDeletes = true;
            }

            transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            coinMakeOk = true;
            if (!playerTurn)
                enemyCoinMakeOk = true;
        }
    }

    void OnMouseUp(){
        if (diceStart){
            DiceDrpoAndValSet();
            diceStart = false;
            coinDeletes = false;
        }
    }

    void DiceDrpoAndValSet(){    // 회전과 값을 결정함.
        diceRotIndex = Random.Range(0, rVall);
        transform.localEulerAngles = new Vector3(rotationVal[diceRotIndex, 0], rotationVal[diceRotIndex, 1], rotationVal[diceRotIndex, 2]);

        diceSmall = true;
        if (playerTurn){
            playerDiceNumVall = Random.Range(0, dVall);
            diceNumVall = playerDiceNumVall;
        }
        else{
            enemyDiceNumVall = Random.Range(0, dVall);
            diceNumVall = enemyDiceNumVall;
        }
    }

    void SmallDice(){
        if (diceSmall){
            if(transform.localScale.x > diceScale.x){
                transform.localScale -= new Vector3(0.01f, 0.01f, 0.01f);
            }
            else{
                DiceResult(diceNumVall);
                diceSmall = false;
            }
        }
    }

    void DiceResult(int valIndex){      // 주사위 값에 따라 눈을 보여줌
        switch (valIndex){
            case 0:
                transform.localEulerAngles = new Vector3(0, 180, 0);
                break;
            case 1:
                transform.localEulerAngles = new Vector3(0, 90, -90);
                break;
            case 2:
                transform.localEulerAngles = new Vector3(0, 270, 0);
                break;
            case 3:
                transform.localEulerAngles = new Vector3(0, 90, 0);
                break;
            case 4:
                transform.localEulerAngles = new Vector3(0, 90, -270);
                break;
            case 5:
                transform.localEulerAngles = new Vector3(0, 0, 0);
                break;
        }
    }
}
