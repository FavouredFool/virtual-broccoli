using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour
{

    [SerializeField] private float _speed = 1.0f;
    [SerializeField] private float _height = 0.05f;

    private Vector3 _startPosition;
    private bool _stop;

    void Start()
    {
        _startPosition = transform.position;
    }

    void Update()
    {
        if(!_stop)
        {
            // Calculate a new position above the starting position based on a sine wave 
            float newYPos = Mathf.Sin(Time.time * _speed) * _height + _startPosition.y;

            // Set the objects new position 
            transform.position = new Vector3(_startPosition.x, newYPos, _startPosition.z);
        }
    }

    public void SetStop(bool stop)
    {
        _stop = stop;
    }
}
