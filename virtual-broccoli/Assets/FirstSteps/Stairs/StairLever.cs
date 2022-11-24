using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static StairRotation;

public class StairLever : MonoBehaviour
{
    [SerializeField]
    private bool _changeStairPos;

    [SerializeField]
    private List<StairRotation> _affectedStairs;

    private bool _oldStairPos;

    public void Update()
    {
        if (_oldStairPos != _changeStairPos)
        {
            ChangeStairPositions();
        }

        _oldStairPos = _changeStairPos;
    }



    private void ChangeStairPositions()
    {
        foreach (StairRotation stair in _affectedStairs)
        {
            stair.ChangeRotation();
        }
    }

    
}
