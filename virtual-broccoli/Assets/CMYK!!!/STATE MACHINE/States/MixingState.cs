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
        _colorMachine.SetGoalColor(_colorMachine.GetActiveCrystalInstruction().GetGoalColors()[_colorMachine.GetMixIteration()]);
    }

    public override void Update()
    {
        if (_colorMachine.GetResetMix())
        {
            _colorMachine.SetResetMix(false);
            _colorMachine.SetState(new ResetPuzzleState(_colorMachine));
        }

    }

}
