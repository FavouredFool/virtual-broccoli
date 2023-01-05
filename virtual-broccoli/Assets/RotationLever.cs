using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RotationLever : MonoBehaviour
{
    [SerializeField] private GameObject _movableLever;
    [SerializeField] private GameObject movingBox;

    [SerializeField] private XRBaseInteractable handle = null;
    [SerializeField] private Transform start = null;
    [SerializeField] private Transform end = null;
    [SerializeField] private bool xAxis = false;
    //[SerializeField] private bool zAxisRot = false;

    private Vector3 grabPosition = Vector3.zero;
    private float startingPercentage = 0.5f;
    private float currentPercentage = 0.0f;

    private bool _rotate = false;

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
        SetLeverVisual();
    }

    public void Update()
    {
        if (handle.isSelected)
        {
            UpdateLever();
            _rotate = true;
        }

        // thumb down to left or right
        if (!handle.isSelected)
        {
            if (currentPercentage != 0.5f)
            {
                int fallSign;

                if (currentPercentage > 0.5f)
                {
                    fallSign = -1;
                }
                else
                {
                    fallSign = 1;
                }


                float newPercentage = currentPercentage + fallSign * 0.01f;

                Quaternion setRotation = Quaternion.Slerp(Quaternion.Euler(new Vector3(0f, 0f, 45f)), Quaternion.Euler(new Vector3(0f, 0f, -45f)), newPercentage);
                _movableLever.transform.localRotation = setRotation;

                currentPercentage = Mathf.Round(Mathf.Clamp01(newPercentage) * 100f) / 100f;
            } else
            {
                _rotate = false;
            }
        }

        if (_rotate) RotateBox();
    }

    private void UpdateLever()
    {
        float newPercentage = startingPercentage + FindPercentageDifference();
        Debug.Log(newPercentage);

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



    private void RotateBox()
    {
        Vector3 boxRot = movingBox.transform.localRotation.eulerAngles;
        float leverRotation = _movableLever.transform.eulerAngles.z;
        Quaternion newRotation = Quaternion.Euler(Vector3.zero);
        Debug.Log(xAxis);
        if (xAxis)
        {
            newRotation = Quaternion.Euler(new Vector3(-leverRotation, boxRot.y, boxRot.z));
        } else
        {
            newRotation = Quaternion.Euler(new Vector3(boxRot.x, boxRot.y, -leverRotation));
        }
        movingBox.transform.localRotation = newRotation;
    }

    private void SetLeverVisual()
    {
        Quaternion rotation;

        switch (currentPercentage)
        {
            case 0.0f:
                rotation = Quaternion.Euler(new Vector3(0f, 0f, 45f));
                break;
            case 0.5f:
                rotation = Quaternion.Euler(new Vector3(0f, 0f, 0));
                break;
            case 1.0f:
                rotation = Quaternion.Euler(new Vector3(0f, 0f, -45));
                break;
            default:
                rotation = Quaternion.Euler(new Vector3(0f, 0f, 0));
                break;
        }

        _movableLever.transform.localRotation = rotation;
    }

    private void OnDrawGizmos()
    {
        /*Shows the general direction of the interaction*/
        if (start && end)
            Gizmos.DrawLine(start.position, end.position);
    }
}
