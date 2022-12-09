using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using static ColorMachineSequence;

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

    [SerializeField]
    private bool _insertCrystal = false;

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

    public void Update()
    {
        if (_insertCrystal)
        {
            _insertCrystal = false;
            _machine.SetInsertedCrystal(CrystalColor.KEY);
        }
    }

    public void CrystalEntered()
    {
        if (_machine.GetMachineState() != MachineState.AWAITCRYSTAL)
        {
            Debug.LogWarning("MACHINE IS IN WRONG STATE");
            return;
        }

        Debug.Log("CRYSTAL ENTERED");
        IXRSelectInteractable crystalXR = _socket.GetOldestInteractableSelected();

        CrystalObject crystal = crystalXR.transform.GetComponent<CrystalObject>();

        Destroy(crystal.gameObject);

        _machine.SetInsertedCrystal(crystal.GetCrystalColor());
    }


    public bool CloseLidPerFrame()
    {
        _hatch.transform.localRotation = Quaternion.RotateTowards(_hatch.transform.localRotation, _closeRotation, _speed * Time.deltaTime);

        return _hatch.transform.localRotation == _closeRotation;
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
