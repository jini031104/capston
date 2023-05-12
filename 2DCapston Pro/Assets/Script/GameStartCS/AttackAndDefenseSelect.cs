using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AttackAndDefenseSelect : MonoBehaviour
{
    // bool firstChoice;

    public static bool attack, defense;

    // Start is called before the first frame update
    void Start(){
        attack = false;
        defense = false;
    }

    // Update is called once per frame
    void Update(){
        // �� ��ư���� true/false ���θ� �����´�.
        attack = GameObject.Find("attackButton").GetComponent<AttackButton>().Attack;
        defense = GameObject.Find("defenseButton").GetComponent<DefenseButton>().Defense;

        if (attack || defense)  // ��ư�� Ŭ���ϸ� ���� ������ �Ѿ �� �ְ� �Ѵ�.
            GameObject.Find("playerDice").GetComponent<GameStartDice>().gameStart();
    }
}
