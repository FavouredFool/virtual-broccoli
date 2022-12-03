using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ColorMachineSequence;

public class YellowInstruction : CrystalInstruction
{


    private readonly List<Vector4> _goalColors = new()
    {
        new Vector4(0, 0, 1, 0),
        new Vector4(0, 0, 0, 0),
        new Vector4(0, 0, 0, 1)
    };

    public override List<Vector4> GetGoalColors()
    {
        return _goalColors;
    }
}
