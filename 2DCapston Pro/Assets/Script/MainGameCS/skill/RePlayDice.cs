using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RePlayDice : MonoBehaviour
{
    public bool StartDice => startDice;
    bool startDice;

    int count;

    // Start is called before the first frame update
    void Start(){
        count = 0;
        startDice = false;
    }

    // Update is called once per frame
    void Update(){
        
    }

    void OnMouseDown(){
        if (count == 0){
            startDice = true;
            Debug.Log("주사위 다시 굴리기 가능");
        }
        else{
            startDice = false;
            Debug.Log("이미 스킬을 사용했습니다.");
        }
        count++;
    }
}
