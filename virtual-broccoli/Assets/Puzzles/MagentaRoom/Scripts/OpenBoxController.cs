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

    private enum STATE { OPEN, CLOSING, CLOSED };
    private STATE _state;

    private void Start()
    {
        _startRotation = transform.localRotation;
    }

    void Update()
    {
        var step = _speed * Time.deltaTime;
        Quaternion localRotation = transform.localRotation;

        if (_rotate)
        {
            transform.localRotation = Quaternion.RotateTowards(localRotation, _startRotation * Quaternion.AngleAxis(_rotation.z, Vector3.forward), step);
        }
        else
        {
            transform.localRotation = Quaternion.RotateTowards(localRotation, _startRotation, step);
        }


        switch (_state)
        {
            case STATE.OPEN:
                if (localRotation == _startRotation) _state = STATE.CLOSED;
                break;
            case STATE.CLOSED:
                if (localRotation != _startRotation) _state = STATE.OPEN;
                break;

            default:
                break;
        }
    }

    public bool GetClosed()
    {
        return _state == STATE.CLOSED;
    }

    public void SetOpen(bool open)
    {
        _rotate = open;
    }
}
