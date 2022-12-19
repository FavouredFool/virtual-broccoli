using UnityEngine;
using UnityEngine.InputSystem;

public abstract class BaseInputApplier
{
    protected InputAction action = null;
    protected Animator animator = null;

    protected int animationIndex = -1;
    protected bool inverted = false;
    protected float currentBlend = 0.0f;

    public BaseInputApplier(InputAction action, Animator animator, bool inverted)
    {
        this.action = action;
        this.animator = animator;
        this.inverted = inverted;
    }

    public virtual void Apply(float speed)
    {
        currentBlend = BlendUsingInput(speed);
    }

    private float BlendUsingInput(float speed)
    {
        float inputValue = action.ReadValue<float>();
        inputValue = inverted ? 1.0f - inputValue : inputValue;

        float delta = speed * Time.deltaTime;
        return Mathf.MoveTowards(currentBlend, inputValue, delta);
    }
}
