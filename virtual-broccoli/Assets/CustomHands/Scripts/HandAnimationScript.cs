using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandAnimationScript : MonoBehaviour
{

    [SerializeField] InputActionReference gripInputAction;
    [SerializeField] InputActionReference triggerInputAction;

    Animator handAnimator;

    private void Awake()
    {
        handAnimator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        gripInputAction.action.performed += GripPressed;
        triggerInputAction.action.performed += TriggerPressed;
    }

    private void OnDisable()
    {
        gripInputAction.action.performed -= GripPressed;
        triggerInputAction.action.performed -= TriggerPressed;
    }

    private void TriggerPressed(InputAction.CallbackContext context)
    {
        handAnimator.SetFloat("Trigger", context.ReadValue<float>());
    }
    private void GripPressed(InputAction.CallbackContext context)
    {
        handAnimator.SetFloat("Grip", context.ReadValue<float>());
    }

}
