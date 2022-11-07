using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float _speed = 5;      //�ړ����x
    [SerializeField] float _jumpPower = 3;  //�W�����v��
    [SerializeField] bool _left = true;    //���̃L�������ǂ���
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

            //�U��
            if (gamepad.leftTrigger.wasPressedThisFrame) _attack.Attack();

            //�X�L���ύX
            if (gamepad.dpad.up.wasPressedThisFrame) _attack.SkillChange(0); //�����L�[��
            if (gamepad.dpad.right.wasPressedThisFrame) _attack.SkillChange(1); //�����L�[�E
            if (gamepad.dpad.left.wasPressedThisFrame) _attack.SkillChange(2); //�����L�[��
            if (gamepad.dpad.down.wasPressedThisFrame) _attack.SkillChange(3); //�����L�[��
        }
        else//�E
        {
            //�X�e�B�b�N���͂̌��o
            _stickValue = gamepad.rightStick.ReadValue().normalized;

            //�W�����v
            if (gamepad.rightShoulder.wasPressedThisFrame) _rb.AddForce(0, _jumpPower, 0, ForceMode.Impulse);


            //�U��
            if (gamepad.rightTrigger.wasPressedThisFrame) _attack.Attack();

            //�X�L���ύX
            if (gamepad.buttonNorth.wasPressedThisFrame) _attack.SkillChange(0); //PS���Ɓ�
            if (gamepad.buttonWest.wasPressedThisFrame) _attack.SkillChange(1); //PS���Ɓ�
            if (gamepad.buttonEast.wasPressedThisFrame) _attack.SkillChange(2); //PS���Ɓ�
            if (gamepad.buttonSouth.wasPressedThisFrame) _attack.SkillChange(3); //PS���Ɓ~
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
