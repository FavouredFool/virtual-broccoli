using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetTrigger : MonoBehaviour
{
    private BoxLabController _boxLabController;

    public void setController(BoxLabController controller)
    {
        _boxLabController = controller;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.Equals(_boxLabController.getSphere()))
        {
            _boxLabController.sphereReset();
        }
    }

}
