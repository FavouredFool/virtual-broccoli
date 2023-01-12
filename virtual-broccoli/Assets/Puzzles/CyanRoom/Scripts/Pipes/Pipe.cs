using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class Pipe : XRGrabInteractable
{
    [SerializeField] private Dictionary<string, GameObject> _neighborPipeBorders;

    [SerializeField] private GameObject _light = null;


    private GameObject _placedGrid = null;

    private GameObject _blueprint;


    private bool _validRotation;

    private void Start()
    {
        _neighborPipeBorders = new Dictionary<string, GameObject>();
        Transform blueprintTransform = transform.parent.gameObject.transform.Find("Blueprint");
        _blueprint = blueprintTransform != null ? blueprintTransform.gameObject : null;
        if (_light != null)
        {
            _light.SetActive(false);
        }
    }

    public bool IsDragged()
    {
        return isSelected && tag.Contains("Pipe");
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (CompareTag("Ventil"))
        {
            return;
        }
        base.OnSelectEntered(args);
        Quaternion startRotation = transform.rotation;
        _blueprint.transform.rotation = startRotation;
        transform.rotation = startRotation;
        _blueprint.GetComponent<MeshRenderer>().enabled = true;
        _validRotation = false;
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Rigidbody>().isKinematic = true;

        if (CompareTag("AngledPipeRotateOnly") || CompareTag("StraightPipeRotateOnly"))
        {
            _blueprint.SetActive(true);
        }
        ChangeLightState(false);
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        if (_placedGrid != null)
        {
            if (_validRotation)
            {
                transform.SetPositionAndRotation(_blueprint.transform.position,
                    _blueprint.transform.rotation);
                GetComponent<Rigidbody>().useGravity = false;
                GetComponent<Rigidbody>().isKinematic = true;
                _blueprint.GetComponent<MeshRenderer>().enabled = false;
            }
        } else
        {
            if (CompareTag("AngledPipe") || CompareTag("StraightPipe"))
            {
                GetComponent<Rigidbody>().useGravity = true;
            }
            GetComponent<Rigidbody>().isKinematic = false;
        }
        PipeManager.CheckFinalState();
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);
        if (isSelected)
        {
            if (PipeManager.CheckValidRotation(gameObject))
            {
                _validRotation = true;
                Quaternion newPositionRotation = Quaternion.Euler(GetRotationAngle(), 90, 0);
                if (!_blueprint.transform.rotation.Equals(newPositionRotation))
                {
                    _blueprint.transform.rotation = newPositionRotation;
                }
            }
            else
            {
                _validRotation = false;
            }
        }
    }

    public void ChangeLightState(bool newLightState)
    {
        if (_light != null && _light.activeSelf != newLightState)
        {
            _light.SetActive(newLightState);
        }
    }

    public void SetPlaceGrid(GameObject gridPlacement)
    {
        _placedGrid = gridPlacement;
    }

    public GameObject GetPlaceGrid()
    {
        return _placedGrid;
    }

    public Dictionary<string, GameObject> GetNeighbors()
    {
        return _neighborPipeBorders;
    }

    public void AddNeighbor(string key, GameObject gameObject)
    {
        _neighborPipeBorders.TryAdd(key, gameObject);
    }

    public void RemoveNeighbor(string key, GameObject gameObject)
    {
        if (_neighborPipeBorders.ContainsValue(gameObject))
        {
            _neighborPipeBorders.Remove(key);
        }
    }

    public void RemoveNeighbors()
    {
        _neighborPipeBorders.Clear();
    }

    public GameObject GetBlueprint()
    {
        return _blueprint;
    }

    private float GetRotationAngle()
    {
        // Schritt 1: Herausfinden ob gespiegelt oder nicht

        bool angleSwitch = Vector3.SignedAngle(transform.right, Vector3.right, Vector3.up) > 0;

        float angleTop = Vector3.SignedAngle(transform.up, Vector3.up, Vector3.forward);

        Debug.Log(angleTop);

        if (CompareTag("StraightPipe") || CompareTag("StraightPipeRotateOnly"))
        {
            if (angleTop < -135 || angleTop > 135)
            {
                return 0;
            }
            else if (angleTop < -45)
            {
                return 90;
            }
            else if (angleTop < 45)
            {
                return 180;
            }
            else
            {
                return 270;
            }
        }
        else
        {
            if (angleSwitch)
            {
                if (angleTop < -135 || angleTop > 135)
                {
                    return 270;
                }
                else if (angleTop < -45)
                {
                    return 0;
                }
                else if (angleTop < 45)
                {
                    return 90;
                }
                else
                {
                    return 180;
                }
            }
            else
            {
                if (angleTop < -135 || angleTop > 135)
                {
                    return 180;
                }
                else if (angleTop < -45)
                {
                    return 270;
                }
                else if (angleTop < 45)
                {
                    return 0;
                }
                else
                {
                    return 90;
                }
            }
        }
    }
}
