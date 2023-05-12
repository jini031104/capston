using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagMove : MonoBehaviour
{
    [SerializeField]
    GameObject[] flag;

    bool playerTurn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerTurn = GameObject.Find("changeButton").GetComponent<ChangeButton>().PlayerTurn;

        if (playerTurn){
            flag[0].SetActive(true);    // playerFlag
            flag[1].SetActive(false);   // enemyFlag
        }
        else{
            flag[0].SetActive(false);
            flag[1].SetActive(true);
        }
    }
}
