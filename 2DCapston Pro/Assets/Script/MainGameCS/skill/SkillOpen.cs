using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillOpen : MonoBehaviour
{
    [SerializeField]
    GameObject[] skill;

    public int[] SelectCard => selectCard;
    int[] selectCard = new int[6] { 0, 0, 0, 0, 0, 0 };

    // Start is called before the first frame update
    void Start()
    {
        selectCard = SkillSet.selectCard;
        for (int i = 0; i < selectCard.Length; i++)
            if (selectCard[i] == 1)
                skill[i].SetActive(true);
            else
                skill[i].SetActive(false);
        Debug.Log(selectCard[0] + " " + selectCard[1] + " " + selectCard[2] + " " + selectCard[3] + " " + selectCard[4] + " " + selectCard[5] + " ");
        // skillLockCard, hpRecoveryCard, diceRePlayCard, dicePlusOneCard, diceMinusOneCard, coinPredictCard
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
