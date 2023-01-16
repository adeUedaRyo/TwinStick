using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    int _skillIndex = 0;
    [SerializeField,Tooltip("�ړ����x")] float _speed = 5;      
    [SerializeField,Tooltip("�ړ����x�̏����l")] float _defaultSpeed = 5;
    [SerializeField,Tooltip("�_�b�V�����̈ړ����x")] float _dashSpeed = 10;
    [SerializeField,Tooltip("�W�����v��")] float _jumpPower = 3;  
    [SerializeField,Tooltip("���̃L�������ǂ���")] bool _left = true;
    [SerializeField,Tooltip("����̑��x")] int _avoid = 3;
    [SerializeField,Tooltip("����̃N�[���^�C��")] float _avoidCoolTime = 1f;
    [SerializeField,Tooltip("�U����")] int _attackPower = 5;
    [SerializeField,Tooltip("�U���̃N�[���^�C��")] float _attackCoolTime = 1.5f;
    [SerializeField,Tooltip("�o���A")] GameObject _barrier = null;
    [SerializeField,Tooltip("�e�i���ߖ����A�^����A�^�����j")] GameObject _bullet, _bulletA, _bulletB = null;
    [SerializeField,Tooltip("�񕜃G���A")] GameObject _recoveryArea = null;
    [SerializeField] Transform _muzzle = null;
    Rigidbody _rb = default;
    float _attackChargeTime = 0; // �U�������߂Ă��鎞��
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
            Debug.Log("�Q�[���p�b�h������܂���B");
            return;
        }

        _timeAvoid += Time.deltaTime;
        _timeAttack += Time.deltaTime;
        //�ړ�
        if (_left)//��
        {
            //�X�e�B�b�N���͂̌��o
            _stickValue = gamepad.leftStick.ReadValue().normalized;

            //�W�����v
            if (gamepad.leftShoulder.wasPressedThisFrame) _rb.AddForce(0, _jumpPower, 0, ForceMode.Impulse);

            

            //�X�L���ύX
            if (gamepad.dpad.up.wasPressedThisFrame) _skillIndex = 0; //�����L�[��
            if (gamepad.dpad.right.wasPressedThisFrame) _skillIndex = 1; //�����L�[�E
            if (gamepad.dpad.left.wasPressedThisFrame) _skillIndex = 2; //�����L�[��
            if (gamepad.dpad.down.wasPressedThisFrame) _skillIndex = 3; //�����L�[��

            if (gamepad.leftTrigger.wasPressedThisFrame && _skillIndex == 0)
            {
                Recovery();
            }
            else if (gamepad.leftTrigger.wasPressedThisFrame && _skillIndex == 1 )
            {
                if (_avoidCoolTime <= _timeAvoid)
                {
                    _rb.AddForce(transform.forward * _avoid, ForceMode.Impulse); // �N�[���^�C�����I����Ă�����������
                    _timeAvoid = 0;
                }
                    _speed = _dashSpeed; // �{�^���𗣂��܂ŃX�s�[�h�A�b�v
            }
            else if (gamepad.leftTrigger.isPressed && _skillIndex == 2 && _attackCoolTime <= _timeAvoid) // �U���𗭂߂�
            {
                _attackChargeTime += Time.deltaTime;
                _speed = _defaultSpeed / 5.0f;
            }
            else if (gamepad.leftTrigger.isPressed && _skillIndex == 3)
            {
                _barrier.SetActive(true);
            }
            else if (gamepad.leftTrigger.wasReleasedThisFrame)�@//�{�^���𗣂�����
            {
                if (_attackChargeTime != 0 && _skillIndex == 2) // �U�������߂Ă�����
                {
                    Attack();
                }

                _speed = _defaultSpeed; // ���̃X�s�[�h�ɖ߂�
                _attackChargeTime = 0;
                _timeAttack = 0;
                _barrier.SetActive(false);
            }
        }
        else//�E
        {
            //�X�e�B�b�N���͂̌��o
            _stickValue = gamepad.rightStick.ReadValue().normalized;

            //�W�����v
            if (gamepad.rightShoulder.wasPressedThisFrame) _rb.AddForce(0, _jumpPower, 0, ForceMode.Impulse);

            //�X�L���ύX
            if (gamepad.buttonNorth.wasPressedThisFrame) _skillIndex = 0; //PS���Ɓ�
            if (gamepad.buttonWest.wasPressedThisFrame) _skillIndex = 1; //PS���Ɓ�
            if (gamepad.buttonEast.wasPressedThisFrame) _skillIndex = 2; //PS���Ɓ�
            if (gamepad.buttonSouth.wasPressedThisFrame) _skillIndex = 3; //PS���Ɓ~

            if (gamepad.rightTrigger.wasPressedThisFrame && _skillIndex == 0)
            {
                Recovery();
            }
            else if (gamepad.rightTrigger.wasPressedThisFrame && _skillIndex == 1)
            {
                if (_avoidCoolTime <= _timeAvoid)
                {
                    _rb.AddForce(transform.forward * _avoid, ForceMode.Impulse); // �N�[���^�C�����I����Ă�����������
                    _timeAvoid = 0;
                }
                _speed = _dashSpeed; // �{�^���𗣂��܂ŃX�s�[�h�A�b�v
            }
            else if (gamepad.rightTrigger.isPressed && _skillIndex == 2 && _attackCoolTime <= _timeAvoid) // �U���𗭂߂�
            {
                _attackChargeTime += Time.deltaTime;
                _speed = _defaultSpeed / 5.0f;
            }
            else if (gamepad.rightTrigger.isPressed && _skillIndex == 3)
            {
                _barrier.SetActive(true);
            }
            else if (gamepad.rightTrigger.wasReleasedThisFrame)�@//�{�^���𗣂�����
            {
                if (_attackChargeTime != 0 && _skillIndex == 2) // �U�������߂Ă�����
                {
                    Attack();
                }

                _speed = _defaultSpeed; // ���̃X�s�[�h�ɖ߂�
                _attackChargeTime = 0;
                _timeAttack = 0;
                _barrier.SetActive(false);
            }
        }

        // �ړ�
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
            Debug.Log("�^���U��");
        }
        else if (_attackChargeTime >= 1f)
        {
            Instantiate(_bulletA, _muzzle.position, this.transform.rotation);
            Debug.Log("���^���U��");
        }
        else
        {
            Instantiate(_bullet, _muzzle.position, this.transform.rotation);
            Debug.Log("�U��");
        }
    }
    void Recovery()
    {
        Instantiate(_recoveryArea, new Vector3(this.transform.position.x, 0 , this.transform.position.z), this.transform.rotation);
        Debug.Log("��");
    }
}
