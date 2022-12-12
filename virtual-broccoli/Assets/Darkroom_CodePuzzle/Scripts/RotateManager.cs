using System.Collections.Generic;
using UnityEngine;

public class RotateManager : MonoBehaviour
{
    [SerializeField]
    private Vector3 _rotatingAxis;

    private List<GameObject> _rotatingWheels;
    private List<int> _wheelMovements;

    private void Start()
    {
        _wheelMovements = new List<int>();
        _rotatingWheels = new List<GameObject>();
        if (_rotatingWheels.Count == 0)
        {
            GameObject[] foundWheels = GameObject.FindGameObjectsWithTag("Wheel");
            foreach (GameObject wheel in foundWheels)
            {
                Debug.Log("Add " + wheel.name);
                _rotatingWheels.Add(wheel);
                _wheelMovements.Add(0);
            }
        }

        if (_rotatingWheels.Count != 3)
        {
            Debug.Log("Wheels not assigned");
        }

        _rotatingAxis = transform.InverseTransformDirection(Vector3.back);
        _rotatingAxis = new Vector3(Mathf.Round(_rotatingAxis.x),
             Mathf.Round(_rotatingAxis.y),
             Mathf.Round(_rotatingAxis.z));
    }

    private void Update()
    {
        bool reverse = Input.GetKey(KeyCode.LeftShift);
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeMovement(0, reverse);
        }  else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeMovement(1, reverse);
        } else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangeMovement(2, reverse);
        }

        for (int i = 0; i < _wheelMovements.Count; i++)
        {
            int movement = _wheelMovements[i];
            if (movement != 0)
            {
                _rotatingWheels[i].transform.rotation *= Quaternion.Euler(movement * 0.5f * _rotatingAxis);
                foreach (Transform symbol in _rotatingWheels[i].transform)
                {
                    symbol.rotation *= Quaternion.Euler(-movement * 0.5f * _rotatingAxis);
                }
            }
        }
    }


    private void ChangeMovement(int wheelIndex, bool reverse)
    {
        int current = _wheelMovements[wheelIndex];
        int requested = reverse ? -1 : 1;
        _wheelMovements[wheelIndex] = current == requested ? 0 : requested;
    }
}
