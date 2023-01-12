using UnityEngine;

public class PipeCollisionChecker : MonoBehaviour
{
    private Pipe _pipe;

    private void Start()
    {
        _pipe = GetComponentInParent<Pipe>();
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (CanEnterOrLeave(collider))
        {
            _pipe.AddNeighbor(name, collider.gameObject);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (CanEnterOrLeave(collider))
        {
            _pipe.RemoveNeighbor(name, collider.gameObject);
        }
    }

    private bool CanEnterOrLeave(Collider collider)
    {
        return collider.gameObject.CompareTag("Ventil") || (collider.gameObject.tag.Contains("Pipe")
            && (!_pipe.IsDragged() || !collider.gameObject.GetComponent<Pipe>().IsDragged()));
    }
}
