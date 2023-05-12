using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class delete_Coin : MonoBehaviour
{
    [SerializeField]
    GameObject[] coin_;

    public bool Attack => attack;
    bool attack, defense;
    bool multiSelect, singleSelect, enemySelectTurn;

    // Start is called before the first frame update
    void Start(){
        multiSelect = MultiButton.multiSelect;
        singleSelect = SingleButton.singleSelect;
        enemySelectTurn = GameStartDice.enemySelectTurn;

        //이걸로 적 주사위가 공방 선택할 수 있게 함.
        if (singleSelect){      //싱글 기준 플레이어 우선권
            attack = AttackAndDefenseSelect.attack;
            defense = AttackAndDefenseSelect.defense;
        }
        else if(multiSelect && enemySelectTurn){    //멀티 기준 적 우선권
            defense = AttackAndDefenseSelect.attack;
            attack = AttackAndDefenseSelect.defense;
        }
        else if (multiSelect && !enemySelectTurn){  //멀티 기준 플레이어 우선권
            attack = AttackAndDefenseSelect.attack;
            defense = AttackAndDefenseSelect.defense;
        }

        if (!attack && !defense)    // 싱글 플레이에서 적이 먼저 공방 정할 경우
            EnemySelectAttackOrDefense();
        DeleteCoin();
    }

    // Update is called once per frame
    void Update(){
        attack = GameObject.Find("startButton").GetComponent<Calculate>().Attack;
        defense = GameObject.Find("startButton").GetComponent<Calculate>().Defense;
        DeleteCoin();
    }

    void EnemySelectAttackOrDefense(){
        int val = Random.Range(0, 2);

        if (val == 0){
            defense = true;
            Debug.Log("defense!!");
        }
        else if (val == 1){
            attack = true;
            Debug.Log("attack!!");
        }
    }

    void DeleteCoin(){
        if (attack){
            coin_[0].SetActive(false);
            coin_[1].SetActive(true);
        }
        else if (defense){
            coin_[0].SetActive(true);
            coin_[1].SetActive(false);
        }
    }
}
