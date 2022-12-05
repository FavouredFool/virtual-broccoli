using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ColorMachineSequence;

public class CrystalObject : MonoBehaviour
{
    [SerializeField]
    private CrystalColor _crystalColor;


    public CrystalColor GetCrystalColor()
    {
        return _crystalColor;
    }
}
