using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkillSet : MonoBehaviour {
    string[] cardName = new string[] { "skillLockCard", "hpRecoveryCard", "diceRePlayCard", "dicePlusOneCard", "diceMinusOneCard", "coinPredictCard" };

    public static int[] selectCard = new int[6] { 0, 0, 0, 0, 0, 0 };

    // Start is called before the first frame update
    void Start(){
    }

    // Update is called once per frame
    void Update(){
        for (int i = 0; i < cardName.Length; i++)   // 메인 게임에 넘겨줄 정보.
            selectCard[i] = GameObject.Find(cardName[i]).GetComponent<SelectSkillCard>().SelectCard[i];

        //Debug.Log(selectCard[0] + " " + selectCard[1] + " " + selectCard[2] + " " + selectCard[3] + " " + selectCard[4] + " " + selectCard[5] + " ");
    }
}
