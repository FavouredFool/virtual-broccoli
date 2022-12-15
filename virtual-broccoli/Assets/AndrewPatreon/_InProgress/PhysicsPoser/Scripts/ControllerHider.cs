using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(PhysicsPoser))]
[RequireComponent(typeof(XRDirectInteractor))]
public class ControllerHider : MonoBehaviour
{
    private PhysicsPoser physicsPoser = null;
    private XRDirectInteractor interactor = null;

    private Transform ControllerTransform => interactor.xrController.model;

    private void Awake()
    {
        physicsPoser = GetComponent<PhysicsPoser>();
        interactor = GetComponent<XRDirectInteractor>();
    }

    private void OnEnable()
    {
        interactor.selectEntered.AddListener(Hide);
        interactor.selectExited.AddListener(Show);
    }

    private void OnDisable()
    {
        interactor.selectEntered.RemoveListener(Hide);
        interactor.selectExited.RemoveListener(Show);
    }

    private void Hide(SelectEnterEventArgs args)
    {
        ControllerTransform.gameObject.SetActive(false);
    }

    private void Show(SelectExitEventArgs args)
    {
        StartCoroutine(WaitForRange());
    }

    private IEnumerator WaitForRange()
    {
        yield return new WaitWhile(physicsPoser.WithinPhysicsRange);
        ControllerTransform.gameObject.SetActive(true);
    }
}
