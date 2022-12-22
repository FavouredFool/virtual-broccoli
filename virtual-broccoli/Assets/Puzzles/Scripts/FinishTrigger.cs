using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishTrigger : MonoBehaviour
{

    [SerializeField]
    private OpenBoxController _openBoxController;

    private BoxLabController _boxLabController;

    public void setController(BoxLabController controller)
    {
        _boxLabController = controller;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.Equals(_boxLabController.getSphere()))
        {
            _openBoxController.setOpen(true);
            _boxLabController.sphereReset();
        }
    }
}
