using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ColorMachine;

public class CrystalObject : MonoBehaviour
{
    [SerializeField]
    private CrystalColor _crystalColor;


    public CrystalColor GetCrystalColor()
    {
        return _crystalColor;
    }
}
