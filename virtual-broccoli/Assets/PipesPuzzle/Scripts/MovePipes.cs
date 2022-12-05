using System.Collections;
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

    private Vector3 _preferredRotationAngles;

    private bool _validRotation = false;

    private Quaternion _preparedPositionRotation;

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
                    if (!hit.collider.CompareTag("Pipe"))
                    {
                        return;
                    }

                    _validRotation = false;
                    _selectedObject = hit.collider.gameObject;
                    //set depth for mouse movement (not needed in VR)
                    _selectedObject.transform.position = new Vector3(
                        _selectedObject.transform.position.x,
                        _selectedObject.transform.position.y,
                        -1f);

                    _selectedObject.GetComponent<Rigidbody>().useGravity = true;
                    _selectedObject.GetComponent<Rigidbody>().isKinematic = true;

                    Quaternion startRotation = Quaternion.Euler(0, 90, 0);
                    _selectedObject.transform.rotation = startRotation;
                    _selectedObject.GetComponent<Pipe>().GetBlueprint().transform.rotation = startRotation;
                    if (_solved)
                    {
                        CheckFinalState();
                    }
                }
            } else
            {
                Pipe pipeComponent = _selectedObject.GetComponent<Pipe>();
                Collider gridPlacement = pipeComponent.GetPlaceGrid();
                if (gridPlacement != null)
                {
                    if (_validRotation)
                    {
                        _selectedObject.transform.SetPositionAndRotation(gridPlacement.transform.position,
                            pipeComponent.GetBlueprint().transform.rotation);
                        //UpdateRotation(gridPlacement);
                        _selectedObject.GetComponent<Rigidbody>().useGravity = false;
                    }
                    else
                    {
                        return;
                    }
                } else
                {
                    _selectedObject.GetComponent<Rigidbody>().useGravity = true;
                    _selectedObject.GetComponent<Rigidbody>().isKinematic = false;
                }

                pipeComponent.GetBlueprint().SetActive(false);
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
            Debug.Log(checkedNeighborPipe.name);
            if (checkedNeighborPipe != current && !checkedPipes.Contains(checkedNeighborPipe))
            {
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

    private void UpdateRotation(Collider collider)
    {
        if (collider != null && _selectedObject != null)
        {
            float rotationX;

            float angleRight = Vector3.Angle(_selectedObject.transform.InverseTransformDirection(Vector3.right), Vector3.up);
            float angleTop = Vector3.Angle(_selectedObject.transform.InverseTransformDirection(Vector3.up), Vector3.up);

            //Debug.Log("Angle horizontal: " + angleRight);
            //Debug.Log("Angle vertical: " + angleTop);

            if (angleTop < 45)
            {
                Debug.Log("Top");
                rotationX = 0;
            }
            else if (angleTop > 135)
            {
                Debug.Log("Bottom");
                rotationX = 180;
            }
            else if (angleRight < 45)
            {
                Debug.Log("Right");
                rotationX = 90;
            }
            else {
                Debug.Log("Left");
                rotationX = 270;
            }
            _selectedObject.transform.rotation = Quaternion.Euler(rotationX, 90, 0);
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

            if (Input.GetKeyDown(KeyCode.Y))
            {
                Vector3 res = _selectedObject.transform.InverseTransformDirection(Vector3.up);
                _selectedObject.transform.rotation *= Quaternion.AngleAxis(6, res);
            }
            if (Input.GetKeyDown(KeyCode.Z))
            {
                Vector3 res = _selectedObject.transform.InverseTransformDirection(Vector3.right);
                _selectedObject.transform.rotation *= Quaternion.AngleAxis(6, res);
            }

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

            Vector3 pos = new(Input.mousePosition.x,
                Input.mousePosition.y,
                Camera.main.WorldToScreenPoint(_selectedObject.transform.position).z);
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(pos);
            _selectedObject.transform.position = new Vector3(worldPosition.x, worldPosition.y, _selectedObject.transform.position.z);

            if (!CheckValidRotation())
            {
                _validRotation = false;
            } else
            {
                Debug.Log("rotation: " + _preferredRotationAngles);
                float x = _preferredRotationAngles.x;
                if (x < 45)
                {
                    x = 0;
                } else if (x < 135) 
                {
                    x = 90;
                } else if (x < 225)
                {
                    x = 180;
                } else
                {
                    x = 270;
                }
                Quaternion newPositionRotation = Quaternion.Euler(x, _preferredRotationAngles.y, _preferredRotationAngles.z);
                GameObject blueprint = _selectedObject.GetComponent<Pipe>().GetBlueprint();
                if (!blueprint.transform.rotation.Equals(newPositionRotation))
                {
                    blueprint.transform.rotation = newPositionRotation;
                }
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
        int correctedZ = (int) (restAngleZ <= _threshold ? z - restAngleZ : _comparisonValue * ((((int)y) / _comparisonValue) + 1));

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

        float difference = Mathf.Abs(firstAngle - secondAngle);
        if (difference != 90)
        {
            return false;
        }

        Vector3 preferredAngles = new(_selectedObject.transform.localEulerAngles.x, firstAngle, secondAngle);
        _validRotation = true;
        if (!Equals(preferredAngles, _preferredRotationAngles))
        {
            _preferredRotationAngles = preferredAngles;
        }
        return true;
    }
}
