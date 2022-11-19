using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recolor : MonoBehaviour
{
    [SerializeField]
    [Range(-1.5f,1.5f)]
    private float Flowpoint;
    [SerializeField]
    private Material GreyScale;
    [SerializeField]
    private Material NormColor;

    private void Start()
    {
        //Minimum of Flowpoint
    }

    void Update()
    {
        GreyScale.SetFloat("_Edge", Flowpoint);
        NormColor.SetFloat("_Edge", Flowpoint);
    }

}
