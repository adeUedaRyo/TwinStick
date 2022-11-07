using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] int _hp = 100;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ChangeHP(int value)
    {
        _hp += value; 
    }

}
