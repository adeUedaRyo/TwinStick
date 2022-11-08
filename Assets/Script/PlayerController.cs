using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    int _skillIndex = 0;
    [SerializeField] float _speed = 5;      //現在の移動速度
    [SerializeField] float _defaultSpeed = 5; //移動速度の初期値
    [SerializeField] float _dashSpeed = 10; //移動速度の初期値
    [SerializeField] float _jumpPower = 3;  //ジャンプ力
    [SerializeField] bool _left = true;    //左のキャラかどうか
    [SerializeField] int _avoid = 3;//回避の速度
    [SerializeField] float _avoidCoolTime = 1.5f; //回避のクールタイム


    Rigidbody _rb = default;

    // Start is called before the first frame update
    Vector2 _stickValue = default;
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
        if (_left)//左
        {
            //スティック入力の検出
            _stickValue = gamepad.leftStick.ReadValue().normalized;

            //ジャンプ
            if (gamepad.leftShoulder.wasPressedThisFrame) _rb.AddForce(0, _jumpPower, 0, ForceMode.Impulse);

            

            //スキル変更
            if (gamepad.dpad.up.wasPressedThisFrame) _skillIndex = 0; //方向キー上
            if (gamepad.dpad.right.wasPressedThisFrame) _skillIndex = 1; //方向キー右
            if (gamepad.dpad.left.wasPressedThisFrame) _skillIndex = 2; //方向キー左
            if (gamepad.dpad.down.wasPressedThisFrame) _skillIndex = 3; //方向キー下

            if (gamepad.leftTrigger.wasPressedThisFrame && _skillIndex == 0)
            {
                Debug.Log("回復");
            }
            else if (gamepad.leftTrigger.wasPressedThisFrame && _skillIndex == 1)
            {
                Debug.Log("ダッシュ");
                _rb.AddForce(transform.forward * _avoid,ForceMode.Impulse);
                _speed = _dashSpeed;
            }
            else if (gamepad.leftTrigger.wasPressedThisFrame && _skillIndex == 2)
            {
                Debug.Log("攻撃");
            }
            else if (gamepad.leftTrigger.isPressed && _skillIndex == 3)
            {
                Debug.Log("ガード");
            }
            else if (gamepad.leftTrigger.wasReleasedThisFrame)
            {
                _speed = _defaultSpeed;
            }

        }
        else//右
        {
            //スティック入力の検出
            _stickValue = gamepad.rightStick.ReadValue().normalized;

            //ジャンプ
            if (gamepad.rightShoulder.wasPressedThisFrame) _rb.AddForce(0, _jumpPower, 0, ForceMode.Impulse);


            //スキル変更
            if (gamepad.buttonNorth.wasPressedThisFrame) _skillIndex = 0; //PSだと△
            if (gamepad.buttonWest.wasPressedThisFrame) _skillIndex = 1; //PSだと□
            if (gamepad.buttonEast.wasPressedThisFrame) _skillIndex = 2; //PSだと○
            if (gamepad.buttonSouth.wasPressedThisFrame) _skillIndex = 3; //PSだと×
        }

        // 移動
        if (_stickValue != new Vector2(0, 0))
        {
            var dir = new Vector3(_stickValue.x, 0, _stickValue.y);
            transform.localRotation = Quaternion.LookRotation(dir);
            transform.Translate(0, 0, _speed * Time.deltaTime);

        }
    }
}
