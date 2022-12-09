using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    protected State _state;

    public void SetState(State state)
    {
        _state = state;
        _state.Start();
    }

    public State GetState()
    {
        return _state;
    }

    public void UpdateState()
    {
        _state.Update();
    }

}
