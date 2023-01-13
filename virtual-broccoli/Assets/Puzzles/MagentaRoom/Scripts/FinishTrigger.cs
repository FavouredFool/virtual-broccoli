using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishTrigger : MonoBehaviour
{
    //[SerializeField] private OpenBoxController openBoxController;
    [SerializeField] private RoomClearController roomClearController;

    private BoxLabController _boxLabController;

    public void setController(BoxLabController controller)
    {
        _boxLabController = controller;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.Equals(_boxLabController.getSphere()))
        {
            roomClearController.ClearLight("marblebox");
            //_openBoxController.setOpen(true);
            _boxLabController.sphereReset();  
        }
    }
}
