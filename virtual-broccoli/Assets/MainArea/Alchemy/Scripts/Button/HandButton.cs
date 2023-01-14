using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;

public class HandButton : XRBaseInteractable
{
    [SerializeField]
    private UnityEvent OnPress = null;
    [SerializeField]
    private UnityEvent OnRelease = null;

    private float _yMin = 0.0f;
    private float _yMax = 0.0f;
    private bool _previousPress = false;

    private float _previousHandHeight = 0.0f;
    private IXRHoverInteractor _hoverInteractor = null;

    protected override void Awake()
    {
        base.Awake();
        hoverEntered.AddListener(StartPress);
        hoverExited.AddListener(EndPress);
    }

    private void Start()
    {
        SetMinMax();
    }

    private void OnDestory()
    {
        hoverEntered.RemoveListener(StartPress);
        hoverExited.RemoveListener(EndPress);
    }

    private void StartPress(HoverEnterEventArgs args)
    {
        _hoverInteractor = args.interactorObject;
        _previousHandHeight = GetLocalYPosition(_hoverInteractor.transform.position);
    }

    private void EndPress(HoverExitEventArgs args)
    {
        _hoverInteractor = null;
        _previousHandHeight = 0.0f;

        _previousPress = false;
        SetYPosition(_yMax);
        if (OnRelease != null) OnRelease.Invoke();
    }

    private void SetMinMax()
    {
        Collider collider = GetComponent<Collider>();
        Vector3 localBounds = transform.InverseTransformVector(collider.bounds.size);

        _yMin = transform.localPosition.y - (Mathf.Abs(localBounds.y) * 0.75f);
        _yMax = transform.localPosition.y;
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        if (_hoverInteractor != null)
        {
            float newHandHeight = GetLocalYPosition(_hoverInteractor.transform.position);
            float handDifference = _previousHandHeight - newHandHeight;

            _previousHandHeight = newHandHeight;

            float newPosition = transform.localPosition.y - handDifference;
            SetYPosition(newPosition);

            CheckPress();
        }
    }

    private float GetLocalYPosition(Vector3 position)
    {
        Vector3 localPosition = transform.InverseTransformVector(position);
        return localPosition.y;
    } 

    private void SetYPosition(float position)
    {
        Vector3 newPosition = transform.localPosition;
        newPosition.y = Mathf.Clamp(position, _yMin, _yMax);
        transform.localPosition = newPosition;
    }

    private void CheckPress()
    {
        bool inPosition = InPosition();

        if (inPosition && inPosition != _previousPress)
        {
            OnPress.Invoke();
        }
        _previousPress = inPosition;
    }

    private bool InPosition()
    {
        float inRange = Mathf.Clamp(transform.localPosition.y, _yMin, _yMin + 0.01f);

        return transform.localPosition.y == inRange;
    }
}
