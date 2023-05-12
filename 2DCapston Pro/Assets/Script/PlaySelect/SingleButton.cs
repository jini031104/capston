using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SingleButton : MonoBehaviour
{
    public static bool singleSelect;

    // Start is called before the first frame update
    void Start(){
        singleSelect = false;
    }

    // Update is called once per frame
    void Update(){
        
    }

    void OnMouseDown(){
        singleSelect = true;
        SceneManager.LoadScene("GameStart");
    }
}
