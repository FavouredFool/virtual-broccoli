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

    public override void Start()
    {
        _startTimeInSeconds = Time.time;
        _colorMachine.GetSmokeEmittor().ReleaseSmoke(_colorMachine.GetActiveCrystalInstruction().GetGoalColors()[_colorMachine.GetMixIteration() - 1]);
    }

    public override void Update()
    {
        if (Time.time - _startTimeInSeconds < _colorMachine.GetColorReleaseTime())
        {
            // PARTICLES
            return;
        }

        if (_colorMachine.GetActiveCrystalInstruction().GetGoalColors().Count > _colorMachine.GetMixIteration())
        {
            _colorMachine.SetState(new ResetPuzzleState(_colorMachine));

        }
        else
        {
            _colorMachine.SetState(new ResetMachineState(_colorMachine));

        }
    }
}
