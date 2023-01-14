using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DoorSetup : MonoBehaviour
{
    [Header("Interaction")]
    [SerializeField] private XRBaseInteractable handle = null;
    [SerializeField] private PhysicsRotator rotator = null;

    [Header("Direction")]
    [SerializeField] private Transform start = null;
    [SerializeField] private Transform end = null;

    private Vector3 grabPosition = Vector3.zero;
    private float startingPercentage = 0.0f;
    private float currentPercentage = 0.0f;

    protected virtual void OnEnable()
    {
        handle.selectEntered.AddListener(StoreGrabInfo);
    }

    protected virtual void OnDisable()
    {
        handle.selectEntered.RemoveListener(StoreGrabInfo);
    }

    private void StoreGrabInfo(SelectEnterEventArgs args)
    {
        /*Update the starting position, this prevents the drawer from jumping to the hand.*/
        startingPercentage = currentPercentage;

        /*Store starting position for pull direction*/
        grabPosition = args.interactorObject.transform.position;
    }

    private void Update()
    {
        /*If the handle of the drawer is selected, update the drawer*/
        if(handle.isSelected)
        {
            UpdateDrawer();
        }

    }

    private void UpdateDrawer()
    {
        /*From the starting percentage, apply the difference from each update*/
        Debug.LogWarning(startingPercentage);
        Debug.LogWarning(FindPercentageDifference());
        float newPercentage = startingPercentage + FindPercentageDifference();

        /*We then use a lerp to find the appropriate position between the start/end points. 
        Allowing the percentage value to be unclamped gives us a more realistic response 
        when pulling beyond the start/end positions.*/
        rotator.RotateTo(Quaternion.Slerp(start.rotation, end.rotation, newPercentage));

        /*We clamp after the percetange has been applied to keep the current percentage valid
        for other uses.*/
        currentPercentage = Mathf.Clamp01(newPercentage);
    }

    private float FindPercentageDifference()
    {
        /*Find the directions for the player's pull direction and the target direction.*/
        Vector3 handPosition = handle.GetOldestInteractorSelecting().transform.position;
        Vector3 pullDirection = handPosition - grabPosition;
        Vector3 targetDirection = end.position - start.position;

        /*Store length, then normalize target direction for use in Vector3.Dot*/
        float length = targetDirection.magnitude;
        targetDirection.Normalize();

        /*Typically, we use two normalized vectors for Vector3.Dot. We'll be leaving one of the 
        directions with its original length. Thus, we get an actual combination of the distance/direction 
        similarity, then dividing by the length, gives us a value between 0 and 1.*/
        return Vector3.Dot(pullDirection, targetDirection) / length;
    }

    private void OnDrawGizmos()
    {
        /*Shows the general direction of the interaction*/
        if (start && end)
            Gizmos.DrawLine(start.position, end.position);
    }
}
