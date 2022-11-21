using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[CanSelectMultiple(true)]
public class MultiGrabInteractable : OffsetInteractable
{
    private bool CanUpdate => interactorsSelecting.Count == 2;

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);

        if (updatePhase == XRInteractionUpdateOrder.UpdatePhase.Dynamic)
        {
            if (CanUpdate)
                UpdateDirection();
        }
    }

    private void UpdateDirection()
    {
        Transform attachTransform = firstInteractorSelecting.GetAttachTransform(this);
        attachTransform.forward = FindDirection();
    }

    private Vector3 FindDirection()
    {
        Vector3 firstPosition = interactorsSelecting[0].transform.position;
        Vector3 secondPosition = interactorsSelecting[1].transform.position;
        return (firstPosition - secondPosition).normalized;
    }

    protected override void Grab()
    {
        // TODO: We may want to change this for like, is first selector?
        if (interactorsSelecting.Count == 1)
            base.Grab();
    }

    protected override void Drop()
    {
        // TODO: We may want to change this for like, is first selector?
        // I'm not sure what happens when the first hand drops the object
        if (interactorsSelecting.Count == 0)
            base.Drop();
    }
}
