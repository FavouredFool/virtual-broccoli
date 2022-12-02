using UnityEngine;

public class OpenBoxController : MonoBehaviour
{
    [SerializeField]
    private bool _rotate;
    [SerializeField]
    private float _speed;
    [SerializeField]
    private Vector3 _rotation;

    private float _fullRotation;
    private float _startRotation;
    private float _endRotation;
    private float _currentRotation;
    private Vector3 _stepVektor;
    private Vector3 _rotateVektor;
    private enum STATE {FORWARD, BACKWARD};
    private STATE _activeState;

    private void Start()
    {
        _fullRotation = 360f;
        _startRotation = Mathf.Round(transform.localEulerAngles.z);

        if (_rotation.z > 0)
        {
            _endRotation = (_startRotation + _rotation.z);
            if (_endRotation > _fullRotation) _endRotation = _fullRotation - _endRotation;
            _activeState = STATE.FORWARD;
        } else
        {
            _endRotation = _startRotation + _rotation.z;
            if (_endRotation < 0) _endRotation = _fullRotation + _endRotation;
            _activeState = STATE.BACKWARD;
        }

        _stepVektor = new Vector3(0, 0, 1f);
        _rotateVektor = new Vector3(0, 0, 0);
    }

    public void setOpen(bool open)
    {
        _rotate = open;
    }

    // Update is called once per frame
    void Update()
    {
        _currentRotation = Mathf.Round(transform.localEulerAngles.z);
        _rotateVektor *= 0;


        switch (_activeState)
        {
            case STATE.FORWARD:
                if (_rotate)
                {
                    if(_startRotation > _endRotation)
                    {
                        if ((_currentRotation <= _fullRotation && _currentRotation >= _startRotation) || _currentRotation < _endRotation)
                        {
                            _rotateVektor = _stepVektor;
                        }
                        else
                        {
                            _rotateVektor = Vector3.zero;
                        }
                    } else
                    {
                        if (_currentRotation < _endRotation && _currentRotation >= _startRotation)
                        {
                            _rotateVektor = _stepVektor;
                        }
                        else
                        {
                            _rotateVektor = Vector3.zero;
                        }
                    }
                }
                else
                {
                    if (_startRotation > _endRotation)
                    {
                        if ((_currentRotation >= 0 && _currentRotation <= _endRotation) || _currentRotation > _startRotation)
                        {
                            _rotateVektor = -_stepVektor;
                        }
                        else
                        {
                            _rotateVektor = Vector3.zero;
                        }
                    }
                    else
                    {
                        if (_endRotation >= _currentRotation && _currentRotation > _startRotation)
                        {
                            _rotateVektor = -_stepVektor;
                        }
                        else
                        {
                            _rotateVektor = Vector3.zero;
                        }
                    }
                }
                
                break;

            case STATE.BACKWARD:
                if (_rotate)
                {
                    if (_startRotation > _endRotation)
                    {
                        if (_currentRotation <= _startRotation && _currentRotation > _endRotation)
                        {
                            _rotateVektor = -_stepVektor;
                        }
                        else
                        {
                            _rotateVektor = Vector3.zero;
                        }
                    }
                    else
                    {
                        if ((_currentRotation <= _startRotation && _currentRotation >= 0) || _currentRotation > _endRotation)
                        {
                            _rotateVektor = -_stepVektor;
                        }
                        else
                        {
                            _rotateVektor = Vector3.zero;
                        }
                    }
                }
                else
                {
                    if (_startRotation > _endRotation)
                    {
                        if (_currentRotation < _startRotation && _currentRotation >= _endRotation)
                        {
                            _rotateVektor = _stepVektor;
                        }
                        else
                        {
                            _rotateVektor = Vector3.zero;
                        }
                    }
                    else
                    {
                        if ((_currentRotation < _startRotation && _currentRotation >= 0) || _currentRotation >= _endRotation)
                        {
                            _rotateVektor = _stepVektor;
                        }
                        else
                        {
                            _rotateVektor = Vector3.zero;
                        }
                    }
                }
                break;

            default:
                _rotateVektor = Vector3.zero;
                break;

        }
        this.transform.Rotate(_rotateVektor);
    }
}
