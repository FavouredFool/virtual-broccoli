using UnityEngine.XR.Interaction.Toolkit;

public class OffsetInteractable : XRGrabInteractable
{
    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        base.OnSelectEntering(args);
        MatchAttachmentPoints(args.interactorObject);
    }

    protected void MatchAttachmentPoints(IXRInteractor interactor)
    {
        if(IsFirstSelecting(interactor))
        {
            bool IsDirect = interactor is XRDirectInteractor;
            attachTransform.position = IsDirect ? interactor.GetAttachTransform(this).position : transform.position;
            attachTransform.rotation = IsDirect ? interactor.GetAttachTransform(this).rotation : transform.rotation;
        }
    }

    private bool IsFirstSelecting(IXRInteractor interactor)
    {
        return interactor == firstInteractorSelecting;
    }
}
