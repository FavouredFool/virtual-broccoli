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

    public void setActive(bool active)
    {
        _active = active;
        activateLight(_active);
    }

    private void activateLight(bool activate)
    {
        _light.SetActive(activate);

        Material mat = gameObject.GetComponent<MeshRenderer>().material;
        if (activate)
        {
            mat.EnableKeyword("_EMISSION");
        }
        else
        {
            mat.DisableKeyword("_EMISSION");
        }
        DynamicGI.UpdateEnvironment();
    }

    private void OnTriggerEnter(Collider other)
    {
        setActive(!_active);
    }
}
