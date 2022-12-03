using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenBoxTrigger : MonoBehaviour
{
    [SerializeField]
    private OpenBoxController _boxController;

    private void OnTriggerEnter(Collider other)
    {
        _boxController.setOpen(true);
    }

    private void OnTriggerExit(Collider other)
    {
        _boxController.setOpen(false);
    }
}
