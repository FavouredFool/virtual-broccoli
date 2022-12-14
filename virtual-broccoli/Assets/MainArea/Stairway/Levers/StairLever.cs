using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using static StairManager;

public class StairLever : MonoBehaviour
{
    [SerializeField] private bool _stairLeft;
    [SerializeField] private StairColor _color;
    [SerializeField] private GameObject _movableLever;
    [SerializeField] private MeshRenderer _markerRenderer;

    [SerializeField] private XRBaseInteractable handle = null;
    [SerializeField] private Transform start = null;
    [SerializeField] private Transform end = null;

    private Vector3 grabPosition = Vector3.zero;
    private float startingPercentage = 0.5f;
    private float currentPercentage = 0.0f;
    private float _prevPercentage = 0.0f;

    private StairManager _stairManager;

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

    public void Start()
    {
        if (!_stairLeft)
        {
            currentPercentage = 1.0f;
            _prevPercentage = 1.0f;
            startingPercentage = 1.0f;
        }
        else
        {
            currentPercentage = 0.0f;
            _prevPercentage = 0.0f;
            startingPercentage = 0.0f;
        }

        SetLeverVisual();
    }

    public void Update()
    {
        if (handle.isSelected)
        {
            UpdateLever();
        }

        // thumb down to left or right
        if (!handle.isSelected)
        {
            if (currentPercentage != 0 && currentPercentage != 1)
            {
                int fallSign;

                if (currentPercentage > 0.5f)
                {
                    fallSign = 1;
                }
                else
                {
                    fallSign = -1;
                }


                float newPercentage = currentPercentage + fallSign * 0.01f;

                Quaternion setRotation = Quaternion.Slerp(Quaternion.Euler(new Vector3(0f, 0f, 45f)), Quaternion.Euler(new Vector3(0f, 0f, -45f)), newPercentage);
                _movableLever.transform.localRotation = setRotation;

                currentPercentage = Mathf.Clamp01(newPercentage);
            }
        }

        if (currentPercentage == 1)
        {
            if (_prevPercentage != 1)
            {
                _prevPercentage = 1;
                ChangeStairPositions();
            }
        }
        else if (currentPercentage == 0)
        {
            if (_prevPercentage != 0)
            {
                _prevPercentage = 0;
                ChangeStairPositions();
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



    private void ChangeStairPositions()
    {
        if (!_stairManager)
        {
            return;
        }

        if (_markerRenderer.material.color == Color.white)
        {
            return;
        }

        foreach (StairRotationScript stair in _stairManager.GetAllStairs())
        {
            if (stair.GetFirstColor() == _color || stair.GetSecondColor() == _color)
            {
                stair.ChangeRotation();
            }
        }
    }

    private void SetLeverVisual()
    {
        Quaternion rotation;

        if (currentPercentage == 0.0f)
        {
            rotation = Quaternion.Euler(new Vector3(0f, 0f, 45f));
        }
        else
        {
            rotation = Quaternion.Euler(new Vector3(0f, 0f, -45f));
        }

        _movableLever.transform.localRotation = rotation;
    }

    public void SetStairManager(StairManager stairManager)
    {
        _stairManager = stairManager;
    }

    private void OnDrawGizmos()
    {
        /*Shows the general direction of the interaction*/
        if (start && end)
            Gizmos.DrawLine(start.position, end.position);
    }


}
