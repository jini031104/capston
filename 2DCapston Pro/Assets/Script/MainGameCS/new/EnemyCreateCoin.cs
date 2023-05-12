using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyCreateCoin : MonoBehaviour
{
    [SerializeField]
    private GameObject[] coinPrefab;
    [SerializeField]
    private Transform spawnPoint;

    bool enemyDiceTurn, startDiceCheck;
    bool makeCoinAfterDice;

    int diceNum;
    int makeNum;
    int randomCoin;

    // Start is called before the first frame update
    void Start()
    {
        makeNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        enemyDiceTurn = GameObject.Find("dice").GetComponent<DiceRotation>().EnemyDice;
        startDiceCheck = GameObject.Find("dice").GetComponent<DiceRotation>().StartDice;

        makeCoinAfterDice = GameObject.Find("coin").GetComponent<makeCoinLimit>().StartDiceCheck;
        if (!makeCoinAfterDice)
            makeNum = 0;

        if (enemyDiceTurn && startDiceCheck)
        {
            diceNum = GameObject.Find("dice").GetComponent<DiceRotation>().IndexVall;
            diceNum++;

            Debug.Log("This is enemyDiceTurn!!!!!!!!!!!!!!!!!!!!!!!!!!!" + diceNum);
            Debug.Log("Enemy coin Create: " + makeNum);

            if(Input.GetMouseButtonDown(0)){
                for(int i=0; i<diceNum; i++){
                    randomCoin = Random.Range(0, 7);
                    GameObject clone = Instantiate(coinPrefab[randomCoin], new Vector3(i - 3, 2, 0), Quaternion.identity);
                    makeNum++;
                }
            }
        }
    }
}
