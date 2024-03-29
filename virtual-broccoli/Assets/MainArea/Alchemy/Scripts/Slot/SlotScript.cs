using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using static ColorMachine;

public class SlotScript : MonoBehaviour
{
    [SerializeField]
    private XRSocketInteractor _socket;

    [SerializeField]
    private Transform _hatch;

    [SerializeField]
    private ColorMachine _machine;

    [SerializeField]
    private float _speed = 100f;

    CrystalObject _crystal = null;

    Quaternion _openRotation;

    Quaternion _closeRotation;

    

    // Start is called before the first frame update
    void Start()
    {
        _openRotation = _hatch.transform.rotation;
        _closeRotation = Quaternion.Euler(0, 0, -90);

        if (_socket == null)
        {
            Debug.LogWarning("SOCKET NOT ATTACHED IN SCRIPT");
        }
    }

    public void CrystalEntered()
    {
        if (_machine.GetState() is not AwaitCrystalState)
        {
            Debug.LogWarning("MACHINE IS IN WRONG STATE");
            return;
        }

        IXRSelectInteractable crystalXR = _socket.GetOldestInteractableSelected();

        _crystal = crystalXR.transform.GetComponent<CrystalObject>();

        /*XRGrabInteractable grabInteractable = _crystal.GetComponent<XRGrabInteractable>();
        grabInteractable.enabled = false;*/

        

        _machine.SetActiveCrystal(_crystal.GetCrystalColor());
    }


    public bool CloseLidPerFrame()
    {
        _hatch.transform.localRotation = Quaternion.RotateTowards(_hatch.transform.localRotation, _closeRotation, _speed * Time.deltaTime);

        bool closed = _hatch.transform.localRotation == _closeRotation;

        if (_crystal != null && closed)
        {
            Destroy(_crystal.gameObject);

            _crystal = null;
        }
        

        return closed;

    }

    public void ResetSlot()
    {
        StartCoroutine(OpenHatch());
    }

    public IEnumerator OpenHatch()
    {
        while (_hatch.transform.localRotation != _openRotation)
        {
            yield return null;
            _hatch.transform.localRotation = Quaternion.RotateTowards(_hatch.transform.localRotation, _openRotation, _speed * Time.deltaTime);
        }
    }
}
