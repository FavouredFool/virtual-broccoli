using UnityEngine;
using UnityEngine.InputSystem;

public class LayerInputApplier : BaseInputApplier
{
    public LayerInputApplier(InputAction action, Animator animator, string animationName, bool inverted)
        : base(action, animator, inverted)
    {
        animationIndex = animator.GetLayerIndex(animationName);
    }

    public override void Apply(float speed)
    {
        base.Apply(speed);
        animator.SetLayerWeight(animationIndex, currentBlend);
    }
}
