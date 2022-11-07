using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    int _skillIndex = 0;

    private void Start()
    {

    }

    private void Update()
    {
        
    }


    public void Attack()
    {
        if(_skillIndex == 0) //上側のボタン
        {
            Debug.Log("斬撃"); 
        }
        else if(_skillIndex == 1) //内側のボタン
        {
            Debug.Log("回復");
        }
        else if(_skillIndex == 2) //外側のボタン
        {
            Debug.Log("射撃");
        }
        else if(_skillIndex == 3) //下側のボタン
        {
            Debug.Log("防御");
        }
    }
    public void SkillChange(int i)
    {
        _skillIndex = i;
    }
    
}
