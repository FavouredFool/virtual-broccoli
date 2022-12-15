using UnityEngine;

public class FaithPlateDebug : MonoBehaviour
{
    [SerializeField, Range(0, 1)] private float percentage = 0.0f;
    [SerializeField, Range(1, 15)] private int sampleSize = 10;

    private void OnDrawGizmos()
    {
        if(TryGetComponent(out FaithPlate faithPlate))
        {
            DrawCurve(faithPlate);
            DrawPercentage(faithPlate);
        }
    }

    private void DrawCurve(FaithPlate faithPlate)
    {
        for (int i = 0; i <= sampleSize; i++)
        {
            float samplePercentage = (float)i / sampleSize;

            Gizmos.color = Color.white;
            Gizmos.DrawSphere(faithPlate.FindCurvePosition(samplePercentage), 0.1f);
        }
    }

    private void DrawPercentage(FaithPlate faithPlate)
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(faithPlate.FindCurvePosition(percentage), 0.15f);
    }
}
