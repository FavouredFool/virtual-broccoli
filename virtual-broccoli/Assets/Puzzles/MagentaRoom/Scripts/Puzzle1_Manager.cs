using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle1_Manager : PuzzleTableManager
{
    [SerializeField]
    private Transform[] _boxes = new Transform[2];

    protected override void GameProgressing()
    {
        bool state;
        foreach (Transform childTransform in _boxes)
        {
            Transform child = childTransform.GetChild(0);
            state = child.GetChild(0).GetComponent<OpenBoxController>().getClosed();
            if (state)
            {
                foreach (Transform pin in child.GetChild(1))
                {
                    pin.GetComponent<PinController>().setActive(false);
                }
            }
        }
    }

    protected override bool CheckStarted()
    {
        bool state = false;
        foreach (Transform box in _boxes)
        {
            state = box.GetChild(1).GetChild(0).GetComponent<OpenBoxTrigger>().getTriggered();
            if (state) return state;
        }
        return state;
    }

    protected override bool CheckFinished()
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

    protected override void ResetComponents()
    {
        foreach (Transform box in _boxes)
        {
            foreach (Transform childTransform in box.GetChild(0).GetChild(1))
            {
                childTransform.GetComponent<PinController>().setActive(false);
            }
        }
    }
}
