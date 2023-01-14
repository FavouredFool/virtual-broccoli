using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockedTrigger : MonoBehaviour
{
    [SerializeField]
    private OpenBoxController _boxController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BoxDoor")) _boxController.callBlocked(this.gameObject.name);
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("TriggerExit: " + other.name);
        if (other.CompareTag("BoxDoor")) _boxController.callBlocked("release");
    }
}
