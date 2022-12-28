using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;

public class SnapTriggerGrabInteractable : XRGrabInteractable
{
    [SerializeField]
    private Transform _positionTrigger;

    protected override void Awake()
    {
        base.Awake();
        trackPosition = false;
        selectEntered.AddListener(SnapMove);
    }

    private void OnDestory()
    {
        selectEntered.RemoveListener(SnapMove);
    }

    private void SnapMove(SelectEnterEventArgs args)
    {
        foreach (Transform trigger in _positionTrigger)
        {
            IndexTrigger triggerScript = trigger.GetComponent<IndexTrigger>();
            if (triggerScript.GetTriggered())
            {
                transform.position = trigger.transform.position;
                triggerScript.SetTriggered(false);
            }
        }
    }
}
