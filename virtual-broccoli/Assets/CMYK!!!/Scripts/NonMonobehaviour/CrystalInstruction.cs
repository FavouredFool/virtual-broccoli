using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ColorMachineSequence;

public abstract class CrystalInstruction
{
    public abstract List<Vector4> GetGoalColors();
}
