using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SlotScript : MonoBehaviour
{
    [SerializeField]
    private XRSocketInteractor _socket;

    [SerializeField]
    private Transform _hatch;

    [SerializeField]
    private ColorMachineSequence _machine;

    [SerializeField]
    private float _speed = 100f;

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
        Debug.Log("CRYSTAL ENTERED");
        IXRSelectInteractable crystalXR = _socket.GetOldestInteractableSelected();

        CrystalObject crystal = crystalXR.transform.GetComponent<CrystalObject>();

        StartCoroutine(ProcessCrystal(crystal));
    }

    public IEnumerator ProcessCrystal(CrystalObject crystal)
    {
        Destroy(crystal.gameObject);

        yield return new WaitForSeconds(0.25f);

        StartCoroutine(CloseHatch());

        yield return new WaitForSeconds(1f);

        _machine.SetInsertedCrystal(crystal.GetCrystalColor());

        

    }

    public IEnumerator CloseHatch()
    {
        while (_hatch.transform.localRotation != _closeRotation)
        {
            yield return null;
            _hatch.transform.localRotation = Quaternion.RotateTowards(_hatch.transform.localRotation, _closeRotation, _speed * Time.deltaTime);
        }
        
        Debug.Log("Hatch closed");
    }

    public void ResetSlot()
    {
        Debug.Log("Reset Slot");
        StartCoroutine(OpenHatch());
    }

    public IEnumerator OpenHatch()
    {
        while (_hatch.transform.localRotation != _openRotation)
        {
            yield return null;
            _hatch.transform.localRotation = Quaternion.RotateTowards(_hatch.transform.localRotation, _openRotation, _speed * Time.deltaTime);
        }

        Debug.Log("Hatch opened");
    }
}
