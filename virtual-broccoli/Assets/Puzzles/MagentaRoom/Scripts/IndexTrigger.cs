using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class IndexTrigger : MonoBehaviour
{
    [SerializeField]
    private string _compareTag;

    [SerializeField]
    private ProgressMovementController _progressController;

    private bool _triggered = false;

    public bool GetTriggered()
    {
        return _triggered;
    }

    public void SetTriggered(bool triggered)
    {
        _triggered = triggered;
        Debug.Log(_triggered);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<XRController>(out XRController controller))
        {
            SetTriggered(true);
        } else if (_compareTag != null && other.CompareTag(_compareTag))
        {
           _progressController.SetHoldValue(name);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<XRController>(out XRController controller))
        {
            SetTriggered(false);
        }
    }
}
