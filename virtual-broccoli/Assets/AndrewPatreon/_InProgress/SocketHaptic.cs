using UnityEngine.XR.Interaction.Toolkit;

public class SocketHaptic : XRSocketInteractor
{
    private XRBaseControllerInteractor previousInteractor;

    protected override void OnHoverEntered(HoverEnterEventArgs args)
    {
        base.OnHoverEntered(args);

        if (args.interactableObject is IXRSelectInteractable interactable)
        {
            if (interactable.firstInteractorSelecting is XRBaseControllerInteractor interactor)
                previousInteractor = interactor;
        }
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        if (previousInteractor)
        {
            previousInteractor.SendHapticImpulse(1.0f, 1.0f);
            previousInteractor = null;
        }
    }
}
