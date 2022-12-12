using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle3_Manager : PuzzleTableManager
{
    [SerializeField]
    private Transform _tower;

    [SerializeField]
    private string[] _nameSeq = new string[4];

    [SerializeField]
    private int[] _valueSeq = new int[4];

    private Dictionary<string, int> _finishingSequence;

    protected override void StartExtend()
    {
        _finishingSequence = new Dictionary<string, int>();
        for (int i = 0; i < _nameSeq.Length; i++)
        {
            _finishingSequence.Add(_nameSeq[i], _valueSeq[i]);
        }
    }

    protected override bool CheckStarted()
    {
        foreach (Transform towerChild in _tower)
        {
            if (towerChild.GetComponent<CalcHeight>().GetCurrentHeight() > 0) return true;
        }
        return false;
    }

    protected override bool CheckFinished()
    {
        bool state = false;
        foreach (Transform towerChild in _tower)
        {
            state = towerChild.GetComponent<CalcHeight>().GetCurrentHeight() == _finishingSequence[towerChild.name];
            if (!state) return state;
        }
        return state;
    }

    protected override void ResetComponents()
    {
        foreach (Transform towerChild in _tower)
        {
            towerChild.GetComponent<CalcHeight>().ResetHeight();
        }
    }
}
