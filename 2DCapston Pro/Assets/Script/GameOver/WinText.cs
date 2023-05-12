using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinText : MonoBehaviour{
    [SerializeField]
    TextMeshProUGUI winText;
    [SerializeField]
    TextMeshProUGUI turnCountText;

    int turnCount;

    bool playerWin, enemyWin;

    // Start is called before the first frame update
    void Start(){
        turnCount = Calculate.turnCount;
        playerWin = Calculate.playerWin;
        enemyWin = Calculate.enemyWin;

        turnCountText.text = " " + turnCount;

        if (playerWin)
            winText.text = "PLAYER";
        if (enemyWin)
            winText.text = "ENEMY";
    }

    // Update is called once per frame
    void Update(){
        
    }
}
