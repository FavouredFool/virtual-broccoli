using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class VelocityController : ActionBasedController
{
    [SerializeField] private InputActionProperty velocityAction;

    public Vector3 Velocity { get; private set; } = Vector3.zero;

    protected override void UpdateTrackingInput(XRControllerState controllerState)
    {
        base.UpdateTrackingInput(controllerState);
        Velocity = velocityAction.action.ReadValue<Vector3>();
    }
}
