using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionScript : MonoBehaviour
{
    [SerializeField]
    MeshRenderer _fluidMeshRenderer;

    OffsetInteractable _offsetInteractable;

    Rigidbody _rb;

    public void Awake()
    {
        _offsetInteractable = GetComponent<OffsetInteractable>();
        _rb = GetComponent<Rigidbody>();
    }

    public void Update()
    {
        if (transform.parent == null)
        {
            _rb.isKinematic = false;
        }
        
    }

    public void SetFluidMaterial(Material mat)
    {
        _fluidMeshRenderer.material = mat;
    }

    public void ChangeGrabableState(bool state)
    {
        _offsetInteractable.enabled = state;
    }

    public Color GetColor()
    {
        return _fluidMeshRenderer.material.GetColor("_TopColor");
    }
}
