using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle3_Manager : MonoBehaviour
{
    [SerializeField]
    private Transform _tower;

    [SerializeField]
    private Transform _finishBox;

    [SerializeField]
    private string[] _nameSeq = new string[4];

    [SerializeField]
    private int[] _valueSeq = new int[4];

    private Dictionary<string, int> _finishingSequence;

    private enum STATE { AWAITINTERACTION, STARTED, FINISHED };
    private STATE _gamestate;

    private void Start()
    {
        ResetTower();
        _finishingSequence = new Dictionary<string, int>();
        _finishingSequence.Add(_nameSeq[0], _valueSeq[0]);
        _finishingSequence.Add(_nameSeq[1], _valueSeq[1]);
        _finishingSequence.Add(_nameSeq[2], _valueSeq[2]);
        _finishingSequence.Add(_nameSeq[3], _valueSeq[3]);
        _gamestate = STATE.AWAITINTERACTION;
    }

    public void StartGame()
    {
        _gamestate = STATE.STARTED;
    }

    // Update is called once per frame
    void Update()
    {
        switch (_gamestate)
        {
            case STATE.AWAITINTERACTION:
                Debug.Log("awaiting");
                if (CheckStarted()) StartGame();
                return;

            case STATE.STARTED:
                Debug.Log("started");
                if (CheckFinished())
                {
                    _gamestate = STATE.FINISHED;
                    FinishGame();
                }
                break;

            case STATE.FINISHED:
                Debug.Log("finished");
                break;

            default:
                Debug.Log("error");
                break;
        }
    }

    private bool CheckStarted()
    {
        foreach (Transform towerChild in _tower)
        {
            if (towerChild.GetComponent<CalcHeight>().GetCurrentHeight() > 0) return true;
        }
        return false;
    }

    private bool CheckFinished()
    {
        bool state = false;
        foreach (Transform towerChild in _tower)
        {
            state = towerChild.GetComponent<CalcHeight>().GetCurrentHeight() == _finishingSequence[towerChild.name];
            if (!state) return state;
        }
        return state;
    }

    private void FinishGame()
    {
        _finishBox.GetChild(0).GetComponent<OpenBoxController>().setOpen(true);
    }

    private void ResetTower()
    {
        foreach (Transform towerChild in _tower)
        {
            towerChild.GetComponent<CalcHeight>().ResetHeight();
        }
    }
}
