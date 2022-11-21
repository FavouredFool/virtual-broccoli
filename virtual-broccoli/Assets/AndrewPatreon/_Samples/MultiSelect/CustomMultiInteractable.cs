using UnityEngine.XR.Interaction.Toolkit;

public class CustomMultiInteractable : XRBaseInteractable
{
    private MaterialApplier materialApplier;

    protected override void Awake()
    {
        base.Awake();
        materialApplier = GetComponent<MaterialApplier>();
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        if (HasMultipleInteractors())
            materialApplier.ApplyOther();
    }

    private bool HasMultipleInteractors()
    {
        return interactorsSelecting.Count > 1;
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);

        if (HasNoInteractors())
            materialApplier.ApplyOriginal();
    }

    private bool HasNoInteractors()
    {
        return interactorsSelecting.Count == 0;
    }
}
