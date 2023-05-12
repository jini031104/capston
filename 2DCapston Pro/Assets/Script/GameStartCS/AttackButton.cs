using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackButton : MonoBehaviour
{
    public bool Attack => attack;
    bool attack;

    // Start is called before the first frame update
    void Start()
    {
        attack = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        Debug.Log("attack!");
        attack = true;
    }
}
