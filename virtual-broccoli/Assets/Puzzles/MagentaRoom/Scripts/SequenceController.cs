using System.Collections.Generic;
using UnityEngine;

public class SequenceController : MonoBehaviour
{
    private Dictionary<string, bool> _letterSeq;
    private bool _triggered;

    private void Start()
    {
        _letterSeq = new Dictionary<string, bool>();
        foreach (Transform childTransform in transform)
        {
            if (!childTransform.name.StartsWith("Trigger"))
            {
                _letterSeq.Add(childTransform.name, false);
            }
        }
    }

    public void CompareTriggerPlate(string triggerName, string plateName)
    {
        if (!_letterSeq.ContainsKey(triggerName))
        {
            return;
        }

        string check = triggerName switch
        {
            "Space - Trigger" or "Space2 - Trigger" => "Space",
            "P - Trigger" => "P",
            "N - Trigger" => "N",
            _ => "WrongName",
        };

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
