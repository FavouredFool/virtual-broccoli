using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LightstickDispenser : XRBaseInteractable
{
    [SerializeField] private GameObject lightstickPrefab;
    [SerializeField] private TMP_Text textMesh;

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        int lightCount = int.Parse(textMesh.text);
        if (lightCount > 0)
        {
            lightCount -= 1;
            CreateAndSelectArrow(args, lightCount);
            if(lightCount == 0)
            {
                this.GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");
            }
        }
    }

    private void CreateAndSelectArrow(SelectEnterEventArgs args, int lightCount)
    {
        // Create lightstick, force into interacting hand
        LightStick lightstick = CreateLightStick(args.interactorObject.transform);
        interactionManager.SelectEnter(args.interactorObject, lightstick);
        textMesh.text = (lightCount).ToString();
    }

    private LightStick CreateLightStick(Transform orientation)
    {
            // Create lightstick, and get lightstick component
            GameObject lightstickObject = Instantiate(lightstickPrefab, orientation.position, orientation.rotation);
            return lightstickObject.GetComponent<LightStick>();
    }
}
