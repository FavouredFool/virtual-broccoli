using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressMovementController : MonoBehaviour
{
    [SerializeField]
    private Transform _pins;
    private int _oldActivePins;

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

            checkPins();
        }
    }

    private void checkPins()
    {
        int newActivePins = 0;
        foreach (Transform pin in _pins)
        {
            if (pin.GetComponent<PinController>().getActive()) newActivePins += 1;
        }

        if (_oldActivePins != newActivePins)
        {

            switchState(STATE.PROGRESSING);
            _oldActivePins = newActivePins;
            _elapsedTime = 0;
            moveProgress();
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

    private void switchState(STATE state)
    {
        _state = state;
    }

    public void moveProgress()
    {
        if(_currentVal == _holdVal && _oldActivePins * _holdStep <= _holdVal)
        {
            switchState(STATE.ONHOLD);
        } else
        {
            _newVal = _oldActivePins * (1f / _pins.childCount);
            if (_currentVal > _holdVal && _newVal < _holdVal) _newVal = _holdVal;
            //if (_currentVal < _holdValue && _newVal < _holdValue) _newVal =
            if (_newVal > 0.99f) _newVal = 1f;
            if (_newVal < 0.01f) _newVal = 0f;
            //if (_state.Equals(STATE.ONHOLD)) switchState(STATE.PROGRESSING);
        }
    }

    public void setHoldValue(string trigger)
    {
        switch (trigger)
        {
            case "IndexTrigger 0":
            default:
                _holdVal = 0 * _holdStep;
                break;

            case "IndexTrigger 1":
                _holdVal = 1 *_holdStep;
                break;

            case "IndexTrigger 2":
                _holdVal = 2 * _holdStep;
                break;

            case "IndexTrigger 3":
                _holdVal = 3 * _holdStep;
                break;

            case "IndexTrigger 4":
                _holdVal = 4 * _holdStep;
                break;
        }

        if (_currentVal != _holdVal)
        {
            switchState(STATE.PROGRESSING);
            _elapsedTime = 0;
            moveProgress();
        }
    }

    public void StartCheck()
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
