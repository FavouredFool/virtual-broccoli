using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    protected ColorMachine _colorMachine;

    public State(ColorMachine colorMachine)
    {
        _colorMachine = colorMachine;
    }

    public virtual void StartState()
    {
    }

    public virtual void UpdateState()
    {
    }

    public virtual void EndState()
    {
    }
}
