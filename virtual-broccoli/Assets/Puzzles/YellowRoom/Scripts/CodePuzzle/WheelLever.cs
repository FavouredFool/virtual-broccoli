using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class WheelLever : MonoBehaviour
{
    [Header("Wheel settings")]
    [SerializeField] private GameObject _wheelPart;
    [SerializeField] private int _wheelRotationStep;
    [SerializeField] private float _wheelRotationUpdate = 0.5f;
    [SerializeField] private int _delayTimeInSeconds;
    private int _blockedPeriodEnd = 0;
    private bool _rotating;
    [SerializeField] private float _rotationEnd;

    [Header("Lever settings")]
    [SerializeField] private GameObject _movableLever;
    [SerializeField] private XRBaseInteractable handle = null;
    [SerializeField] private Transform start = null;
    [SerializeField] private Transform end = null;

    private Vector3 grabPosition = Vector3.zero;
    private Vector3 _rotatingAxis;
    private float startingPercentage;
    private float currentPercentage;
    private float _prevPercentage;



    public void Start()
    {
        currentPercentage = _prevPercentage = startingPercentage = 0.0f;

        _rotatingAxis = transform.TransformDirection(Vector3.up);
        _rotatingAxis = new Vector3(Mathf.Round(_rotatingAxis.x),
             Mathf.Round(_rotatingAxis.y),
             Mathf.Round(_rotatingAxis.z));

        SetLeverVisual();

        _rotationEnd = _wheelPart.transform.localEulerAngles.y;
    }

    protected virtual void OnEnable()
    {
        handle.selectEntered.AddListener(StoreGrabInfo);
    }

    protected virtual void OnDisable()
    {
        handle.selectEntered.RemoveListener(StoreGrabInfo);
    }

    private void StoreGrabInfo(SelectEnterEventArgs args)
    {
        startingPercentage = currentPercentage;

        /*Store starting position for pull direction*/
        grabPosition = args.interactorObject.transform.position;
    }

    public void Update()
    {
        if (handle.isSelected)
        {
            UpdateLever();
            if (currentPercentage == 1 && _prevPercentage != 1)
            {
                _prevPercentage = 1;

                _rotationEnd = (_rotationEnd + _wheelRotationStep) % 360;

                if (!_rotating)
                {
                    _blockedPeriodEnd = Mathf.FloorToInt(Time.time + _delayTimeInSeconds);
                }
            }
        }
        else if (currentPercentage != 0)
        {
            _prevPercentage = currentPercentage;
            float newPercentage = currentPercentage - 0.05f;

            Quaternion setRotation = Quaternion.Slerp(
                Quaternion.Euler(new Vector3(0f, 0f, 45f)),
                Quaternion.Euler(new Vector3(0f, 0f, -45f)),
                newPercentage);
            _movableLever.transform.localRotation = setRotation;

            currentPercentage = Mathf.Clamp01(newPercentage);
        }

        //wait for delay
        if (Time.time >= _blockedPeriodEnd)
        {
            if (Mathf.RoundToInt(_wheelPart.transform.localEulerAngles.y) != _rotationEnd)
            {
                _wheelPart.transform.rotation *= Quaternion.Euler(_wheelRotationUpdate * _rotatingAxis);
                foreach (Transform symbol in _wheelPart.transform)
                {
                    symbol.rotation *= Quaternion.Euler(-_wheelRotationUpdate * _rotatingAxis);
                }
            } else if (_rotating)
            {
                _rotating = false;
            }
        }

    }

    private void UpdateLever()
    {
        float newPercentage = startingPercentage + FindPercentageDifference();

        Quaternion setRotation = Quaternion.Slerp(Quaternion.Euler(new Vector3(0f, 0f, 45f)), Quaternion.Euler(new Vector3(0f, 0f, -45f)), newPercentage);

        _movableLever.transform.localRotation = setRotation;

        currentPercentage = Mathf.Clamp01(newPercentage);
    }

    private float FindPercentageDifference()
    {
        /*Find the directions for the player's pull direction and the target direction.*/
        Vector3 handPosition = handle.GetOldestInteractorSelecting().transform.position;
        Vector3 pullDirection = handPosition - grabPosition;
        Vector3 targetDirection = end.position - start.position;

        /*Store length, then normalize target direction for use in Vector3.Dot*/
        float length = targetDirection.magnitude;
        targetDirection.Normalize();

        /*Typically, we use two normalized vectors for Vector3.Dot. We'll be leaving one of the 
        directions with its original length. Thus, we get an actual combination of the distance/direction 
        similarity, then dividing by the length, gives us a value between 0 and 1.*/
        return Vector3.Dot(pullDirection, targetDirection) / length;
    }

    private void SetLeverVisual()
    {
        int rotationZ = currentPercentage == 0.0f ? 45 : -45;
        _movableLever.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, rotationZ));
    }

    private void OnDrawGizmos()
    {
        /*Shows the general direction of the interaction*/
        if (start && end)
        {
            Gizmos.DrawLine(start.position, end.position);
        }
    }
}
