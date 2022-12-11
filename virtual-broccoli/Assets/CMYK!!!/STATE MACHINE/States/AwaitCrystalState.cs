using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ColorMachine;

public class AwaitCrystalState : State
{
    public AwaitCrystalState(ColorMachine colorMachine) : base(colorMachine)
    {
    }

    public override void StartState()
    {       
    }

    public override void UpdateState()
    {
        if (_colorMachine.GetActiveCrystal() != CrystalColor.NONE)
        {
            _colorMachine.SetState(new ProcessCrystalState(_colorMachine));
        }
    }
}
