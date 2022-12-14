using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ColorMachine;

public class ResetPuzzleState : State
{
    public ResetPuzzleState(ColorMachine colorMachine) : base(colorMachine)
    {
    }

    private float _startTimeInSeconds;
    private bool _shouldResetToWhite;

    public override void StartState()
    {
        _startTimeInSeconds = Time.time;

        _shouldResetToWhite = _colorMachine.GetCauldron().GetActiceCauldronColor() != Vector4.zero;

        if (_shouldResetToWhite)
        {
            _colorMachine.GetCauldron().SetCauldronColorCMYK(Vector4.zero);
        }
    }

    public override void UpdateState()
    {
        if (_shouldResetToWhite && Time.time - _startTimeInSeconds < _colorMachine.GetCauldronRefillTime())
        {
            return;
        }

        _colorMachine.SetState(new MixingState(_colorMachine));
    }

    public override void EndState()
    {
        _colorMachine.GetCauldron().SetCauldronColorCMYK(_colorMachine.GetActiveCrystalInstruction().GetGoalColors()[0]);

        _colorMachine.GetScreen().SetColorAndText(_colorMachine.GetActiveCrystalInstruction().GetGoalColors()[_colorMachine.GetMixIteration()]);
    }
}
