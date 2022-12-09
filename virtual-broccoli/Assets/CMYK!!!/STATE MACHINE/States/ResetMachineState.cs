using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ColorMachine;

public class ResetMachineState : State
{
    public ResetMachineState(ColorMachine colorMachine) : base(colorMachine)
    {
    }

    public override void Start()
    {
        _colorMachine.SetActiveCrystal(CrystalColor.NONE);

        _colorMachine.SetMixIteration(1);

        _colorMachine.GetScreen().ResetColorAndText();

        _colorMachine.GetCauldron().SetCauldronColorCMYK(new Vector4(0, 0, 0, 0));

        _colorMachine.GetSlot().ResetSlot();

        _colorMachine.SetState(new AwaitCrystalState(_colorMachine));
    }

    public override void Update()
    {

    }
}
