using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle2_Manager : PuzzleTableManager
{
    [SerializeField]
    private GameObject _indexTrigger;
    [SerializeField]
    private GameObject _startTrigger;

    [SerializeField]
    private ProgressMovementController _progressbarController;

    [SerializeField]
    private Transform _buttons;

    protected override void ResetComponents()
    {
        _progressbarController.ResetCurrentValue();
        _indexTrigger.transform.position = _startTrigger.transform.position;
    }

    protected override void StartGameExtend()
    {
        _progressbarController.SetStarted();
    }

    protected override bool CheckStarted()
    {
        bool state = false;
        foreach (Transform button in _buttons)
        {
            state = button.GetComponent<Puzzle2ButtonController>().GetActive();
            if (state) return state;
        }
        return state;
    }

    protected override bool CheckFinished()
    {
        return _progressbarController.getCurrentValue() >= 1;
    }
}
