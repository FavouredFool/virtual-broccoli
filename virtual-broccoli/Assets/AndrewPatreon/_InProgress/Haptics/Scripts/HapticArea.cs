using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HapticArea : XRBaseInteractable
{
    [Header("Haptic Settings")]
    [SerializeField] private float impulseFrequency = 0.1f;
    [SerializeField] private float maxHapticDistance = 1.0f;

    protected override void OnEnable()
    {
        base.OnEnable();
        firstHoverEntered.AddListener(StartImpulseRoutine);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        firstHoverEntered.RemoveListener(StartImpulseRoutine);
    }

    private void StartImpulseRoutine(HoverEnterEventArgs interactor)
    {
        StartCoroutine(ImpulseRoutine());
    }

    private IEnumerator ImpulseRoutine()
    {
        // Once the routine is start, let's continue for any hovering interactors
        while (interactorsHovering.Count > 0)
        {
            // Send a haptic impulse to every interactor
            foreach (IXRHoverInteractor interactor in interactorsHovering)
                SendImpulseUsingDistance(interactor);

            // Wait for the current impulse to be over
            yield return new WaitForSeconds(impulseFrequency);
        }
    }

    private void SendImpulseUsingDistance(IXRInteractor interactor)
    {
        // Using the controllers ditance to the first, find a value between
        // 0 and 1 using inverse lerp for the controller.
        float currentDistance = GetDistanceSqrToInteractor(interactor);



        currentDistance = Mathf.Clamp(currentDistance, 0, maxHapticDistance);

        // ISSUE: Because we're using the interactor, we aren't using the center of the sphere trigger

        // We can make the 

        // TODO: Try and pass in a long time, then pass in a length of 0 to stop

        float amplitude = Mathf.InverseLerp(0, maxHapticDistance, maxHapticDistance);
        // print(currentDistance);

        // Primarily cast to send impulse to controller
        /*
        if (interactor is XRBaseControllerInteractor controller)
            controller.SendHapticImpulse(amplitude, impulseFrequency);
        */
    }

    public override float GetDistanceSqrToInteractor(IXRInteractor interactor)
    {
        // Get the actual distance with square root
        return Mathf.Sqrt(base.GetDistanceSqrToInteractor(interactor));
    }

    public override bool IsHoverableBy(IXRHoverInteractor interactor)
    {
        // If the haptic area is on the proper layer, and is a player's hand
        return base.IsHoverableBy(interactor) && interactor is XRDirectInteractor;
    }

    public override bool IsSelectableBy(IXRSelectInteractor interactor)
    {
        // The haptic area can't be selected
        return false;
    }

    private void OnDrawGizmos()
    {
        if (TryGetComponent(out SphereCollider collider))
            Gizmos.DrawWireSphere(transform.position, maxHapticDistance);
    }
}
