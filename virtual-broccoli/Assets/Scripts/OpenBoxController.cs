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
    private Quaternion _startRotation;
    private float _endRotation;
    private float _currentRotation;
    private Vector3 _stepVektor;
    private Vector3 _rotateVektor;
    private enum STATE {FORWARD, BACKWARD};
    private STATE _activeState;

    private enum BLOCKED { TOP, BOT, NOT };
    private BLOCKED _blocked;

    private void Start()
    {
        /*_fullRotation = 360f;
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
        _rotateVektor = new Vector3(0, 0, 0);*/

        _startRotation = transform.localRotation;
        _blocked = BLOCKED.NOT;
    }

    public void setOpen(bool open)
    {
        _rotate = open;
    }

    public void callBlocked(string trigger)
    {
        switch (trigger)
        {
            case "topTrigger":
                _blocked = BLOCKED.TOP;
                break;

            case "botTrigger":
                _blocked = BLOCKED.BOT;
                break;

            default:
                _blocked = BLOCKED.NOT;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*_currentRotation = Mathf.Round(transform.localEulerAngles.z);
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

        }*/

        var step = _speed * Time.deltaTime;

        switch (_blocked)
        {
            case BLOCKED.NOT:

                if (_rotate)
                {
                    transform.localRotation = Quaternion.RotateTowards(transform.localRotation, _startRotation * Quaternion.AngleAxis(_rotation.z, Vector3.forward), step);
                }
                else
                {
                    transform.localRotation = Quaternion.RotateTowards(transform.localRotation, _startRotation, step);
                }
                break;

            case BLOCKED.TOP:

                if (!_rotate)
                {
                    transform.localRotation = Quaternion.RotateTowards(transform.localRotation, _startRotation, step);
                }
                break;

            case BLOCKED.BOT:
            default:

                break;
        }
    }
}
