using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PlateTrigger : MonoBehaviour
{
    [SerializeField] private XRSocketInteractor _socket;
    private IXRSelectInteractable _otherXR;

    //TODO test this
    public void TriggerEnter()
    {
        _otherXR = _socket.GetOldestInteractableSelected();
        SendCompareInfo(_otherXR.transform.gameObject.name);
    }

    public void TriggerExit()
    {
        SendCompareInfo("");
    }

    private void SendCompareInfo(string comparement)
    {
        Debug.Log(comparement);
        GetComponentInParent<SequenceController>().CompareTriggerPlate(name, comparement);
    }
}
