using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishPuzzle_Manager : PuzzleTableManager
{
    [SerializeField]
    private SequenceController _triggerController;

    protected override void ResetComponents()
    {
        _triggerController.SetTriggered(false);
    }

    protected override bool CheckStarted()
    {
        return _triggerController.GetTriggered();
    }

    protected override bool CheckFinished()
    {
        foreach (bool value in _triggerController.GetLetterSequence().Values)
        {
            if (!value) return value;
        }
        return true;
    }
}
