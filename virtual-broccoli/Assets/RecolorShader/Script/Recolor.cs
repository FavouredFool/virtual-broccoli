using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recolor : MonoBehaviour
{
    const float endPoint = 1.5f;
    const float startPoint = -0.1f;
    private float currentStart;
    private float currentEnd;
    [SerializeField]
    [Range(startPoint, endPoint)]
    private float flowPoint = startPoint;
    [SerializeField]
    private float flowStep = 0.0015f;
    Material mat;
    float meshSize;
    float diff;
    [SerializeField]
    bool forward;
    [SerializeField]
    bool backward;
    bool inverted;

    private void Start()
    {
        
        mat = this.gameObject.GetComponent<Renderer>().material;
        Vector3 axisSelectionVector = new Vector3(mat.GetInt("_xAxis"), mat.GetInt("_yAxis"), mat.GetInt("_zAxis"));
        inverted = (mat.GetInt("_inverted") > 0);

        Bounds meshBounds = this.gameObject.GetComponent<MeshRenderer>().localBounds;
        Vector3 diffVector = meshBounds.center;
        Vector3 meshSizeVector = meshBounds.size;

        // Skalarproduct, um Abweichung (Verschiebung des Center-punkts) auf entsprechend ausgewählter Achse festzustellen
        diff = this.Scalar(axisSelectionVector, diffVector);

        // Skalarproduct, um Abweichung (Verschiebung des Center-punkts) auf entsprechend ausgewählter Achse festzustellen
        meshSize = this.Scalar(axisSelectionVector, meshSizeVector);

    }

    private float Scalar (Vector3 vecA, Vector3 vecB)
    {
        Vector3 multVector = Vector3.Scale(vecA, vecB);
        float scalar = (multVector.x + multVector.y + multVector.z);
        return scalar;
    }

    void Update()
    {
        
        mat.SetFloat("_Edge", ((flowPoint * meshSize)/2 + (1-flowPoint)*diff));

        if(!inverted)
        {
            if (flowPoint <= endPoint && forward) flowPoint += flowStep;
            if (flowPoint >= startPoint && backward) flowPoint -= flowStep;
        } else
        {
            if (flowPoint <= endPoint && backward) flowPoint += flowStep;
            if (flowPoint >= startPoint && forward) flowPoint -= flowStep;
        }

        
    }

}
