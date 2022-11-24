using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static StairRotation;

public class StairLever : MonoBehaviour
{
    [SerializeField]
    private bool _stairPos;

    [SerializeField]
    private List<StairRotation> _affectedStairs;

    [SerializeField]
    private GameObject _movableLever;

    private bool _oldStairPos;

    public void Start()
    {
        SetLeverVisual();
    }

    public void Update()
    {
        if (_oldStairPos != _stairPos)
        {
            ChangeStairPositions();
        }

        _oldStairPos = _stairPos;
    }



    private void ChangeStairPositions()
    {
        SetLeverVisual();

        foreach (StairRotation stair in _affectedStairs)
        {
            stair.ChangeRotation();
        }
    }

    private void SetLeverVisual()
    {
        Quaternion rotation;

        if (_stairPos)
        {
            rotation = Quaternion.Euler(new Vector3(0f, 0f, -45f));
        }
        else
        {
            rotation = Quaternion.Euler(new Vector3(0f, 0f, 45f));
        }
        _movableLever.transform.rotation = rotation;
    }

    
}
