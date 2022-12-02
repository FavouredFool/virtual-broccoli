using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenBoxController : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> walls;
    [SerializeField]
    private bool _open;
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _gap;
    [SerializeField]
    private Vector3 _rotation;

    private float _fullRotation;
    private float _endRotation;
    private float _currentRotation;
    private Vector3 _rotateVektor;

    private void Start()
    {
        _fullRotation = 360;
        if(_rotation.z > 0)
        {
            _endRotation = _fullRotation - _rotation.z;
        } else
        {
            _endRotation = _rotation.z + _fullRotation;
        }

        _rotateVektor = new Vector3(0, 0, 0);
    }

    public void setOpen(bool open)
    {
        _open = open;
    }

    // Update is called once per frame
    void Update()
    {
        _currentRotation = transform.localEulerAngles.z;
        _rotateVektor *= 0;
        if (_open)
        {
            if (_currentRotation < _gap || _currentRotation > _endRotation)
            {
                _rotateVektor = _rotation;
            }
        }
        else
        {
            if (_currentRotation > _gap)
            {
                _rotateVektor = -_rotation;
            }
        }

        this.transform.Rotate(_rotateVektor * Time.deltaTime * _speed);
    }
}
