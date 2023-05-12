using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectSkillCard : MonoBehaviour{
    [SerializeField]
    GameObject cardPrefab;
    [SerializeField]
    GameObject cardClick;

    public int[] SelectCard => selectCard;
    int[] selectCard = new int[6] { 0, 0, 0, 0, 0, 0 };
    int count, selectCount;

    // Start is called before the first frame update
    void Start(){
        count = 0;
        selectCount = 0;
    }

    // Update is called once per frame
    void Update(){

    }

    void OnMouseDown(){
        selectCount = GameObject.Find("nextGame").GetComponent<NextGame>().SelectCount;
        if(selectCount != 2)    // 스킬은 2개만 선택할 수 있다.
            if (count == 0){
                Instantiate(cardClick, cardClick.transform.position, Quaternion.identity);
                switch (cardPrefab.name){
                    case "skillLockCard":
                        selectCard[0] = 1;
                        break;
                    case "hpRecoveryCard":
                        selectCard[1] = 1;
                        break;
                    case "diceRePlayCard":
                        selectCard[2] = 1;
                        break;
                    case "dicePlusOneCard":
                        selectCard[3] = 1;
                        break;
                    case "diceMinusOneCard":
                        selectCard[4] = 1;
                        break;
                    case "coinPredictCard":
                        selectCard[5] = 1;
                        break;
                }
                count++;
            }
    }
}
