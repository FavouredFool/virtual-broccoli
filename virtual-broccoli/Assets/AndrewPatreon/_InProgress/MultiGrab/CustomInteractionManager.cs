using UnityEngine.XR.Interaction.Toolkit;

public class CustomInteractionManager : XRInteractionManager
{
    protected override bool ResolveExistingSelect(IXRSelectInteractor interactor, IXRSelectInteractable interactable)
    {
        return true;
    }
}
