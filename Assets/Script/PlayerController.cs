using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float _speed = 5;      //移動速度
    [SerializeField] float _jumpPower = 3;  //ジャンプ力
    [SerializeField] bool _left = true;    //左のキャラかどうか
    Rigidbody _rb = default;
    // Start is called before the first frame update
    Vector2 _stickValue = default;
    PlayerAttack _attack = default;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _attack = GetComponent<PlayerAttack>();
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

            //攻撃
            if (gamepad.leftTrigger.wasPressedThisFrame) _attack.Attack();

            //スキル変更
            if (gamepad.dpad.up.wasPressedThisFrame) _attack.SkillChange(0); //方向キー上
            if (gamepad.dpad.right.wasPressedThisFrame) _attack.SkillChange(1); //方向キー右
            if (gamepad.dpad.left.wasPressedThisFrame) _attack.SkillChange(2); //方向キー左
            if (gamepad.dpad.down.wasPressedThisFrame) _attack.SkillChange(3); //方向キー下
        }
        else//右
        {
            //スティック入力の検出
            _stickValue = gamepad.rightStick.ReadValue().normalized;

            //ジャンプ
            if (gamepad.rightShoulder.wasPressedThisFrame) _rb.AddForce(0, _jumpPower, 0, ForceMode.Impulse);


            //攻撃
            if (gamepad.rightTrigger.wasPressedThisFrame) _attack.Attack();

            //スキル変更
            if (gamepad.buttonNorth.wasPressedThisFrame) _attack.SkillChange(0); //PSだと△
            if (gamepad.buttonWest.wasPressedThisFrame) _attack.SkillChange(1); //PSだと□
            if (gamepad.buttonEast.wasPressedThisFrame) _attack.SkillChange(2); //PSだと○
            if (gamepad.buttonSouth.wasPressedThisFrame) _attack.SkillChange(3); //PSだと×
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
