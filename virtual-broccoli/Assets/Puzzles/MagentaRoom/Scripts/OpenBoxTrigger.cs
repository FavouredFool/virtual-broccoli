using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenBoxTrigger : MonoBehaviour
{
    [SerializeField]
    private OpenBoxController _boxController;
    [SerializeField]
    private bool _triggered;

    private void Awake()
    {
        _triggered = false;
    }

    public bool getTriggered()
    {
        return _triggered;
    }

    private void OnTriggerEnter(Collider other)
    {
        _boxController.setOpen(true);
        _triggered = true;
    }

    private void OnTriggerExit(Collider other)
    {
        _boxController.setOpen(false);
    }
}
