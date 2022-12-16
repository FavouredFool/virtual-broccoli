using System.Collections.Generic;
using UnityEngine;

public class RotateManager : MonoBehaviour
{
    [SerializeField]
    private Vector3 _rotatingAxis;
    [SerializeField]
    private List<float> _rotationAngles;
    [SerializeField]
    private List<GameObject> _rotatingWheels;

    private readonly List<int> _wheelMovements = new();

    private void Start()
    {
        _rotatingWheels.ForEach(wheel => _wheelMovements.Add(0));
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
    }


    private void ChangeMovement(int wheelIndex, bool reverse)
    {
        int current = _wheelMovements[wheelIndex];
        int requested = reverse ? -1 : 1;
        _wheelMovements[wheelIndex] = current == requested ? 0 : requested;

        int movement = _wheelMovements[wheelIndex];
        if (movement != 0) {
            _rotatingWheels[wheelIndex].transform.rotation *= Quaternion.Euler(movement * _rotationAngles[wheelIndex] * _rotatingAxis);
            foreach (Transform symbol in _rotatingWheels[wheelIndex].transform)
            {
               symbol.rotation *= Quaternion.Euler(-movement * _rotationAngles[wheelIndex] * _rotatingAxis);
            }
        }
    }
}
