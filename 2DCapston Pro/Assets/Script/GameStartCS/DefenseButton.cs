using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseButton : MonoBehaviour
{
    public bool Defense => defense;
    bool defense;

    // Start is called before the first frame update
    void Start()
    {
        defense = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        Debug.Log("defense!");
        defense = true;
    }
}
