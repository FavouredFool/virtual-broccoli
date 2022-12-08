using System.Collections.Generic;
using UnityEngine;

public class MovePipes : MonoBehaviour
{
    [SerializeField]
    private float _rotationAngle;

    [SerializeField]
    private int _threshold;

    [SerializeField]
    private GameObject _puzzleStart;

    [SerializeField]
    private GameObject _puzzleEnd;

    [SerializeField]
    private bool _solved = false;

    private GameObject _selectedObject;

    private bool _validRotation = false;

    private readonly int _comparisonValue = 45;

    private float _moving = 0f;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_selectedObject == null)
            {
                RaycastHit hit = CastRay();

                if (hit.collider != null)
                {
                    if (!(hit.collider.CompareTag("Pipe") || hit.collider.CompareTag("PipeRotateOnly")))
                    {
                        return;
                    }

                    _validRotation = false;
                    _selectedObject = hit.collider.gameObject;
                    //set depth for mouse movement (not needed in VR)
                    if (_selectedObject.CompareTag("Pipe"))
                    {
                        _selectedObject.transform.position = new Vector3(
                            _selectedObject.transform.position.x,
                            _selectedObject.transform.position.y,
                            -1f);
                    }

                    _selectedObject.GetComponent<Rigidbody>().useGravity = true;
                    _selectedObject.GetComponent<Rigidbody>().isKinematic = true;

                    Quaternion startRotation = Quaternion.Euler(_selectedObject.transform.rotation.eulerAngles.x, 90, 0);
                    _selectedObject.transform.rotation = startRotation;
                    GameObject blueprint = _selectedObject.GetComponent<Pipe>().GetBlueprint();
                    blueprint.transform.rotation = startRotation;
                    blueprint.GetComponent<MeshRenderer>().enabled = true;
                    if (_selectedObject.CompareTag("PipeRotateOnly"))
                    {
                        blueprint.SetActive(true);
                    }

                    //Check end state because of pickup
                    if (_solved)
                    {
                        CheckFinalState();
                    }
                }
            } else
            {
                Pipe pipeComponent = _selectedObject.GetComponent<Pipe>();
                if (pipeComponent.GetPlaceGrid() != null)
                {
                    if (_validRotation)
                    {
                        GameObject blueprint = pipeComponent.GetBlueprint();
                        _selectedObject.transform.SetPositionAndRotation(blueprint.transform.position,
                            pipeComponent.GetBlueprint().transform.rotation);
                        _selectedObject.GetComponent<Rigidbody>().useGravity = false;
                        blueprint.GetComponent<MeshRenderer>().enabled = false;
                    }
                    else
                    {
                        return;
                    }
                } else
                {
                    if (_selectedObject.CompareTag("Pipe"))
                    {
                        _selectedObject.GetComponent<Rigidbody>().useGravity = true;
                    }
                    _selectedObject.GetComponent<Rigidbody>().isKinematic = false;
                }

                CheckFinalState();
                _selectedObject = null;
            }
        }

        HandleMovementAndRotation();
    }

    private void CheckFinalState()
    {
        List<GameObject> checkedPipes = new() { _puzzleStart };
        GameObject current = null;
        Dictionary<string, GameObject> startNeighbors = _puzzleStart.GetComponent<Pipe>().GetNeighbors();
        if (startNeighbors.Count == 1)
        {
            foreach (KeyValuePair<string, GameObject> pair in startNeighbors)
            {
                Debug.Log("Start gefunden");
                current = pair.Value;
                checkedPipes.Add(current);
                break;
            }
            TraversePipeNeighbors(current, checkedPipes);
            Debug.Log("________________________________________________________________________________________________");
        }
    }

    private void TraversePipeNeighbors(GameObject current, List<GameObject> checkedPipes)
    {
        GameObject checkedNeighborPipe;
        foreach (KeyValuePair<string, GameObject> pair in current.GetComponent<Pipe>().GetNeighbors())
        {
            checkedNeighborPipe = pair.Value;
            if (!checkedPipes.Contains(checkedNeighborPipe))
            {
                Debug.Log("Pipe: " + current.name + " -> neighbor: " + checkedNeighborPipe.name);
                if (checkedNeighborPipe.CompareTag("Ventil"))
                {
                    _solved = true;
                    Debug.Log("Ende");
                    return;
                }
                checkedPipes.Add(checkedNeighborPipe);
                TraversePipeNeighbors(checkedNeighborPipe, checkedPipes);
            }
        }
    }

    private float GetRotationAngle()
    {
        float angleRight = Vector3.Angle(_selectedObject.transform.InverseTransformDirection(Vector3.right), Vector3.up);
        float angleTop = Vector3.Angle(_selectedObject.transform.InverseTransformDirection(Vector3.up), Vector3.up);

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

    private RaycastHit CastRay()
    {
        Vector3 screenMousePosFar = new(
            Input.mousePosition.x, 
            Input.mousePosition.y, 
            Camera.main.farClipPlane);

        Vector3 screenMousePosNear = new(
            Input.mousePosition.x, 
            Input.mousePosition.y, 
            Camera.main.nearClipPlane);

        Vector3 worldMousePosFar = Camera.main.ScreenToWorldPoint(screenMousePosFar);
        Vector3 worldMousePosNear = Camera.main.ScreenToWorldPoint(screenMousePosNear);

        Physics.Raycast(worldMousePosNear, worldMousePosFar - worldMousePosNear, out RaycastHit hit);

        return hit;
    }

    private void HandleMovementAndRotation()
    {
        if (_selectedObject != null)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                _moving = _moving == 1 ? 0 : 1;
            }
            if (Input.GetKeyDown(KeyCode.T))
            {
                _moving = _moving == -1 ? 0 : -1;
            }

            if (_moving == 1 || _moving == -1)
            {
                Vector3 res = _selectedObject.transform.InverseTransformDirection(Vector3.forward);
                _selectedObject.transform.rotation *= Quaternion.AngleAxis(_moving * _rotationAngle, res);
            }


            if (_selectedObject.CompareTag("Pipe"))
            {
                Vector3 pos = new(Input.mousePosition.x, Input.mousePosition.y,
                    Camera.main.WorldToScreenPoint(_selectedObject.transform.position).z);
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(pos);
                _selectedObject.transform.position = new Vector3(worldPosition.x, worldPosition.y, _selectedObject.transform.position.z);
            }

            if (CheckValidRotation())
            {
                _validRotation = true;
                Quaternion newPositionRotation = Quaternion.Euler(GetRotationAngle(), 90, 0);
                GameObject blueprint = _selectedObject.GetComponent<Pipe>().GetBlueprint();
                if (!blueprint.transform.rotation.Equals(newPositionRotation))
                {
                    blueprint.transform.rotation = newPositionRotation;
                }
                
            } else
            {
                _validRotation = false;
            }
        }
    }

    private bool CheckValidRotation()
    {
        float y = _selectedObject.transform.localEulerAngles.y;
        float z = _selectedObject.transform.localEulerAngles.z;

        float restAngleY = y % _comparisonValue;
        float restAngleZ = z % _comparisonValue;

        if (!(restAngleY <= _threshold || restAngleY >= (_comparisonValue - _threshold)))
        {
            Debug.Log("invalid Y: " + y);
            return false;
        }

        if (!(restAngleZ <= _threshold || restAngleZ >= (_comparisonValue - _threshold)))
        {
            Debug.Log("invalid Z: " + z);
            return false;
        }

        int correctedY = (int) (restAngleY <= _threshold ? y - restAngleY : _comparisonValue * ((((int)y) / _comparisonValue) + 1));
        int correctedZ = (int) (restAngleZ <= _threshold ? z - restAngleZ : _comparisonValue * ((((int)z) / _comparisonValue) + 1));
        return CheckAbsoluteAngleDifference(correctedY, correctedZ);
    }

    private bool CheckAbsoluteAngleDifference(int firstAngle, int secondAngle)
    {
        int max = Mathf.Max(firstAngle, secondAngle);
        int min = Mathf.Min(firstAngle, secondAngle);
        if (max <= 180 || min < 90)
        {
            firstAngle = firstAngle >= 180 ? firstAngle -= 360 : firstAngle;
            secondAngle = secondAngle >= 180 ? secondAngle -= 360 : secondAngle;
        }

        return Mathf.Abs(firstAngle - secondAngle) == 90;
    }
}