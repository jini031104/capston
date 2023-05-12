using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MultiButton : MonoBehaviour
{
    public static bool multiSelect;

    // Start is called before the first frame update
    void Start(){
        multiSelect = false;
    }

    // Update is called once per frame
    void Update(){
        
    }

    void OnMouseDown(){
        multiSelect = true;
        SceneManager.LoadScene("GameStart");
    }
}
