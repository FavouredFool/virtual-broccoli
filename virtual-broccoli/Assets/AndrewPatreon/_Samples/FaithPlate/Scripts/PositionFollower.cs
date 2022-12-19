using UnityEngine;

public class PositionFollower : MonoBehaviour
{
    [SerializeField] private Transform target;

    private void Update()
    {
        FollowTarget();
    }

    private void FollowTarget()
    {
        transform.position = target.position;
    }
}
