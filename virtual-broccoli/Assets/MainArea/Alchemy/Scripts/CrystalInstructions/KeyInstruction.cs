using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInstruction : CrystalInstruction
{
    private readonly List<Vector4> _goalColors = new()
    {
        new Vector4(0f, 0f, 0f, 1f),
        new Vector4(0f, 0f, 0f, 0.5f),
        new Vector4(0f, 0f, 0f, 0.25f),
        new Vector4(0f, 0f, 0f, 0.625f)
    };

    public override List<Vector4> GetGoalColors()
    {
        return _goalColors;
    }
}
