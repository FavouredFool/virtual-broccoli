using UnityEngine;

public class OpenBoxController : MonoBehaviour
{
    [SerializeField]
    private bool _rotate;
    [SerializeField]
    private float _speed;
    [SerializeField]
    private Vector3 _rotation;

    private Quaternion _startRotation;

    private enum STATE { OPEN, CLOSING, CLOSED};
    private STATE _state;

    private void Start()
    {
        _startRotation = transform.localRotation;
    }

    public void setOpen(bool open)
    {
        _rotate = open;
    }

    // Update is called once per frame
    void Update()
    {
        var step = _speed * Time.deltaTime;
        Quaternion localRotation = transform.localRotation;
        /*switch (_blocked)
        {
            case BLOCKED.NOT:*/

                if (_rotate)
                {
                    transform.localRotation = Quaternion.RotateTowards(localRotation, _startRotation * Quaternion.AngleAxis(_rotation.z, Vector3.forward), step);
                }
                else
                {
                    transform.localRotation = Quaternion.RotateTowards(localRotation, _startRotation, step);
                }
                /*break;

            case BLOCKED.TOP:

                if (!_rotate)
                {
                    transform.localRotation = Quaternion.RotateTowards(localRotation, _startRotation, step);
                }
                break;

            case BLOCKED.BOT:
            default:

                break;
        }*/



        switch (_state)
        {
            case STATE.OPEN:
                if (localRotation == _startRotation) _state = STATE.CLOSED;
                break;

            /* STATE.CLOSING:
                _closing = true;
                _state = STATE.CLOSED;
                break;*/

            case STATE.CLOSED:
                //_closing = false;
                if (localRotation != _startRotation) _state = STATE.OPEN;
                break;

            default:
                break;
        }
    }

    /*public bool getClosing()
    {
        return _closing;
    }*/

    public bool getClosed()
    {
        return _state == STATE.CLOSED;
    }
}
