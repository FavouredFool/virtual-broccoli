using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ColorMachine;

public class ProcessCrystalState : State
{
    public ProcessCrystalState(ColorMachine colorMachine) : base(colorMachine)
    {
    }

    public override void Start()
    {
        Debug.Log("process Start");
    }

    public override void Update()
    {
        bool slotIsClosed = _colorMachine.GetSlot().CloseLidPerFrame();

        if (slotIsClosed)
        {
            _colorMachine.SetState(new FreeColorState(_colorMachine));
        }
    }


}
