using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Calculate : MonoBehaviour
{
    int[] pCoin = new int[7] { 0, 0, 0, 0, 0, 0, 0 };
    int[] eCoin = new int[7] { 0, 0, 0, 0, 0, 0, 0 };
    int[] pCoinCopy = new int[7] { 0, 0, 0, 0, 0, 0, 0 };
    int[] eCoinCopy = new int[7] { 0, 0, 0, 0, 0, 0, 0 };

    int[] count = new int[7] { 0, 0, 0, 0, 0, 0, 0 };

    int[] pCoinLeft = new int[7] { 0, 0, 0, 0, 0, 0, 0 };
    int[] eCoinLeft = new int[7] { 0, 0, 0, 0, 0, 0, 0 };

    [SerializeField]
    TextMeshProUGUI playerHpText;
    [SerializeField]
    TextMeshProUGUI enemyHpText;
    [SerializeField]
    TextMeshProUGUI turnCountText;

    public int PlayerHP => playerHP;
    public int EnemyHP => enemyHP;
    int coinLeft, playerHP, enemyHP, damage;

    string[] coinName = new string[] { "coin1", "coin2", "coin3", "coin4", "coin5", "coin6", "coin-" };
    string[] enemyCoinName = new string[] { "ecoin1", "ecoin2", "ecoin3", "ecoin4", "ecoin5", "ecoin6", "ecoin-" };
    string[] deleteCoinName = new string[] { "clonCoin1(Clone)", "clonCoin2(Clone)", "clonCoin3(Clone)", "clonCoin4(Clone)", "clonCoin5(Clone)", "clonCoin6(Clone)", "clonCoin-(Clone)" };
    string[] deleteEnemyCoinName = new string[] { "eClon1(Clone)", "eClon2(Clone)", "eClon3(Clone)", "eClon4(Clone)", "eClon5(Clone)", "eClon6(Clone)", "eClon-(Clone)" };

    public bool DeleteOK => deleteOK;
    public bool PlayerTurn => playerTurn;
    public bool DeleteCoinNum => deleteCoinNum;
    bool deleteOK, playerTurn, deleteCoinNum;

    public bool Attack => attack;
    public bool Defense => defense;
    bool attack, defense, AttackOrDefenseChangeOk;

    public bool CoinRotation => coinRotation;
    bool coinRotation;

    public static int turnCount;
    int attackResult, clickCount;

    public static bool playerWin, enemyWin;
    bool cardClick, playerTurnCheck;

    // Start is called before the first frame update
    void Start(){
        playerWin = false;
        enemyWin = false;
        clickCount = 0;
        turnCount = 0;
        coinLeft = 0;
        playerHP = (int)GameObject.Find("playerHP").GetComponent<Slider>().value;
        enemyHP = (int)GameObject.Find("enemyHP").GetComponent<Slider>().value;
        damage = 0;
        deleteOK = false;
        deleteCoinNum = false;
        attackResult = GameObject.Find("changeButton").GetComponent<ChangeButton>().AttackResult;
        if (attackResult == 0)
            playerTurn = false;
        else if (attackResult == 1)
            playerTurn = true;
        AttackOrDefenseChangeOk = false;
    }

    // Update is called once per frame
    void Update(){
        attack = GameObject.Find("delete_Coin").GetComponent<delete_Coin>().Attack;

        if (attack){
            for (int i = 0; i < 6; i++)
                pCoin[i] = GameObject.Find(coinName[i]).GetComponent<MakeCoin>().PCoin[i];
            for (int i = 0; i < 7; i++)
                eCoin[i] = GameObject.Find(enemyCoinName[i]).GetComponent<EnemyMakeClickCoin>().ECoin[i];
        }
        else{
            for (int i = 0; i < 7; i++)
                pCoin[i] = GameObject.Find(coinName[i]).GetComponent<MakeCoin>().PCoin[i];
            for (int i = 0; i < 6; i++)
                eCoin[i] = GameObject.Find(enemyCoinName[i]).GetComponent<EnemyMakeClickCoin>().ECoin[i];
        }

        //���� �ڵ� ���� ���� �갡 �ʿ�.....
        //eCoin = GameObject.Find("enemyCoin").GetComponent<EnemyMakeCoin>().ECoin;

        //cardClick = GameObject.Find("hpRecoveryCard").GetComponent<HpRecovery>().CardClick;
        //playerTurnCheck = GameObject.Find("changeButton").GetComponent<ChangeButton>().PlayerTurn;
        //if (cardClick){
        //    if (playerTurnCheck)
        //        playerHP = GameObject.Find("hpRecoveryCard").GetComponent<HpRecovery>().PlayerHP;
        //    else
        //        enemyHP = GameObject.Find("hpRecoveryCard").GetComponent<HpRecovery>().EnemyHP;
        //}

        playerHpText.text = " " + playerHP;
        enemyHpText.text = " " + enemyHP;
        turnCountText.text = "Turn: " + turnCount;
        GameObject.Find("playerHP").GetComponent<Slider>().value = playerHP;
        GameObject.Find("enemyHP").GetComponent<Slider>().value = enemyHP;

        if (AttackOrDefenseChangeOk){
            AttackOrDefenseChange();
            AttackOrDefenseChangeOk = false;
        }

        if (playerHP <= 0){     // ���� �̱�
            playerHP = 0;
            enemyWin = true;
            if (Input.GetMouseButtonDown(1))
                SceneManager.LoadScene("GameOver");
        }
        else if(enemyHP <= 0){  // �÷��̾ �̱�
            enemyHP = 0;
            playerWin = true;
            if (Input.GetMouseButtonDown(1))
                SceneManager.LoadScene("GameOver");
        }
    }

    void OnMouseDown(){
        CoinCheck();
        HpResult();
        clickCount++;
        if (clickCount % 2 == 0)
            turnCount++;

        deleteOK = true;
        deleteCoinNum = true;
        AttackOrDefenseChangeOk = true;
        coinRotation = true;

        if (attackResult == 0)
            playerTurn = true;
        else if (attackResult == 1)
            playerTurn = false;

        for (int i = 0; i < 7; i++)
            count[i] = 0;
    }

    void OnMouseUp(){
        if (attackResult == 0)
            playerTurn = false;
        else if (attackResult == 1)
            playerTurn = true;

        deleteCoinNum = false;
        coinRotation = false;

        Debug.Log("�÷��̾� ����1:" + pCoinCopy[0] + " ����2:" + pCoinCopy[1] + " ����3:" + pCoinCopy[2] + " ����4:" + pCoinCopy[3] + " ����5:" + pCoinCopy[4] + " ����6:" + pCoinCopy[5] + " ����-:" + pCoinCopy[6]);
        Debug.Log("�� ����1:" + eCoinCopy[0] + " ����2:" + eCoinCopy[1] + " ����3:" + eCoinCopy[2] + " ����4:" + eCoinCopy[3] + " ����5:" + eCoinCopy[4] + " ����6:" + eCoinCopy[5] + " ����-:" + eCoinCopy[6]);
        Debug.Log("playerHP: " + playerHP + "             enemyHP: " + enemyHP);
        Debug.Log("���� �÷��̾� ����1:" + pCoinLeft[0] + " ����2:" + pCoinLeft[1] + " ����3:" + pCoinLeft[2] + " ����4:" + pCoinLeft[3] + " ����5:" + pCoinLeft[4] + " ����6:" + pCoinLeft[5] + " ����-:" + pCoinLeft[6]);
        Debug.Log("���� �� ����1:" + eCoinLeft[0] + " ����2:" + eCoinLeft[1] + " ����3:" + eCoinLeft[2] + " ����4:" + eCoinLeft[3] + " ����5:" + eCoinLeft[4] + " ����6:" + eCoinLeft[5] + " ����-:" + eCoinLeft[6]);

        int pMin = 0, pCount = 0;
        int eMin = 0, eCount = 0; ;
        for(int i=0; i<6; i++){
            if (0 < pCoinLeft[i] && pCount == 0){
                pMin = i + 1;
                pCount++;
            }
            if (0 < eCoinLeft[i] && eCount == 0){
                eMin = i + 1;
                eCount++;
            }
        }

        Debug.Log("���� �÷��̾� ���� �� ���� ���� ��: " + pMin);
        Debug.Log("���� �� ���� �� ���� ���� ��: " + eMin);

        deleteOK = false;
    }

    void AttackOrDefenseChange(){
        if (attack){
            attack = false;
            defense = true;
        }
        else{
            attack = true;
            defense = false;
        }
    }

    void CoinCheck(){
        for (int i = 0; i < 7; i++){
            pCoinCopy[i] = 0;
            eCoinCopy[i] = 0;

            // -���� ����.
            if (pCoin[6] == 0)      // -������ 0���� ���, ��ġ�� ���� ������ �״�� ���.
                pCoinCopy[i] = pCoin[i];
            else if (pCoin[6] == 1) // -������ �ִ� ���, ��ġ�� ���ε��� ��ġ�� ���� ���� �� ����.
                if (pCoin[i] > 0)
                    pCoinCopy[i] = 6;

            if (eCoin[6] == 0)
                eCoinCopy[i] = eCoin[i];
            else if (eCoin[6] == 1)
                if (eCoin[i] > 0)
                    eCoinCopy[i] = 6;

            // ���� ���� ���.
            if (pCoinCopy[i] > eCoinCopy[i]){   // �÷��̾� ������ �� ����.
                coinLeft = pCoinCopy[i] - eCoinCopy[i];
                pCoinLeft[i] = coinLeft;
                eCoinLeft[i] = 0;
            }
            else if (pCoinCopy[i] < eCoinCopy[i]){  // �� ������ �� ����.
                coinLeft = eCoinCopy[i] - pCoinCopy[i];
                eCoinLeft[i] = coinLeft;
                pCoinLeft[i] = 0;
            }
            else if (pCoinCopy[i] == eCoinCopy[i]){
                pCoinLeft[i] = 0;
                eCoinLeft[i] = 0;
            }

            if (pCoinCopy[i] == 0)
                pCoinLeft[i] = 0;
            if (eCoinCopy[i] == 0)
                eCoinLeft[i] = 0;
        }
    }

    void HpResult(){    // HP ���.
        if (attack)
            for (int i = 0; i < 6; i++){
                damage = (i + 1) * pCoinLeft[i];
                enemyHP -= damage;
            }
        else
            for (int i = 0; i < 6; i++){
                damage = (i + 1) * eCoinLeft[i];
                playerHP -= damage;
            }
    }
}
