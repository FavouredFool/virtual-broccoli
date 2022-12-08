using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndexTrigger : MonoBehaviour
{
    [SerializeField]
    private string _compareTag;
    [SerializeField]
    private ProgressMovementController _progressController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_compareTag))
        {
            other.attachedRigidbody.velocity.Set(0f, 0f, 0f);
            other.gameObject.transform.position = this.gameObject.transform.position;
            if (_progressController) _progressController.setHoldValue(name);
        }
    }
}
