using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LightstickDispenser : XRBaseInteractable
{
    [SerializeField] private GameObject lightstickPrefab;

    private void Start()
    {
        //if emission may be deactivated
        GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        CreateAndSelectArrow(args);        
    }

    private void CreateAndSelectArrow(SelectEnterEventArgs args)
    {
        // Create lightstick, force into interacting hand
        OffsetInteractorLightstick lightstick = CreateLightStick(args.interactorObject.transform);
        interactionManager.SelectEnter(args.interactorObject, lightstick);
    }

    private OffsetInteractorLightstick CreateLightStick(Transform orientation)
    {
        // Create lightstick, and get lightstick component
        GameObject lightstickObject = Instantiate(lightstickPrefab, orientation.position, orientation.rotation);
        return lightstickObject.GetComponent<OffsetInteractorLightstick>();
    }
}
