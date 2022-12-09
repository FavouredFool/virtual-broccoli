using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    protected State _state;

    public void SetState(State state)
    {
        if (_state != null)
        {
            _state.End();
        }
        
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
