using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static StairManager;

public class StairRotation : MonoBehaviour
{
    [SerializeField]
    private Rotation _goalRotationEnum;

    [SerializeField]
    private float _speed = 0.1f;

    [SerializeField]
    private GameObject _stairVisual;

    [SerializeField]
    private StairColor _firstColor;

    [SerializeField]
    private StairColor _secondColor;

    [SerializeField]
    private List<MeshRenderer> _leftMarkers;

    [SerializeField]
    private List<MeshRenderer> _rightMarkers;

    [SerializeField]
    private List<MeshRenderer> _stairSegments;

    private StairManager _stairManager;

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

    public void Start()
    {
        foreach(MeshRenderer renderer in _rightMarkers)
        {
            renderer.sharedMaterial = _stairManager.StairColorToMaterial(_firstColor);
        }

        foreach (MeshRenderer renderer in _leftMarkers)
        {
            renderer.sharedMaterial = _stairManager.StairColorToMaterial(_secondColor);
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

        _stairVisual.transform.localRotation = Quaternion.Slerp(Quaternion.Euler(0, -45, 0), Quaternion.Euler(0, 45, 0), _t);
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

    public StairColor GetFirstColor()
    {
        return _firstColor;
    }

    public StairColor GetSecondColor()
    {
        return _secondColor;
    }

    public void SetStairManager(StairManager stairManager)
    {
        _stairManager = stairManager;
    }
}
