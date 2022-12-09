using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ColorMachine;

public class AwaitCrystalState : State
{
    public AwaitCrystalState(ColorMachine colorMachine) : base(colorMachine)
    {
    }

    public override void Start()
    {       
    }

    public override void Update()
    {
        if (_colorMachine.GetActiveCrystal() != CrystalColor.NONE)
        {
            _colorMachine.SetState(new ProcessCrystalState(_colorMachine));
        }
    }
}
