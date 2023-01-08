using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class Pipe : XRGrabInteractable
{
    [SerializeField]
    private Dictionary<string, GameObject> _neighborPipeBorders;

    private GameObject _placedGrid = null;

    private GameObject _blueprint;

    private bool _validRotation;

    private void Start()
    {
        _neighborPipeBorders = new Dictionary<string, GameObject>();
        Transform blueprintTransform = transform.parent.gameObject.transform.Find("Blueprint");
        _blueprint = blueprintTransform != null ? blueprintTransform.gameObject : null;
    }

    public bool IsDragged()
    {
        return isSelected && (CompareTag("PipeRotateOnly") || CompareTag("Pipe"));
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (CompareTag("Ventil"))
        {
            return;
        }
        base.OnSelectEntered(args);
        Quaternion startRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 90, 0);
        _blueprint.transform.rotation = startRotation;
        transform.rotation = startRotation;
        _blueprint.GetComponent<MeshRenderer>().enabled = true;
        _validRotation = false;
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Rigidbody>().isKinematic = true;

        if (CompareTag("PipeRotateOnly"))
        {
            _blueprint.SetActive(true);
        }
        MovePipes.CheckFinalState();
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
            if (CompareTag("Pipe"))
            {
                GetComponent<Rigidbody>().useGravity = true;
            }
            GetComponent<Rigidbody>().isKinematic = false;
        }
        MovePipes.CheckFinalState();
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);
        if (isSelected)
        {
            if (MovePipes.CheckValidRotation(gameObject))
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
        float angleRight = Vector3.Angle(transform.InverseTransformDirection(Vector3.right), Vector3.up);
        float angleTop = Vector3.Angle(transform.InverseTransformDirection(Vector3.up), Vector3.up);

        if (angleTop < 45)
        {
            return 0;
        }
        else if (angleTop > 135)
        {
            return 180;
        }
        else if (angleRight < 45)
        {
            return 90;
        }
        else
        {
            return 270;
        }
    }
}
