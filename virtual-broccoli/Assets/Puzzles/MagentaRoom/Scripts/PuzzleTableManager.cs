using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleTableManager: MonoBehaviour
{
    [SerializeField]
    protected Transform _finishBox;

    [SerializeField]
    protected GameObject[] _letterChips = new GameObject[2];

    protected enum STATE { AWAITINTERACTION, STARTED, FINISHED };
    protected STATE _gamestate;

    void Start()
    {
        ResetComponents();
        StartExtend();
        SwitchState(STATE.AWAITINTERACTION);
        foreach (GameObject chip in _letterChips)
        {
            if (chip) chip.SetActive(false);
        }
    }

    protected virtual void StartExtend() { }

    void Update()
    {
        switch (_gamestate)
        {
            case STATE.AWAITINTERACTION:
                Debug.Log("awaiting");
                if (CheckStarted()) StartGame();
                break;

            case STATE.STARTED:
                Debug.Log("started");
                GameProgressing();
                if (CheckFinished())
                {
                    FinishGame();
                    _gamestate = STATE.FINISHED;
                }
                break;

            case STATE.FINISHED:
                Debug.Log("finished");
                break;

            default:
                Debug.Log("error");
                break;
        }
        RegularCheck();
    }

    private void SwitchState(STATE state)
    {
        _gamestate = state;
    }

    // Is called at the Start methode and at the Start of the game (after the first interaction).
    protected virtual void ResetComponents() { }

    protected virtual bool CheckStarted()
    {
        return false;
    }

    private void StartGame()
    {
        StartGameExtend();
        SwitchState(STATE.STARTED);
    }

    // Extends the Method called at the start of the game (after the first interaction
    protected virtual void StartGameExtend() { }

    protected virtual void GameProgressing() { }

    protected virtual void RegularCheck() { }

    protected virtual bool CheckFinished()
    {
        return false;
    }

    protected void FinishGame()
    {
        _finishBox.GetChild(0).GetComponent<OpenBoxController>().setOpen(true);
        foreach(GameObject chip in _letterChips)
        {
            if (chip) chip.SetActive(!chip.activeSelf);
        }
    }
}
