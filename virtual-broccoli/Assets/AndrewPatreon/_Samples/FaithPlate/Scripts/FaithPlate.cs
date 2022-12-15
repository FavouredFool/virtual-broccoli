using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FaithPlate : TeleportationAnchor
{
    [Header("Faith Plate References")]
    [SerializeField] private LeapProvider leapProvider;

    [Header("Faith Plate Settings")]
    [SerializeField, Range(0, 10)] private float height = 10.0f;
    [SerializeField, Range(0, 10)] private float distance = 10.0f;

    [SerializeField] private AnimationCurve movementCurve;

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        leapProvider.TryLeap(this);
    }

    public Vector3 FindCurvePosition(float percentage)
    {
        // Find value of the curve
        float influence = movementCurve.Evaluate(percentage);

        // Apply height, and distance to the 0 - 1 value
        float pointHeight = height * influence;
        float pointDistance = distance * percentage;
        Vector3 curvePosition = new Vector3(0, pointHeight, pointDistance);

        // Position, and rotate curve based on the anchor's transform
        Matrix4x4 curveMatrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);
        return curveMatrix.MultiplyPoint(curvePosition);
    }
}
