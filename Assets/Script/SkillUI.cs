using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SkillUI : MonoBehaviour
{
    [SerializeField] Image _up, _down, _left, _right = null;
    [SerializeField] bool _isLeft = false;
    Color _upC, _downC, _leftC, _rightC = default;

    // Start is called before the first frame update
    void Start()
    {
        _upC = _up.color;
        _downC = _down.color;
        _leftC = _left.color;
        _rightC = _right.color;

        _up.color = Color.gray;
        _down.color = Color.gray;
        _left.color = Color.gray;
        _right.color = Color.gray;
    }
    public void ActiveSkill(int index)
    {
        if (index == 0)
        {
            _up.color = Color.gray;
            _down.color = Color.gray;
            _left.color = Color.gray;
            _right.color = Color.gray;
            _up.color = _upC;
        }
        else if (index == 3)
        {
            _up.color = Color.gray;
            _down.color = Color.gray;
            _left.color = Color.gray;
            _right.color = Color.gray;
            _down.color =_downC;
        }
        else if (index == 1)
        {
            _up.color = Color.gray;
            _down.color = Color.gray;
            _left.color = Color.gray;
            _right.color = Color.gray;
            _right.color = _rightC;
        }
        else if (index == 2)
        {
            _up.color = Color.gray;
            _down.color = Color.gray;
            _left.color = Color.gray;
            _right.color = Color.gray;
            _left.color = _leftC;
        }
    }
}
