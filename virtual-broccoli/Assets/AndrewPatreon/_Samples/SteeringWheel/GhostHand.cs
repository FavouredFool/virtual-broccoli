using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GhostHand : MonoBehaviour
{
    private XRBaseInteractor interactor;
    private MeshRenderer meshRenderer;

    private void Awake()
    {
        interactor = GetComponentInParent<XRBaseInteractor>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        Hide(null);
    }

    private void OnEnable()
    {
        interactor.selectEntered.AddListener(Show);
        interactor.selectExited.AddListener(Hide);
    }

    private void OnDisable()
    {
        interactor.selectEntered.RemoveListener(Show);
        interactor.selectExited.AddListener(Hide);
    }

    private void Show(SelectEnterEventArgs args)
    {
        meshRenderer.enabled = true;
    }

    private void Hide(SelectExitEventArgs args)
    {
        meshRenderer.enabled = false;
    }
}
