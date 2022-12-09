using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ColorMachine;

public class MixingState : State
{
    public MixingState(ColorMachine colorMachine) : base(colorMachine)
    {
    }

    public override void Start()
    {
        _colorMachine.SetResetMix(false);
    }

    public override void Update()
    {
        if (_colorMachine.GetResetMix())
        {
            _colorMachine.SetResetMix(false);
            _colorMachine.SetState(new ResetPuzzleState(_colorMachine));
        }

        if (_colorMachine.GetCauldron().GetActiceCauldronColor() == _colorMachine.GetActiveCrystalInstruction().GetGoalColors()[_colorMachine.GetMixIteration()])
        {
            _colorMachine.SetMixIteration(_colorMachine.GetMixIteration() + 1);

            _colorMachine.SetState(new FreeColorState(_colorMachine));
        }

    }

}
