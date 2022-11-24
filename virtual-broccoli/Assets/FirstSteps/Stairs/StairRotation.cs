using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairRotation : MonoBehaviour
{
    public enum Rotation { RIGHT, LEFT };

    [SerializeField]
    private Rotation _goalRotationEnum;

    [SerializeField]
    private float _speed = 0.1f;

    private float _goalT;
    private float _t;


    public void Awake()
    {
        if (_goalRotationEnum == Rotation.RIGHT)
        {
            _t = 1f;
        }
        else
        {
            _t = 0f;
        }
    }

    public void Update()
    {
        if (_goalRotationEnum == Rotation.RIGHT)
        {
            _goalT = 1f;
        } else
        {
            _goalT = 0f;
        }

        _t = Mathf.SmoothStep(_t, _goalT, _speed);

        transform.localRotation = Quaternion.Slerp(Quaternion.Euler(0, -45, 0), Quaternion.Euler(0, 45, 0), _t);
    }

    public void ChangeRotation()
    {
        if (_goalRotationEnum == Rotation.RIGHT)
        {
            _goalRotationEnum = Rotation.LEFT;
        } else
        {
            _goalRotationEnum = Rotation.RIGHT;
        }
    }
}
