using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleMatching : MonoBehaviour
{
    public Transform schablone;
    public Transform moveable;

    public float matchRate = 0.9f;

    void Update()
    {
        float dotUp = Vector3.Dot(schablone.up, moveable.up);
        float dotRight = Vector3.Dot(schablone.right, moveable.right);

        if (Input.GetKey(KeyCode.F))
        {
           Debug.Log("dotRight: " + dotRight + ", dotUp: " + dotUp);
        }
        if (dotRight >= matchRate && dotUp >= matchRate)
        {
            Debug.Log("Passt ;)");
        }
    }
}
