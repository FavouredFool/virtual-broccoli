using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class OffsetInteractable : XRGrabInteractable
{
    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        base.OnSelectEntering(args);
        MatchAttachPoint(args.interactor);
    }

    private void MatchAttachPoint(XRBaseInteractor interactor)
    {
        bool isDirect = interactor is XRDirectInteractor;

        attachTransform.position = isDirect ? GetAttachTransform(interactor).position : transform.position;
        attachTransform.rotation = isDirect ? GetAttachTransform(interactor).rotation : transform.rotation;
    }
}
