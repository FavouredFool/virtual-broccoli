using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ColorMachine;

public class FreeColorState : State
{
    public FreeColorState(ColorMachine colorMachine) : base(colorMachine)
    {
    }

    private float _startTimeInSeconds;
    private bool _initialFree = true;

    public override void StartState()
    {
        _startTimeInSeconds = Time.time;

        int iteration = _colorMachine.GetMixIteration();

        _initialFree = _colorMachine.GetActiveCrystalInstruction().GetGoalColors().Count > iteration;

        _colorMachine.GetSmokeEmittor().ReleaseSmoke(_colorMachine.GetActiveCrystalInstruction().GetGoalColors()[0]);
    }

    public override void UpdateState()
    {
        if (Time.time - _startTimeInSeconds < _colorMachine.GetColorReleaseTime())
        {
            // PARTICLES
            return;
        }

        if (_initialFree)
        {
            _colorMachine.SetState(new ResetPuzzleState(_colorMachine));

        }
        else
        {
            _colorMachine.SetState(new ResetMachineState(_colorMachine));
        }
    }

    public override void EndState()
    {
        if (_initialFree)
        {
            _colorMachine.GetColorManager().ColorPrimaryMaterialOfColor(_colorMachine.GetActiveCrystal());
        }
        else
        {
            _colorMachine.GetColorManager().ColorOtherMaterialsOfColor(_colorMachine.GetActiveCrystal());
        }
        

    }
}
