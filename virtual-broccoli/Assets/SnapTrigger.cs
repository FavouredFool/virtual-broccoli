using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapTrigger : MonoBehaviour
{
    [SerializeField]
    private string _compareTag;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(_compareTag);
        if (other.CompareTag(_compareTag))
        {
            other.attachedRigidbody.velocity.Set(0f, 0f, 0f);
            Vector3 otherPosition = other.gameObject.transform.position;
            Vector3 targetPosition = new Vector3(otherPosition.x, this.gameObject.transform.position.y, otherPosition.y);
            other.gameObject.transform.position = targetPosition;
            
        }
    }
}
