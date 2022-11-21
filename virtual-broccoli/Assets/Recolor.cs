using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recolor : MonoBehaviour
{
    [SerializeField]
    [Range(0,1)]
    private float Flowpoint;
    Material mat;
    float meshSize;
    float diff;

    private void Start()
    {
        mat = this.gameObject.GetComponent<Renderer>().material;
        Vector3 axisSelectionVector = new Vector3(mat.GetInt("_xAxis"), mat.GetInt("_yAxis"), mat.GetInt("_zAxis"));

        Bounds meshBounds = this.gameObject.GetComponent<MeshRenderer>().localBounds;
        Vector3 diffVector = meshBounds.center;
        Vector3 meshSizeVector = meshBounds.size;

        Debug.Log("min: " + meshBounds.min);
        Debug.Log("max: " + meshBounds.max);
        Debug.Log("center: " + meshBounds.center);
        Debug.Log("meshSize: " + meshBounds.size);

        // Skalarproduct, um Abweichung (Verschiebung des Center-punkts) auf entsprechend ausgewählter Achse festzustellen
        diff = this.Scalar(axisSelectionVector, diffVector);

        // Skalarproduct, um Abweichung (Verschiebung des Center-punkts) auf entsprechend ausgewählter Achse festzustellen
        meshSize = this.Scalar(axisSelectionVector, meshSizeVector);
        Debug.Log("Size: " + meshSize);
    }

    private float Scalar (Vector3 vecA, Vector3 vecB)
    {
        Vector3 multVector = Vector3.Scale(vecA, vecB);
        float scalar = (multVector.x + multVector.y + multVector.z);
        return scalar;
    }

    void Update()
    {
        
        mat.SetFloat("_Edge", ((Flowpoint * meshSize)/2 + (1-Flowpoint)*diff));
    }

}
