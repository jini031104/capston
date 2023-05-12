using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeCoin : MonoBehaviour
{
    [SerializeField]
    GameObject coinPrefab;
    [SerializeField]
    Transform spawnPoint;

    public bool CoinMakeOk => coinMakeOk;
    bool coinMakeOk;
    public int[] PCoin => pCoin;
    int[] pCoin = new int[7] { 0, 0, 0, 0, 0, 0, 0 };

    bool coinMakeClear, deleteCoinNum;

    int coinSpawn;

    // Start is called before the first frame update
    void Start(){
    }

    // Update is called once per frame
    void Update(){
        coinSpawn = GameObject.Find("playerCoin").GetComponent<ClonCoinLimit>().CoinSpawn;
        coinMakeOk = GameObject.Find("dice").GetComponent<DiceRot>().CoinMakeOk;    // 주사위를 던지면 코인을 만들 수 있다.
        coinMakeClear = GameObject.Find("playerCoin").GetComponent<ClonCoinLimit>().CoinMakeClear;
        deleteCoinNum = GameObject.Find("startButton").GetComponent<Calculate>().DeleteCoinNum;
        if (deleteCoinNum){
            for (int i = 0; i < 7; i++)
                pCoin[i] = 0;
        }
    }

    void OnMouseDown() {
        if (coinMakeOk && coinMakeClear){   // 코인을 만들고 그 개수를 확인한다.
            Instantiate(coinPrefab, new Vector3(-3 + coinSpawn, -2, 0), Quaternion.identity);
            switch (coinPrefab.name){
                case "clonCoin1":
                    pCoin[0]++;
                    break;
                case "clonCoin2":
                    pCoin[1]++;
                    break;
                case "clonCoin3":
                    pCoin[2]++;
                    break;
                case "clonCoin4":
                    pCoin[3]++;
                    break;
                case "clonCoin5":
                    pCoin[4]++;
                    break;
                case "clonCoin6":
                    pCoin[5]++;
                    break;
                case "clonCoin-":
                    pCoin[6]++;
                    break;
            }
        }
    }
}
