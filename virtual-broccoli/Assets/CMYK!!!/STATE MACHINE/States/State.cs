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

    public virtual void Start()
    {
    }

    public virtual void Update()
    {
    }

    public virtual void End()
    {
    }
}
