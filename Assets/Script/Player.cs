using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] float _speed = 5;      //移動速度
    [SerializeField] float _jumpPower = 3;  //ジャンプ力
    [SerializeField] bool _left = true;    //左のキャラかどうか
    Rigidbody _rb;
    // Start is called before the first frame update
    Vector2 _stickValue;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        var gamepad = Gamepad.current;
        if (gamepad == null)
        {
            Debug.Log("ゲームパッドがありません。");
            return;
        }
        
        //移動
        if(_left)
        {
            _stickValue = gamepad.leftStick.ReadValue().normalized; 
        }
        else
        {
            _stickValue = gamepad.rightStick.ReadValue().normalized;
        }
        if(_stickValue !=  new Vector2(0,0))
        {
            var dir = new Vector3(_stickValue.x,0,_stickValue.y);
            transform.localRotation = Quaternion.LookRotation(dir);
            transform.Translate(0, 0,_speed * Time.deltaTime);
            //transform.Translate(_stickValue.x * _speed * Time.deltaTime, 0, _stickValue.y * _speed * Time.deltaTime);
        }
        
        
        //ジャンプ
        if (_left)
        {
            if (gamepad.leftShoulder.wasPressedThisFrame)
            {
                _rb.AddForce(0, _jumpPower, 0, ForceMode.Impulse);
            }
        }
        else
        {
            if(gamepad.rightShoulder.wasPressedThisFrame)
            {
                _rb.AddForce(0,_jumpPower,0,ForceMode.Impulse);
            }
        }

    }
}
