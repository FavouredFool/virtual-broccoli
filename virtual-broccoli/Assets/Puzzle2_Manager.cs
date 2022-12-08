using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle2_Manager : MonoBehaviour
{
    [SerializeField]
    private GameObject _indexTrigger;
    [SerializeField]
    private GameObject _startTrigger;

    [SerializeField]
    private ProgressMovementController _progressbarController;

    [SerializeField]
    private Transform _buttons;

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
        _progressbarController.ResetCurrentValue();
        _progressbarController.StartCheck();
        _indexTrigger.transform.position = _startTrigger.transform.position;
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
                if (CheckProgress())
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

    private bool CheckTriggered()
    {
        bool state = false;
        foreach (Transform button in _buttons)
        {
            state = button.GetComponent<PinController>().getActive();
            if (state) return state;
        }
        return state;
    }

    private bool CheckProgress()
    {
        return _progressbarController.getCurrentValue() >= 1;
    }

    private void FinishGame()
    {
        _finishBox.GetChild(0).GetComponent<OpenBoxController>().setOpen(true);
    }

}
