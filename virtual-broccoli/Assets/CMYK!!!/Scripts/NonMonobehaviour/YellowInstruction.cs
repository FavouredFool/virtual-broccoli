using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowInstruction : CrystalInstruction
{


    private readonly List<Vector4> _goalColors = new()
    {
        new Vector4(0f, 0f, 1f, 0f),
        new Vector4(0f, 0.5f, 0.375f, 0.125f),
        new Vector4(0.25f, 0f, 0.375f, 0.125f),
        new Vector4(0.125f, 0.125f, 0.125f, 0.125f)
    };

    public override List<Vector4> GetGoalColors()
    {
        return _goalColors;
    }
}
