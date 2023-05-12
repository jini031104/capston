using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextGame : MonoBehaviour
{
    public int SelectCount => selectCount;
    int selectCount;

    // Start is called before the first frame update
    void Start(){
        selectCount = 0;
    }

    // Update is called once per frame
    void Update(){
        selectCount = GameObject.FindGameObjectsWithTag("CardClick").Length;
    }

    void OnMouseDown(){
        if (selectCount == 2)
            SceneManager.LoadScene("MainGameCS");
    }
}
