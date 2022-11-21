using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class BoundsDrawer : MonoBehaviour
{
    private void Start()
    {
        List<Vector3> points = new List<Vector3>();
        SetupBounds(points);
        DrawLine(points);
    }

    private void SetupBounds(List<Vector3> points)
    {
        // Get XR Input System
        List<XRInputSubsystem> inputSubsystems = new List<XRInputSubsystem>();
        SubsystemManager.GetInstances(inputSubsystems);

        foreach (XRInputSubsystem inputSubsystem in inputSubsystems)
        {
            if (inputSubsystem.TryGetBoundaryPoints(points))
                print("Boundary Points: Success");
        }

        // NOTE: This may not be necessary - May be returning dimensions
        for (int i = 0; i < points.Count; i++)
            points[i] *= 0.5f;
    }

    private void DrawLine(List<Vector3> points)
    {
        // Add first point to end to complete rectangle
        points.Add(points[0]);

        // Apply data to line renderer
        if (TryGetComponent(out LineRenderer lineRenderer))
        {
            lineRenderer.positionCount = points.Count;
            lineRenderer.SetPositions(points.ToArray());
        }
    }
}
