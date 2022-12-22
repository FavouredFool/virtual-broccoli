using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class RayToggle : MonoBehaviour
{
    [SerializeField]
    private InputActionReference activateReference = null;

    private XRRayInteractor rayInteractor = null;
    private bool isEnabled = false;

    private void Awake()
    {
        rayInteractor = GetComponent<XRRayInteractor>();
    }

    private void OnEnable()
    {
        activateReference.action.started += ToggleRay;
        activateReference.action.canceled += ToggleRay;
    }

    private void OnDisable()
    {
        activateReference.action.started -= ToggleRay;
        activateReference.action.canceled -= ToggleRay;
    }

    private void ToggleRay(InputAction.CallbackContext context)
    {
        isEnabled = context.control.IsPressed();
    }

    private void LateUpdate()
    {
        ApplyStatus();
    }

    private void ApplyStatus()
    {
        if(rayInteractor.enabled != isEnabled)
        {
            rayInteractor.enabled = isEnabled;
        }
    }
}
