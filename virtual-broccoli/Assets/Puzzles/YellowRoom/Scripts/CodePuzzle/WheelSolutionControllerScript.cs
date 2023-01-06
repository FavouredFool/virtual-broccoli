using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelSolutionControllerScript : MonoBehaviour
{
    [SerializeField]
    private List<Collider> _solutionParts;

    private List<Collider> _partsPositionedCorrectly = new();

    [SerializeField]
    private bool _solved = false;

    private void OnTriggerEnter(Collider other)
    {
        if (_solutionParts.Contains(other) && !_partsPositionedCorrectly.Contains(other))
        {
            Debug.Log(other.name + " entered right position");
            _partsPositionedCorrectly.Add(other);
            CheckCurrentState();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_solutionParts.Contains(other) && _partsPositionedCorrectly.Contains(other))
        {
            Debug.Log(other.name + " exited right position");
            _partsPositionedCorrectly.Remove(other);
            CheckCurrentState();
        }
    }

    private void CheckCurrentState()
    {
        _solved = _solutionParts.Count == _partsPositionedCorrectly.Count;
    }

}
