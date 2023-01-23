using UnityEngine;

public class BallPositionChecker : MonoBehaviour
{
    private Vector3 lastUpdateCheck;
    private Vector3 lastMovelessPosition;

    void Update()
    {
        if (lastUpdateCheck == null)
        {
            lastMovelessPosition = transform.position;
        } else if (transform.position == lastUpdateCheck && lastUpdateCheck != lastMovelessPosition)
        {
            lastMovelessPosition = transform.position;
        }
        lastUpdateCheck = transform.position;
    }

    public Vector3 GetLastMovelessPosition()
    {
        return lastMovelessPosition;
    }
}
