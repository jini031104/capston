using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartDice : MonoBehaviour
{
    [SerializeField]
    private GameObject playerDice;
    Vector3 playerDiceScale;

    [SerializeField]
    private GameObject enemyDice;
    Vector3 enemyDiceScale;

    [SerializeField]
    private GameObject Button;

    int[] diceVall = new int[] { 1, 2, 3, 4, 5, 6 };
    int[,] rotationVal = new int[,] { { 40, 135, 270 },
                                        {40, 45, 90 },
                                        {130, 45, 0 } };
    int rVall, dVall;
    public static int enemyDiceVal, playerDiceVal;

    bool gameStartCheck = false;
    
    public bool FirstChoice => firstChoice;
    bool firstChoice = false;
    bool diceDrop = false, diceRoll = false;

    bool multiSelect, singleSelect, test;
    public static bool enemySelectTurn;

    // Start is called before the first frame update
    void Start(){
        enemySelectTurn = false;

        // 주사위의 크기 값을 가져옴.
        playerDiceScale = playerDice.gameObject.transform.localScale;
        enemyDiceScale = enemyDice.gameObject.transform.localScale;

        dVall = diceVall.GetLength(0);
        rVall = rotationVal.GetLength(0);

        multiSelect = MultiButton.multiSelect;
        singleSelect = SingleButton.singleSelect;

        Button.SetActive(false);    // 게임 시작시, 공/방 버튼은 비활성 처리한다.
    }

    int i = 0;

    // Update is called once per frame
    void Update(){
        if(diceDrop){
            if(singleSelect)
                if(enemyDiceVal < playerDiceVal){
                    Button.SetActive(true);
                    firstChoice = true;
                    //gameStart();
                }
                else if(enemyDiceVal == playerDiceVal){
                    firstChoice = false;
                    diceDrop = false;
                }
                else
                    gameStart();

            if (multiSelect && enemyDiceVal != playerDiceVal)
                Button.SetActive(true);
        }
        test = GameObject.Find("testServer").GetComponent<testServer>().test;
        if(test && i == 0){
            enemyDice.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            i = 1;
        }

        SmallDice(playerDice, playerDiceScale);
        SmallDice(enemyDice, enemyDiceScale);
        //Debug.Log("playerDiceVal: " + playerDiceVal + " enemyDiceVal: " + enemyDiceVal);
    }

    public void gameStart(){
        if(gameStartCheck){
            if(Input.GetMouseButtonDown(0))
                SceneManager.LoadScene("SkillSelect");
        }
    }

    private void OnMouseDown(){ // 주사위를 클릭하면, 주사위가 커진다.
        if (!diceRoll){
            playerDice.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        }
    }

    private void OnMouseUp(){   // 주사위를 굴린다.
        if (!diceRoll){
            DiceDrpoAndValSet(playerDice);
            //DiceDrpoAndValSet(enemyDice);
        }

        if (playerDiceVal > enemyDiceVal)
            Debug.Log("플레이어 우선권");
        else if (playerDiceVal < enemyDiceVal){
            enemySelectTurn = true;
            Debug.Log("적 우선권");
        }
        else if(playerDiceVal == enemyDiceVal)
            Debug.Log("다시 굴리기");
        Debug.Log("playerDiceVal: " + playerDiceVal + "enemyDiceVal: " + enemyDiceVal);
    }

    public void DiceDrpoAndValSet(GameObject Dice){   // 주사위 값 세팅
        int rotIndex = Random.Range(0, rVall);
        Dice.transform.localEulerAngles = new Vector3(rotationVal[rotIndex, 0], rotationVal[rotIndex, 1], rotationVal[rotIndex, 2]);

        int valIndex;
        valIndex = Random.Range(0, dVall);
        if(Dice.name == "playerDice")
            playerDiceVal = valIndex;
        //if(Dice.name == "enemyDice")
        //    enemyDiceVal = valIndex;
    }

    void SmallDice(GameObject Dice, Vector3 diceScale){ // 주사위가 떨어지는 동안 점점 작아짐.
        if (Dice.transform.localScale.x > diceScale.x){
            Dice.transform.localScale -= new Vector3(0.01f, 0.01f, 0.01f);
        }
        else{
            DiceResult(playerDiceVal, playerDice);
            //DiceResult(enemyDiceVal, enemyDice);
            gameStartCheck = true;
            diceDrop = true;
            if(playerDiceVal != enemyDiceVal)
                diceRoll = true;
        }
    }

    void DiceResult(int valIndex, GameObject Dice){
        switch (valIndex){
            case 0:
                Dice.transform.localEulerAngles = new Vector3(0, 180, 0);
                break;
            case 1:
                Dice.transform.localEulerAngles = new Vector3(0, 90, -90);
                break;
            case 2:
                Dice.transform.localEulerAngles = new Vector3(0, 270, 0);
                break;
            case 3:
                Dice.transform.localEulerAngles = new Vector3(0, 90, 0);
                break;
            case 4:
                Dice.transform.localEulerAngles = new Vector3(0, 90, -270);
                break;
            case 5:
                Dice.transform.localEulerAngles = new Vector3(0, 0, 0);
                break;
        }
    }
}
