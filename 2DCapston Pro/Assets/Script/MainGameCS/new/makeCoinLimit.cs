using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class makeCoinLimit : MonoBehaviour
{
    GameObject[] coinTag;

    int[] coinNum = new int[7];
    int coinCountResult, diceNum;

    public bool StartDiceCheck => startDiceCheck;
    public bool DiceReplay => diceReplay;
    bool startDiceCheck, diceReplay;

    public bool DiceChange => diceChange;
    bool diceChange;

    //bool firstChoice;

    // Start is called before the first frame update
    void Start()
    {
        coinTag = GameObject.FindGameObjectsWithTag("CoinTag");
        diceReplay = false;
    }

    // Update is called once per frame
    public void Update()
    {
        startDiceCheck = GameObject.Find("dice").GetComponent<DiceRotation>().StartDice;
        // firstChoice = GameObject.Find("playerDice").GetComponent<GameStartDice>().FirstChoice;
        diceChange = GameObject.Find("dice").GetComponent<DiceRotation>().DiceChange;

        if (startDiceCheck) // 주사위 클릭하면 작동함.
        {
            diceReplay = true;
            diceNum = GameObject.Find("dice").GetComponent<DiceRotation>().IndexVall;
            diceNum++;

            coinCountResult = 0;
            for (int i = 0; i < coinTag.Length; i++)
            {
                coinNum[i] = coinTag[i].GetComponent<ClickCreateClone>().MakeNum;
                coinCountResult += coinNum[i];
            }
            Debug.Log("make Coin Num: " + coinCountResult);
            Debug.Log("Dice Num: " + diceNum);

            // If DiceNum and make CoinNum same, We have Dice roll replay check.
            if (coinCountResult == diceNum)
            {
                startDiceCheck = false;
                diceReplay = false;
                diceChange = true;
            }
        }
        else
        {
            Debug.Log("Dice Roll");
        }
    }
}
