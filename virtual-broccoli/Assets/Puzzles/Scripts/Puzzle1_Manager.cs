using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle1_Manager : MonoBehaviour
{
    [SerializeField]
    private Transform[] _boxes = new Transform[2];
    [SerializeField]
    private Transform _finishBox;

    private enum STATE { AWAITINTERACTION, STARTED, FINISHED };
    private STATE _gamestate;

    // Start is called before the first frame update
    void Start()
    {
        _gamestate = STATE.AWAITINTERACTION;
    }

    public void StartGame()
    {
        ResetPins(_boxes);
        _gamestate = STATE.STARTED;
    }

    // Update is called once per frame
    void Update()
    {
        switch (_gamestate)
        {
            case STATE.AWAITINTERACTION:
                Debug.Log("awaiting");
                if (CheckTriggered()) StartGame();
                return;

            case STATE.STARTED:
                Debug.Log("started");
                if (CheckPins())
                {
                    _gamestate = STATE.FINISHED;
                    FinishGame();
                }
                CheckDoors();
                break;

            case STATE.FINISHED:
                Debug.Log("finished");
                break;

            default:
                Debug.Log("error");
                break;
        }


    }

    private void CheckDoors()
    {
        bool state;
        foreach (Transform childTransform in _boxes)
        {
            state = childTransform.GetChild(0).GetChild(0).GetComponent<OpenBoxController>().getClosing();
            if (state) ResetPins(new Transform[]{childTransform});
        }
    }

    private bool CheckTriggered()
    {
        bool state = false;
        foreach (Transform box in _boxes)
        {
            state = box.GetChild(1).GetChild(0).GetComponent<OpenBoxTrigger>().getTriggered();
            if (state) return state;
        }
        return state;
    }

    private bool CheckPins()
    {
        bool state = true;
        foreach (Transform childTransform in _boxes)
        {
            foreach (Transform pin in childTransform.GetChild(0).GetChild(1))
            {
                state = pin.GetComponent<PinController>().getActive();
                if (!state) return state;
            } 
        }
        return state;
    }

    private void FinishGame()
    {
        ResetPins(_boxes);
        _finishBox.GetChild(0).GetComponent<OpenBoxController>().setOpen(true);
    }

    private void ResetPins(Transform[] boxes)
    {
        foreach (Transform box in boxes)
        {
            foreach (Transform childTransform in box.GetChild(0).GetChild(1))
            {
                childTransform.GetComponent<PinController>().setActive(false);
            }
        }
    }
}
