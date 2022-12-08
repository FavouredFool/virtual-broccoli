using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ColorChanger : MonoBehaviour
{
    [SerializeField]
    private Material _selectMaterial = null;

    private MeshRenderer _meshRenderer = null;
    private XRBaseInteractable _interactable = null;
    private Material _originalMaterial = null;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _originalMaterial = _meshRenderer.material;

        _interactable = GetComponent<XRBaseInteractable>();
        _interactable.hoverEntered.AddListener(SetSelectMaterial);
        _interactable.hoverExited.AddListener(SetOriginalMaterial);

    }

    private void OnDestroy()
    {
        _interactable.hoverEntered.RemoveListener(SetSelectMaterial);
        _interactable.hoverExited.RemoveListener(SetOriginalMaterial);
    }
    
    private void SetSelectMaterial(HoverEnterEventArgs args)
    {
        _meshRenderer.material = _selectMaterial;
    }

    private void SetOriginalMaterial(HoverExitEventArgs args)
    {
        _meshRenderer.material = _originalMaterial;
    }
}
