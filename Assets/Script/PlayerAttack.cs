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
        if(_skillIndex == 0) //�㑤�̃{�^��
        {
            Debug.Log("�a��"); 
        }
        else if(_skillIndex == 1) //�����̃{�^��
        {
            Debug.Log("��");
        }
        else if(_skillIndex == 2) //�O���̃{�^��
        {
            Debug.Log("�ˌ�");
        }
        else if(_skillIndex == 3) //�����̃{�^��
        {
            Debug.Log("�h��");
        }
    }
    public void SkillChange(int i)
    {
        _skillIndex = i;
    }
    
}
