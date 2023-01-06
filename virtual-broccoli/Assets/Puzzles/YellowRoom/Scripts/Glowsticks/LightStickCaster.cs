using UnityEngine;

public class LightStickCaster : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask = ~0;

    private Vector3 lastPosition = Vector3.zero;

    public bool CheckForCollision(out RaycastHit hit)
    {
        if (lastPosition == Vector3.zero)
            lastPosition = transform.position;

        bool collided = Physics.Linecast(lastPosition, transform.position, out hit, layerMask);
        lastPosition = collided ? lastPosition : transform.position;

        return collided;
    }
}
