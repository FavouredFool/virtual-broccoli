using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LeapProvider : LocomotionProvider
{
    [SerializeField] private float duration = 5.0f;
    [SerializeField] private float delay = 0.5f;

    private XROrigin origin => system.xrOrigin;

    public Vector3 CurvePosition { get; private set; } = Vector3.zero;

    public void TryLeap(FaithPlate faithPlate)
    {
        // If we can begin, send the tween
        if (CanBeginLocomotion())
            TweenLeap();

        // Use the 0 - 1 value from the tween to find curve position
        void ApplyValue(float percentage)
        {
            CurvePosition = faithPlate.FindCurvePosition(percentage);
            origin.MoveCameraToWorldLocation(FindCameraPosition() + CurvePosition);
        }

        // Let the system know when we're leaping     
        void TweenLeap() => TweenExtension.FaithCurve(BeginLeap, EndLeap, ApplyValue, duration, delay);
        void BeginLeap() => BeginLocomotion();
        void EndLeap() => EndLocomotion();
    }

    private Vector3 FindCameraPosition()
    {
        return origin.transform.up * origin.CameraInOriginSpaceHeight;
    }
}
