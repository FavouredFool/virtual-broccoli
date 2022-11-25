using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static StairManager;

public class StairLever : MonoBehaviour
{
    [SerializeField]
    private bool _stairPos;

    [SerializeField]
    private StairColor _color;

    [SerializeField]
    private GameObject _movableLever;

    [SerializeField]
    private MeshRenderer _marker;


    private StairManager _stairManager;

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

        _marker.material = _stairManager.StairColorToMaterial(_color);
    }



    private void ChangeStairPositions()
    {
        SetLeverVisual();
        
        foreach (StairRotation stair in _stairManager.GetAllStairs())
        {
            if (stair.GetFirstColor() == _color || stair.GetSecondColor() == _color)
            {
                stair.ChangeRotation();
            }
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

    public void SetStairManager(StairManager stairManager)
    {
        _stairManager = stairManager;
    }


}
