using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ColorMachineSequence;

public class CyanInstruction : CrystalInstruction
{
    private readonly List<Vector4> _goalColors = new()
    {
        new Vector4(1f, 0f, 0f, 0f),
        new Vector4(0.75f, 0f, 0f, 0f),
        new Vector4(0.625f, 0f, 0f, 0.375f),
        new Vector4(0.125f, 0f, 0f, 0.125f)
    };

    public override List<Vector4> GetGoalColors()
    {
        return _goalColors;
    }
}
