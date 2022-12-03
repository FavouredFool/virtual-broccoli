using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ColorMachineSequence;

public class KeyInstruction : CrystalInstruction
{
    private readonly List<Vector4> _goalColors = new()
    {
        new Vector4(0, 0, 0, 1),
        new Vector4(0, 0, 0, 0.5f),
        new Vector4(0, 0, 0, 0.75f)
    };

    public override List<Vector4> GetGoalColors()
    {
        return _goalColors;
    }
}
