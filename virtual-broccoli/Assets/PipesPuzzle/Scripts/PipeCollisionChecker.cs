using System;
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
        if ((collider.gameObject.CompareTag("Ventil") || collider.gameObject.CompareTag("Pipe")) && (!_pipe.IsDragged() || !collider.gameObject.GetComponent<Pipe>().IsDragged()))
        {
            _pipe.AddNeighbor(this.name, collider.gameObject);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if ((collider.gameObject.CompareTag("Ventil") || collider.gameObject.CompareTag("Pipe")) && (_pipe.IsDragged() ^ collider.gameObject.GetComponent<Pipe>().IsDragged()))
        {
            _pipe.RemoveNeighbor(this.name);
        }
    }

}
