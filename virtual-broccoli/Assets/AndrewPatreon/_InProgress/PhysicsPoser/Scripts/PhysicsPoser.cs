using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(Rigidbody))]
public class PhysicsPoser : MonoBehaviour
{
    public float physicsRange = 0.1f;
    public Vector3 physicsOffset = Vector3.zero;
    public LayerMask physicsMask = ~0;

    [Range(0, 1)] public float slowDownVelocity = 0.75f;
    [Range(0, 1)] public float slowDownAngularVelocity = 0.75f;

    [Range(0, 100)] public float maxPositionChange = 75.0f;
    [Range(0, 100)] public float maxRotationChange = 75.0f;

    private Rigidbody rigidBody = null;
    private XRDirectInteractor interactor = null;
    private ActionBasedController controller = null;

    private Vector3 targetPosition = Vector3.zero;
    private Quaternion targetRotation = Quaternion.identity;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        interactor = GetComponent<XRDirectInteractor>();
        controller = GetComponent<ActionBasedController>();
    }

    private void Start()
    {
        DisableDefaultTracking();
    }

    private void DisableDefaultTracking()
    {
        controller.enableInputTracking = false;
    }

    private void Update()
    {
        UpdateTracking(controller);
    }

    private void UpdateTracking(ActionBasedController controller)
    {
        targetPosition = controller.positionAction.action.ReadValue<Vector3>();
        targetRotation = controller.rotationAction.action.ReadValue<Quaternion>();
    }

    private void FixedUpdate()
    {
        if (IsHoldingObject() || !WithinPhysicsRange())
        {
            MoveUsingTransform();
            RotateUsingTransform();
        }
        else
        {
            MoveUsingPhysics();
            RotateUsingPhysics();
        }
    }

    public bool IsHoldingObject()
    {
        return interactor.hasSelection;
    }

    public bool WithinPhysicsRange()
    {
        return Physics.CheckSphere(transform.position + physicsOffset, physicsRange, physicsMask, QueryTriggerInteraction.Ignore);
    }

    private void MoveUsingPhysics()
    {
        rigidBody.velocity *= slowDownVelocity;
        Vector3 velocity = FindNewVelocity();

        if (IsValidVelocity(velocity.x))
        {
            float maxChange = maxPositionChange * Time.deltaTime;
            rigidBody.velocity = Vector3.MoveTowards(rigidBody.velocity, velocity, maxChange);
        }
    }

    private Vector3 FindNewVelocity()
    {
        Vector3 worldPosition = transform.root.TransformPoint(targetPosition);
        Vector3 difference = worldPosition - rigidBody.position;
        return difference / Time.deltaTime;
    }

    private void RotateUsingPhysics()
    {
        rigidBody.angularVelocity *= slowDownAngularVelocity;
        Vector3 angularVelocity = FindNewAngularVelocity();

        if (IsValidVelocity(angularVelocity.x))
        {
            float maxChange = maxRotationChange * Time.deltaTime;
            rigidBody.angularVelocity = Vector3.MoveTowards(rigidBody.angularVelocity, angularVelocity, maxChange);
        }
    }

    private Vector3 FindNewAngularVelocity()
    {
        Quaternion worldRotation = transform.root.rotation * targetRotation;
        Quaternion difference = worldRotation * Quaternion.Inverse(rigidBody.rotation);

        difference.ToAngleAxis(out float angleInDegrees, out Vector3 rotationAxis);
        angleInDegrees -= angleInDegrees > 180 ? 360 : 0;

        return (rotationAxis * angleInDegrees * Mathf.Deg2Rad) / Time.deltaTime;
    }

    private bool IsValidVelocity(float value)
    {
        return !float.IsNaN(value) && !float.IsInfinity(value);
    }

    private void MoveUsingTransform()
    {
        rigidBody.velocity = Vector3.zero;
        transform.localPosition = targetPosition;
    }

    private void RotateUsingTransform()
    {
        rigidBody.angularVelocity = Vector3.zero;
        transform.localRotation = targetRotation;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position + physicsOffset, physicsRange);
    }

    private void OnValidate()
    {
        if (TryGetComponent(out Rigidbody rigidBody))
            rigidBody.useGravity = false;
    }
}
