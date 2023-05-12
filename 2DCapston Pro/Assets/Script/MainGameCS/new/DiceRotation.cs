using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRotation : MonoBehaviour
{
    Vector3 diceScale;

    bool diceSmall = false;
    public bool StartDice => startDice;
    bool startDice = false;
    bool makeCoinAfterDice;
    bool diceReplay;

    int[] diceVall = new int[] { 1, 2, 3, 4, 5, 6 };    // 주사위 값
    int[,] rotationVal = new int[,] { { 40, 135, 270 },
                                        {40, 45, 90 },
                                        {130, 45, 0 } };    // 떨어지는 동안의 주사위 회전
    int rVall, dVall;

    public int IndexVall => indexVall;
    int index, indexVall;

    public bool EnemyDice => enemyDice;
    bool enemyDice = false;
    bool playerDice = true;

    public bool DiceChange => diceChange;
    bool diceChange = false;

    // Start is called before the first frame update
    void Start()
    {
        diceScale = this.gameObject.transform.localScale;

        rVall = rotationVal.GetLength(0);
        dVall = diceVall.GetLength(0);
    }

    // Update is called once per frame
    void Update()
    {
        diceReplay = GameObject.Find("coin").GetComponent<makeCoinLimit>().DiceReplay;
        diceChange = GameObject.Find("coin").GetComponent<makeCoinLimit>().DiceChange;


        if (playerDice){
            Debug.Log("Player!");
        }
        else if(enemyDice){
            Debug.Log("Enemy!");
        }

        SmallDice();

        // If DiceNum and make CoinNum same, We have Dice roll replay check.
        makeCoinAfterDice = GameObject.Find("coin").GetComponent<makeCoinLimit>().StartDiceCheck;
        if (!makeCoinAfterDice){
            startDice = false;
            if (diceChange)
            {
                DicePlayerOrEnemyChange();  // 플레이어 <-> 적 주사위 교체
                diceChange = false;
            }
        }
    }

    public void OnMouseDown()  // Dice Up. 주사위가 커지기만 함.
    {
        if (!diceReplay){
            if(playerDice)
                BigDice();
            else if(enemyDice)
                BigDice();
        }
    }

    public void OnMouseUp() // Dice Rotation
    {
        if (!diceReplay)    // 주사위가 눌리지 않았을 때만 가능함.
            if(playerDice)
                DiceDrpoAndValSet();
            else if(enemyDice)
                DiceDrpoAndValSet();
            // 주사위 클릭하면 생성되었던 클론들 삭제시키기
    }

    // 주사위의 값을 결정 & 계속 작아짐
    void SmallDice(){
        if(diceSmall){
            if(transform.localScale.x > diceScale.x)
                transform.localScale -= new Vector3(0.01f, 0.01f, 0.01f);
            else{
                diceSmall = false;
                DiceResult(indexVall);  // 주사위 값을 보여줌.
            }
        }
    }

    void DiceDrpoAndValSet(){   // 회전과 값을 결정함.
        index = Random.Range(0, rVall);
        transform.localEulerAngles = new Vector3(rotationVal[index, 0], rotationVal[index, 1], rotationVal[index, 2]);
        indexVall = Random.Range(0, dVall); // 1~6 사이의 주사위 값 결정.
        diceSmall = true;
        startDice = true;
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

    void DicePlayerOrEnemyChange()
    {
        if (playerDice)
        {
            playerDice = false;
            enemyDice = true;
        }
        else if (enemyDice)
        {
            playerDice = true;
            enemyDice = false;
        }
    }

    void BigDice(){
        transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
    }
}
