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
            Debug.Log("�ֻ��� �ٽ� ������ ����");
        }
        else{
            startDice = false;
            Debug.Log("�̹� ��ų�� ����߽��ϴ�.");
        }
        count++;
    }
}
