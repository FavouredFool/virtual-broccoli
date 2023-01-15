using UnityEngine;

public class RotationController : MonoBehaviour
{
    [SerializeField] private bool _rotate;
    [SerializeField] private bool _forward;
    [SerializeField] private float _speed;
    [SerializeField] private Vector3 _rotation;

    private Quaternion _startRotation;
    //private Quaternion _endRotation;

    private void Start()
    {
        _startRotation = transform.localRotation;
        _rotate = false;
    }

    public void setOpen(bool open)
    {
        _rotate = open;
        _startRotation = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        if(_rotate)
        {
            var step = _speed * Time.deltaTime;
            Quaternion localRotation = transform.localRotation;

            if (_forward)
            {
                transform.localRotation = Quaternion.RotateTowards(localRotation, _startRotation * Quaternion.Euler(_rotation), step);
            }
            else
            {
                transform.localRotation = Quaternion.RotateTowards(localRotation, _startRotation, step);
            }
        }
    }

    public bool GetRotationCheck()
    {
        return transform.localRotation == _startRotation * Quaternion.Euler(_rotation);
    }
        
}
