using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMakeCoin : MonoBehaviour
{
    [SerializeField]
    GameObject[] enemyCoin;

    int enemyDiceNumVall;
    int diceNum, randomCoin, makeNum;

    public int[] ECoin => eCoin;
    int[] eCoin = new int[7] { 0, 0, 0, 0, 0, 0, 0 };

    bool playerTurn, attack, deleteCoinNum;

    // Start is called before the first frame update
    void Start(){
        makeNum = 0;
        CoinRotation();
    }

    // Update is called once per frame
    void Update(){
        attack = GameObject.Find("delete_Coin").GetComponent<delete_Coin>().Attack;
        playerTurn = GameObject.Find("changeButton").GetComponent<ChangeButton>().PlayerTurn;
        deleteCoinNum = GameObject.Find("startButton").GetComponent<Calculate>().DeleteCoinNum;
        enemyDiceNumVall = GameObject.Find("dice").GetComponent<DiceRot>().EnemyDiceNumVall;
        diceNum = enemyDiceNumVall + 1;

        if (deleteCoinNum){
            for (int i = 0; i < 7; i++)
                eCoin[i] = 0;
        }

        //AutoGame();
    }

    void AutoGame() {
        if (!playerTurn)
            if (Input.GetMouseButtonDown(1)){
                for (int i = 0; i < diceNum; i++){
                    if (attack){
                        if (eCoin[6] == 1)
                            randomCoin = Random.Range(0, 6);
                        else{
                            if (diceNum == 1)
                                randomCoin = Random.Range(0, 6);
                            else
                                randomCoin = Random.Range(0, 7);
                        }
                    }
                    else
                        randomCoin = Random.Range(0, 6);

                    switch (randomCoin){
                        case 0:
                            eCoin[0]++;
                            break;
                        case 1:
                            eCoin[1]++;
                            break;
                        case 2:
                            eCoin[2]++;
                            break;
                        case 3:
                            eCoin[3]++;
                            break;
                        case 4:
                            eCoin[4]++;
                            break;
                        case 5:
                            eCoin[5]++;
                            break;
                        case 6:
                            eCoin[6]++;
                            break;
                    }

                    Instantiate(enemyCoin[randomCoin], new Vector3(i - 3, 2, 0), Quaternion.identity);
                    makeNum++;
                }
            }
    }

    void CoinRotation(){
        for(int i=0; i<7; i++)
            enemyCoin[i].transform.GetChild(1).GetComponent<SpriteRenderer>().sortingOrder = 2;
    }
}
