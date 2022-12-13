using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishTrigger : MonoBehaviour
{

    [SerializeField]
    private OpenBoxController _openBoxController;

    private BoxLabController _controller;

    public void setController(BoxLabController controller)
    {
        _controller = controller;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.Equals(_controller.getSphere()))
        {
            _openBoxController.setOpen(true);
            _controller.sphereReset();
        }
    }
}
