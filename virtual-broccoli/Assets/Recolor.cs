using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recolor : MonoBehaviour
{
    [SerializeField]
    [Range(-1,1)]
    private float Flowpoint;
    [SerializeField]
    private Material GreyScale;
    [SerializeField]
    private Material NormColor;

    void Update()
    {
        GreyScale.SetFloat("Edge", Flowpoint);
        NormColor.SetFloat("Edge", Flowpoint);
    }

}
