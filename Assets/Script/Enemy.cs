using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] Transform _muzzle = null;
    [SerializeField] GameObject _bullet = null;

    [SerializeField,Tooltip("攻撃のクールタイム")] float _attackCT = 2.5f;
    [SerializeField, Tooltip("HP")] int _hp = 100;
    float _time = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _time += Time.deltaTime;
        if (_time >= _attackCT)
        {
            Instantiate(_bullet,_muzzle);
            _time = 0;
        }
    }
    public void Damaged(int damage)
    {
        _hp -= damage;
        Debug.Log(_hp);
    }
}
