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
        // 각 버튼에서 true/false 여부를 가져온다.
        attack = GameObject.Find("attackButton").GetComponent<AttackButton>().Attack;
        defense = GameObject.Find("defenseButton").GetComponent<DefenseButton>().Defense;

        if (attack || defense)  // 버튼을 클릭하면 다음 씬으로 넘어갈 수 있게 한다.
            GameObject.Find("playerDice").GetComponent<GameStartDice>().gameStart();
    }
}
