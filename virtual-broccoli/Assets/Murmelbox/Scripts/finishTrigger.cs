using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishTrigger : MonoBehaviour
{

    [SerializeField]
    private OpenBoxController _openBoxController;

    private boxLabController _controller;

    public void setController(boxLabController controller)
    {
        _controller = controller;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.Equals(_controller.getSphere()))
        {
            //_openBoxController.setOpen(true);
        }
    }
}
