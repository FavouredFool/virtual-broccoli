using UnityEngine;
using UnityEngine.InputSystem;

public class ParameterInputApplier : BaseInputApplier
{
    public ParameterInputApplier(InputAction action, Animator animator, string animationName, bool inverted)
        : base(action, animator, inverted)
    {
        animationIndex = Animator.StringToHash(animationName);
    }

    public override void Apply(float speed)
    {
        base.Apply(speed);
        animator.SetFloat(animationIndex, currentBlend);
    }
}
