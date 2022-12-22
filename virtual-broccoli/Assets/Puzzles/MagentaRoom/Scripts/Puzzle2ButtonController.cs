using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle2ButtonController : MonoBehaviour
{
    [SerializeField]
    private ProgressMovementController _progressController;

    [SerializeField]
    private bool _active;

    private bool _triggered;

    private void Start()
    {
        _active = false;
        _triggered = false;
    }

    public bool GetActive()
    {
        return _active;
    }

    public void SetActive(bool active)
    {
        _active = active;
    }

    private void Activate(bool active)
    {
        _progressController.ToggleButton(name, active);
    }

    private void Update()
    {
        if(!_triggered && _active)
        {
            Activate(_active);
            _triggered = true;
        }

        if (!_active && _triggered)
        {
            _triggered = false;
            Activate(_active);
        }
    }
}
