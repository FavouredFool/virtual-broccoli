using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressMovementController : MonoBehaviour
{
    [SerializeField]
    private Transform _buttons;
    private int _oldActiveButtons;
    private List<string> _activatedButtons;
    private List<string> _holdButtons;

    [SerializeField]
    private float _moveTime;
    private float _elapsedTime;

    private Material _mat;
    private float _newVal;
    private float _currentVal;

    private float _holdStep;
    private float _holdVal;
    private enum STATE { ONHOLD, PROGRESSING};
    private STATE _state;
    private bool _started;

    // Start is called before the first frame update
    void Start()
    {
        _mat = gameObject.GetComponent<MeshRenderer>().material;
        _currentVal = _mat.GetFloat("_ProgressValue");
        _newVal = _currentVal;
        _state = STATE.ONHOLD;
        _holdVal = 0;
        _holdStep = 0.25f;
        _holdButtons = new List<string>();
        _activatedButtons = new List<string>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_started)
        {
            switch (_state)
            {
                case STATE.PROGRESSING:
                    progressing();
                    if (_currentVal == _newVal) switchState(STATE.ONHOLD);
                    break;

                case STATE.ONHOLD:
                default:
                    break;
            }

            checkButtons();
        }
    }

    private void progressing()
    {
        _elapsedTime += Time.deltaTime;
        float percentageTime = _elapsedTime / _moveTime;
        _currentVal = Mathf.Lerp(_currentVal, _newVal, Mathf.SmoothStep(0, 1, percentageTime));
        _mat.SetFloat("_ProgressValue", _currentVal);

        if (_currentVal == 1) _started = false;
    }

    private void checkButtons()
    {
        int newActiveButtons = 0;
        foreach (Transform button in _buttons)
        {
            ToggleButton(button.name, button.GetComponent<Puzzle2ButtonController>().GetActive());

            if (_activatedButtons.Contains(button.name) || _holdButtons.Contains(button.name))
            {
                newActiveButtons += 1;
            }
        }

        if (_oldActiveButtons != newActiveButtons)
        {

            switchState(STATE.PROGRESSING);
            _oldActiveButtons = newActiveButtons;
            moveProgress();
        }
    }

    private void switchState(STATE state)
    {
        _state = state;
    }

    public void moveProgress()
    {
        _elapsedTime = 0;
        if (_currentVal == _holdVal && _oldActiveButtons * _holdStep <= _holdVal)
        {
            switchState(STATE.ONHOLD);
        } else
        {
            _newVal = _oldActiveButtons * (1f / _buttons.childCount);
            if (_currentVal > _holdVal && _newVal < _holdVal) _newVal = _holdVal;
            if (_newVal > 0.99f) _newVal = 1f;
            if (_newVal < 0.01f) _newVal = 0f;
        }
    }

    public void SetHoldValue(string trigger)
    {
        bool resetted = false;
        int triggerNumber;
        switch (trigger)
        {
            case "IndexTrigger 0":
            default:
                triggerNumber = 0;
                resetted = true;
                break;

            case "IndexTrigger 1":
                triggerNumber = 1;
                break;

            case "IndexTrigger 2":
                triggerNumber = 2;
                break;

            case "IndexTrigger 3":
                triggerNumber = 3;
                break;

            case "IndexTrigger 4":
                triggerNumber = 4;
                break;
        }

        _holdVal = triggerNumber * _holdStep;

        if(resetted || _currentVal < _holdVal)
        {
            _holdButtons.Clear();
            _activatedButtons.Clear();
        } else
        {
            int size = _holdButtons.Count;
            if (size > triggerNumber && _holdButtons.Count > 0)
            {
                _holdButtons.RemoveAt(size - 1);
            }

            if (size < triggerNumber && _activatedButtons.Count > 0 && !_holdButtons.Contains(_activatedButtons[0]))
            {
                _holdButtons.Add(_activatedButtons[0]);
                _activatedButtons.RemoveAt(0);
            }
        }
    }

    public void ToggleButton(string name, bool entered)
    {
        if (entered)
        {
            if (!_activatedButtons.Contains(name)) _activatedButtons.Add(name);
        } else
        {
            if (_activatedButtons.Contains(name)) _activatedButtons.Remove(name);
        }
        
    }

    public void SetStarted()
    {
        _started = true;
    }

    public float getCurrentValue()
    {
        return _currentVal;
    }

    public float ResetCurrentValue()
    {
        return _currentVal = 0f;
    }
}
