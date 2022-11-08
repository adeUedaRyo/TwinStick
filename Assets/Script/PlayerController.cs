using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    int _skillIndex = 0;
    [SerializeField] float _speed = 5;      //���݂̈ړ����x
    [SerializeField] float _defaultSpeed = 5; //�ړ����x�̏����l
    [SerializeField] float _dashSpeed = 10; //�ړ����x�̏����l
    [SerializeField] float _jumpPower = 3;  //�W�����v��
    [SerializeField] bool _left = true;    //���̃L�������ǂ���
    [SerializeField] int _avoid = 3;//����̑��x
    [SerializeField] float _avoidCoolTime = 1.5f; //����̃N�[���^�C��


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
            Debug.Log("�Q�[���p�b�h������܂���B");
            return;
        }

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
                Debug.Log("��");
            }
            else if (gamepad.leftTrigger.wasPressedThisFrame && _skillIndex == 1)
            {
                Debug.Log("�_�b�V��");
                _rb.AddForce(transform.forward * _avoid,ForceMode.Impulse);
                _speed = _dashSpeed;
            }
            else if (gamepad.leftTrigger.wasPressedThisFrame && _skillIndex == 2)
            {
                Debug.Log("�U��");
            }
            else if (gamepad.leftTrigger.isPressed && _skillIndex == 3)
            {
                Debug.Log("�K�[�h");
            }
            else if (gamepad.leftTrigger.wasReleasedThisFrame)
            {
                _speed = _defaultSpeed;
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
        }

        // �ړ�
        if (_stickValue != new Vector2(0, 0))
        {
            var dir = new Vector3(_stickValue.x, 0, _stickValue.y);
            transform.localRotation = Quaternion.LookRotation(dir);
            transform.Translate(0, 0, _speed * Time.deltaTime);

        }
    }
}
