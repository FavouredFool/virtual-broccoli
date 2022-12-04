using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ColorMachineSequence;

public class MagentaInstruction : CrystalInstruction
{
    private readonly List<Vector4> _goalColors = new()
    {
        new Vector4(0f, 1f, 0f, 0f),
        new Vector4(0.375f, 0.375f, 0f, 0f),
        new Vector4(0.25f, 0.625f, 0f, 0f),
        new Vector4(0.375f, 0.125f, 0f, 0.5f)
    };

    public override List<Vector4> GetGoalColors()
    {
        return _goalColors;
    }
}
