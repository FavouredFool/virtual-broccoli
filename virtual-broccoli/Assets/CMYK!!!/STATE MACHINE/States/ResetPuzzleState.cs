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

    public override void Start()
    {
        _startTimeInSeconds = Time.time;
        _colorMachine.GetCauldron().SetCauldronColorCMYK(Vector4.zero);
    }

    public override void Update()
    {
        

        if (Time.time - _startTimeInSeconds < _colorMachine.GetCauldronRefillTime())
        {
            // PARTICLES
            return;
        }

        Debug.Log("Resetting Puzzle");

        _colorMachine.SetState(new MixingState(_colorMachine));

    }

    public override void End()
    {
        _colorMachine.GetCauldron().SetCauldronColorCMYK(_colorMachine.GetActiveCrystalInstruction().GetGoalColors()[0]);

        _colorMachine.GetScreen().SetColorAndText(_colorMachine.GetActiveCrystalInstruction().GetGoalColors()[_colorMachine.GetMixIteration()]);
    }
}
