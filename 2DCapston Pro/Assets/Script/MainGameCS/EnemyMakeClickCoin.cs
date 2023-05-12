using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMakeClickCoin : MonoBehaviour
{
    [SerializeField]
    GameObject coinPrefab;

    int coinSpawn;
    public int[] ECoin => eCoin;
    int[] eCoin = new int[7] { 0, 0, 0, 0, 0, 0, 0 };

    bool playerTurn;
    bool coinMakeOk, enemyCoinMakeClear, deleteCoinNum;

    // Start is called before the first frame update
    void Start(){
    }

    // Update is called once per frame
    void Update(){
        coinSpawn = GameObject.Find("playerCoin").GetComponent<ClonCoinLimit>().EnemyCoinSpawn;
        coinMakeOk = GameObject.Find("dice").GetComponent<DiceRot>().CoinMakeOk;    // 주사위를 던지면 코인을 만들 수 있다.
        enemyCoinMakeClear = GameObject.Find("playerCoin").GetComponent<ClonCoinLimit>().EnemyCoinMakeClear;
        playerTurn = GameObject.Find("changeButton").GetComponent<ChangeButton>().PlayerTurn;
        deleteCoinNum = GameObject.Find("startButton").GetComponent<Calculate>().DeleteCoinNum;
        if (deleteCoinNum){
            for (int i = 0; i < 7; i++)
                eCoin[i] = 0;
        }
    }

    void OnMouseDown(){
        if (!playerTurn && coinMakeOk && enemyCoinMakeClear){
            Instantiate(coinPrefab, new Vector3(-3 + coinSpawn, 2, 0), Quaternion.identity);
            switch (coinPrefab.name){
                case "eClon1":
                    eCoin[0]++;
                    break;
                case "eClon2":
                    eCoin[1]++;
                    break;
                case "eClon3":
                    eCoin[2]++;
                    break;
                case "eClon4":
                    eCoin[3]++;
                    break;
                case "eClon5":
                    eCoin[4]++;
                    break;
                case "eClon6":
                    eCoin[5]++;
                    break;
                case "eClon-":
                    eCoin[6]++;
                    break;
            }
        }
    }
}
