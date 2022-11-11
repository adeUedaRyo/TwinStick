using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] int _hp = 100;
    [SerializeField] int _maxHp = 100; // HP�ő�l
    [SerializeField] Slider _hpSlider = null; // HP�o�[


    void Start()
    {
        _hp = _maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        _hpSlider.value = (float)_hp / (float)_maxHp;
    }
    public void ChangeHP(int value)
    {
        _hp += value;
        if(_hp > _maxHp) _hp = _maxHp;
        
    }


}
