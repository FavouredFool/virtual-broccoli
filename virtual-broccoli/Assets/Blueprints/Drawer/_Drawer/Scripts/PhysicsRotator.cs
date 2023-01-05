using UnityEngine;

public class PhysicsRotator : MonoBehaviour
{
    private new Rigidbody rigidbody = null;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    public void RotateTo(Quaternion newRotation)
    {
        Debug.LogWarning(newRotation);
        rigidbody.MoveRotation(newRotation);
    }
}
