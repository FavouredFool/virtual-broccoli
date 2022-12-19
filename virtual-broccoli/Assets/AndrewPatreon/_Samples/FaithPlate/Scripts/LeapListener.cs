using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LeapListener : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LeapProvider leapProvider;
    [SerializeField] private MonoBehaviour targetBehaviour;

    private void Awake()
    {
        targetBehaviour.enabled = false;
    }

    private void OnEnable()
    {
        leapProvider.beginLocomotion += Enable;
        leapProvider.endLocomotion += Disable;
    }

    private void OnDisable()
    {
        leapProvider.beginLocomotion -= Enable;
        leapProvider.endLocomotion -= Disable;
    }

    private void Enable(LocomotionSystem locomotionSystem)
    {
        targetBehaviour.enabled = true;
    }

    private void Disable(LocomotionSystem locomotionSystem)
    {
        targetBehaviour.enabled = false;
    }
}
