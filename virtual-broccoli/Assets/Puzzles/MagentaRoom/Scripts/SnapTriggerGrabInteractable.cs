using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;

public class SnapTriggerGrabInteractable : XRGrabInteractable
{
    [SerializeField]
    private Transform _positionTrigger;

    private IXRSelectInteractor _interactor;

    protected override void Awake()
    {
        base.Awake();
        trackPosition = false;
        selectEntered.AddListener(SetInteractor);
    }

    protected void OnDestory()
    {
        selectEntered.RemoveListener(SetInteractor);
    }

    private void SetInteractor(SelectEnterEventArgs args)
    {
        _interactor = args.interactorObject;
    }

    private void Update()
    {
        if(this.isSelected)
        {
            foreach (Transform trigger in _positionTrigger)
            {
                /*IndexTrigger triggerScript = trigger.GetComponent<IndexTrigger>();
                if (triggerScript.GetTriggered())
                {
                    transform.position = trigger.transform.position;
                    triggerScript.SetTriggered(false);
                }*/
                if (_interactor.transform.GetComponent<Collider>().bounds.Intersects(trigger.GetComponent<Collider>().bounds))
                {
                    Vector3 triggerPos = trigger.transform.position;
                    Vector3 transformPos = transform.position;
                    transform.position = new Vector3(transformPos.x, triggerPos.y, transformPos.z);
                }
            }
        }
    }
}
