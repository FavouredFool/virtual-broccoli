using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    protected State _state;

    public void SetState(State state)
    {
        if (_state != null)
        {
            _state.EndState();
        }
        
        _state = state;
        _state.StartState();
    }

    public State GetState()
    {
        return _state;
    }

    public void UpdateState()
    {
        _state.UpdateState();
    }

}
