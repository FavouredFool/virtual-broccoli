using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetTrigger : MonoBehaviour
{
    private BoxLabController _controller;

    public void setController(BoxLabController controller)
    {
        _controller = controller;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.Equals(_controller.getSphere()))
        {
            _controller.sphereReset();
        }
    }

}
