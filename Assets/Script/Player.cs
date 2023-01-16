using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField,Tooltip("HP")] float _hp = 100;
    [SerializeField,Tooltip("HP�ő�l")] float _maxHp = 100;
    [SerializeField,Tooltip("HP�o�[")] Slider _hpSlider = null;

    bool _guard = false; // �o���A�̒��ɂ���
    void Start()
    {
        _hp = _maxHp;
        _hpSlider.value = (float)_hp / (float)_maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Damaged(float damage)
    {
        _hp -= damage;
        _hpSlider.value = (float)_hp / (float)_maxHp;
    }
    public void Recovery(float a)
    {
        _hp += a;
        if (_hp >= _maxHp) _hp = _maxHp;
        _hpSlider.value = (float)_hp / (float)_maxHp;
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Barrier")
        {
            _guard = true;
            Debug.Log("�K�[�h");
        }
    }
}
