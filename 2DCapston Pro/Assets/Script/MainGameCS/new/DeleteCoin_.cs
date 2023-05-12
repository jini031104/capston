using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteCoin_ : MonoBehaviour
{
    [SerializeField]
    GameObject[] coin_;

    bool attack, defense;

    // Start is called before the first frame update
    void Start()
    {
        attack = AttackAndDefenseSelect.attack;
        defense = AttackAndDefenseSelect.defense;
    }

    // Update is called once per frame
    void Update()
    {
        if (attack)
        {
            coin_[0].SetActive(false);
            coin_[1].SetActive(true);
        }
        else if (defense)
        {
            coin_[0].SetActive(true);
            coin_[1].SetActive(false);
        }
    }
}
