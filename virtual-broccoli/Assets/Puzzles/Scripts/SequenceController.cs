using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceController : MonoBehaviour
{
    private Dictionary<string, bool> _letterSeq;
    private bool _triggered;

    private void Start()
    {
        _letterSeq = new Dictionary<string, bool>();
        foreach (Transform childTrnasform in transform)
        {
            _letterSeq.Add(childTrnasform.name, false);
        }
    }

    public void CompareTriggerPlate(string triggerName, string plateName)
    {
        string check = "";
        switch (triggerName)
        {
            case "Space - Trigger":
            case "Space2 - Trigger":
                check = "Space";
                break;

            case "P - Trigger":
                check = "P";
                break;

            case "N - Trigger":
                check = "N";
                break;

            default:
                check = "WrongName";
                break;
        }

        _letterSeq[triggerName] = plateName.Equals(check);
        if (!_triggered) SetTriggered(true);
    }

    public Dictionary<string, bool> GetLetterSequence()
    {
        return _letterSeq;
    }

    public bool GetTriggered()
    {
        return _triggered;
    }

    public void SetTriggered(bool triggered)
    {
        _triggered = triggered;
    }
}
