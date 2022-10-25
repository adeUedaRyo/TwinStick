using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] int _tacklePower = 3;
    int _weaponIndex = 0;
    
    public void Attack()
    {
        if(_weaponIndex == 0)
        {
            Debug.Log("“ËŒ‚");
        }
        else if(_weaponIndex == 1)
        {
            Debug.Log("ŽËŒ‚");
        }
        else if(_weaponIndex == 2)
        {
            Debug.Log("ŽaŒ‚");
        }
    }
    public void WeaponChange(int i)
    {
        _weaponIndex = i;
    }
    void Tackle()
    {

    }
}
