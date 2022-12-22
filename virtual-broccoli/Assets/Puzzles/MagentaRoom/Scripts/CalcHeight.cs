using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalcHeight : MonoBehaviour
{
    [SerializeField]
    private Transform _startPosition;

    private int _currentHeight;

    public void ResetHeight()
    {
        Vector3 position = gameObject.transform.position;
        gameObject.transform.position = new Vector3(position.x, _startPosition.position.y, position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("HeightIndex"))
        {
            switch (other.name)
            {
                case "Trigger 0":
                    SetCurrentHeight(0);
                    break;

                case "Trigger 1":
                    SetCurrentHeight(1);
                    break;

                case "Trigger 2":
                    SetCurrentHeight(2);
                    break;

                case "Trigger 3":
                    SetCurrentHeight(3);
                    break;

                case "Trigger 4":
                    SetCurrentHeight(4);
                    break;

                default:
                    break;
            }
        }
    }

    private void SetCurrentHeight(int height)
    {
        _currentHeight = height;
    }

    public int GetCurrentHeight()
    {
        return _currentHeight;
    }
}
