using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] float _speed = 5;      //�ړ����x
    [SerializeField] float _jumpPower = 3;  //�W�����v��
    [SerializeField] bool _left = true;    //���̃L�������ǂ���
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
            Debug.Log("�Q�[���p�b�h������܂���B");
            return;
        }
        
        //�ړ�
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
        
        
        //�W�����v
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
