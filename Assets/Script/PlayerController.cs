using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    int _skillIndex = 0;
    [SerializeField,Tooltip("移動速度")] float _speed = 5;      
    [SerializeField,Tooltip("移動速度の初期値")] float _defaultSpeed = 5;
    [SerializeField,Tooltip("ダッシュ中の移動速度")] float _dashSpeed = 10;
    [SerializeField,Tooltip("ジャンプ力")] float _jumpPower = 3;  
    [SerializeField,Tooltip("左のキャラかどうか")] bool _left = true;
    [SerializeField,Tooltip("回避の速度")] int _avoid = 3;
    [SerializeField,Tooltip("回避のクールタイム")] float _avoidCoolTime = 1f;
    [SerializeField,Tooltip("攻撃力")] int _attackPower = 5;
    [SerializeField,Tooltip("攻撃のクールタイム")] float _attackCoolTime = 1.5f;
    [SerializeField,Tooltip("バリア")] GameObject _barrier = null;
    [SerializeField,Tooltip("弾（ため無し、タメ弱、タメ強）")] GameObject _bullet, _bulletA, _bulletB = null;
    [SerializeField,Tooltip("回復エリア")] GameObject _recoveryArea = null;
    [SerializeField] Transform _muzzle = null;
    Rigidbody _rb = default;
    float _attackChargeTime = 0; // 攻撃をためている時間
    float _timeAvoid, _timeAttack = 0;


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

        _timeAvoid += Time.deltaTime;
        _timeAttack += Time.deltaTime;
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
                Recovery();
            }
            else if (gamepad.leftTrigger.wasPressedThisFrame && _skillIndex == 1 )
            {
                if (_avoidCoolTime <= _timeAvoid)
                {
                    _rb.AddForce(transform.forward * _avoid, ForceMode.Impulse); // クールタイムが終わっていたら回避する
                    _timeAvoid = 0;
                }
                    _speed = _dashSpeed; // ボタンを離すまでスピードアップ
            }
            else if (gamepad.leftTrigger.isPressed && _skillIndex == 2 && _attackCoolTime <= _timeAvoid) // 攻撃を溜める
            {
                _attackChargeTime += Time.deltaTime;
                _speed = _defaultSpeed / 5.0f;
            }
            else if (gamepad.leftTrigger.isPressed && _skillIndex == 3)
            {
                _barrier.SetActive(true);
            }
            else if (gamepad.leftTrigger.wasReleasedThisFrame)　//ボタンを離したら
            {
                if (_attackChargeTime != 0 && _skillIndex == 2) // 攻撃をためていたら
                {
                    Attack();
                }

                _speed = _defaultSpeed; // 元のスピードに戻る
                _attackChargeTime = 0;
                _timeAttack = 0;
                _barrier.SetActive(false);
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

            if (gamepad.rightTrigger.wasPressedThisFrame && _skillIndex == 0)
            {
                Recovery();
            }
            else if (gamepad.rightTrigger.wasPressedThisFrame && _skillIndex == 1)
            {
                if (_avoidCoolTime <= _timeAvoid)
                {
                    _rb.AddForce(transform.forward * _avoid, ForceMode.Impulse); // クールタイムが終わっていたら回避する
                    _timeAvoid = 0;
                }
                _speed = _dashSpeed; // ボタンを離すまでスピードアップ
            }
            else if (gamepad.rightTrigger.isPressed && _skillIndex == 2 && _attackCoolTime <= _timeAvoid) // 攻撃を溜める
            {
                _attackChargeTime += Time.deltaTime;
                _speed = _defaultSpeed / 5.0f;
            }
            else if (gamepad.rightTrigger.isPressed && _skillIndex == 3)
            {
                _barrier.SetActive(true);
            }
            else if (gamepad.rightTrigger.wasReleasedThisFrame)　//ボタンを離したら
            {
                if (_attackChargeTime != 0 && _skillIndex == 2) // 攻撃をためていたら
                {
                    Attack();
                }

                _speed = _defaultSpeed; // 元のスピードに戻る
                _attackChargeTime = 0;
                _timeAttack = 0;
                _barrier.SetActive(false);
            }
        }

        // 移動
        if (_stickValue != new Vector2(0, 0))
        {
            var dir = new Vector3(_stickValue.x, 0, _stickValue.y);
            transform.localRotation = Quaternion.LookRotation(dir);
            transform.Translate(0, 0, _speed * Time.deltaTime);

        }
    }
    void Attack()
    {
        if (_attackChargeTime >= 3f)
        {
            Instantiate(_bulletB, _muzzle.position, this.transform.rotation);
            Debug.Log("タメ攻撃");
        }
        else if (_attackChargeTime >= 1f)
        {
            Instantiate(_bulletA, _muzzle.position, this.transform.rotation);
            Debug.Log("小タメ攻撃");
        }
        else
        {
            Instantiate(_bullet, _muzzle.position, this.transform.rotation);
            Debug.Log("攻撃");
        }
    }
    void Recovery()
    {
        Instantiate(_recoveryArea, new Vector3(this.transform.position.x, 0 , this.transform.position.z), this.transform.rotation);
        Debug.Log("回復");
    }
}
