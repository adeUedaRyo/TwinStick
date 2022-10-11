using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] float _speed = 5;
    [SerializeField] bool _right = false;

    // Start is called before the first frame update
    Vector2 _stickValue;
    void Start()
    {
        
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
        
        if(!_right)
        {
            _stickValue = gamepad.leftStick.ReadValue().normalized;

        }
        else
        {
            _stickValue = gamepad.rightStick.ReadValue().normalized;
        }
        transform.Translate(_stickValue.x*_speed * Time.deltaTime,0,_stickValue.y * _speed * Time.deltaTime);
    }
}
