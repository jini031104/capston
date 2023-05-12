using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinFix : MonoBehaviour
{
    bool coinRotation;

    // Start is called before the first frame update
    void Start(){

    }

    // Update is called once per frame
    void Update(){
        coinRotation = GameObject.Find("startButton").GetComponent<Calculate>().CoinRotation;

        if(coinRotation)
            transform.GetChild(1).GetComponent<SpriteRenderer>().sortingOrder = 0;
    }
}
