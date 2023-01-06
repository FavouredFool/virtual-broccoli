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

    private void OnTriggerEnter(Collider other)
    {
        if (!string.IsNullOrEmpty(_compareTag) && other.CompareTag(_compareTag))
        {
           _progressController.SetHoldValue(name);
        }
    }
}
