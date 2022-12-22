using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinController : MonoBehaviour
{
    [SerializeField]
    private GameObject _light;

    [SerializeField]
    private bool _active;

    private void Awake()
    {
        setActive(false);
    }

    public bool getActive()
    {
        return _active;
    }

    [SerializeField]
    public void setActive(bool active)
    {
        _active = active;
        activateLight(_active);
    }

    private void activateLight(bool activate)
    {
        _light.SetActive(activate);
    }

    private void OnTriggerEnter(Collider other)
    {
        setActive(!_active);
    }
}
