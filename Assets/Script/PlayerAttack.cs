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
            Debug.Log("�ˌ�");
        }
        else if(_weaponIndex == 1)
        {
            Debug.Log("�ˌ�");
        }
        else if(_weaponIndex == 2)
        {
            Debug.Log("�a��");
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
