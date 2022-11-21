using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.XR.Interaction.Toolkit;

public class Tooltip : MonoBehaviour
{
    [SerializeField] private XRRayInteractor rayInteractor;
    [SerializeField] private GameObject toolTipObject;

    private void Update()
    {
        UpdateTooltip();
    }

    private void UpdateTooltip()
    {
        rayInteractor.TryGetCurrentUIRaycastResult(out RaycastResult raycastResult);

        if(SetVisibility(raycastResult))
            toolTipObject.transform.position = raycastResult.worldPosition;
    }

    private bool SetVisibility(RaycastResult raycastResult)
    {
        toolTipObject.SetActive(raycastResult.isValid);
        return raycastResult.isValid;
    }
}
