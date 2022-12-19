using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ColorMachine;

public class MixingState : State
{
    public MixingState(ColorMachine colorMachine) : base(colorMachine)
    {
    }

    private float _startTime = float.NaN;
    private float _delay = 2;
    private bool _break = false;

    public override void StartState()
    {
        _colorMachine.SetResetMix(false);
    }

    public override void UpdateState()
    {
        if (_break)
        {
            if (float.IsNaN(_startTime))
            {
                _startTime = Time.time;
            }

            if (Time.time - _startTime < _delay)
            {
                return;
            }

            if (_colorMachine.GetActiveCrystalInstruction().GetGoalColors().Count > _colorMachine.GetMixIteration())
            {
                _colorMachine.SetState(new ResetPuzzleState(_colorMachine));
            }
            else
            {
                _colorMachine.SetState(new FreeColorState(_colorMachine));
            }

            return;

        }

        if (_colorMachine.GetResetMix())
        {
            _colorMachine.SetResetMix(false);
            _colorMachine.SetState(new ResetPuzzleState(_colorMachine));
        }

        if (_colorMachine.GetCauldron().GetActiceCauldronColor() == _colorMachine.GetActiveCrystalInstruction().GetGoalColors()[_colorMachine.GetMixIteration()])
        {
            _colorMachine.SetMixIteration(_colorMachine.GetMixIteration() + 1);

            _break = true;
        }

    }

}
