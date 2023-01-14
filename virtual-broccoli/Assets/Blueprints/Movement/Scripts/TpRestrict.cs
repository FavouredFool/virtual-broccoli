using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using static StairManager;

public class TpRestrict : MonoBehaviour
{
    [Header("Stairs")]
    [SerializeField]
    StairRotationScript _middleStair;

    [SerializeField]
    StairRotationScript _lowerStairLeft;

    [SerializeField]
    StairRotationScript _lowerStairRight;

    [SerializeField]
    StairRotationScript _upperStairRight;

    [SerializeField]
    StairRotationScript _upperStairLeft;

    [Header("Sockets")]
    [SerializeField]
    GameObject _lowerSocketLeft;

    [SerializeField]
    GameObject _lowerSocketRight;

    [SerializeField]
    GameObject _upperSocketLeft;

    [SerializeField]
    GameObject _upperSocketRight;



    BaseTeleportationInteractable _tpInteractable;


    void Start()
    {
        _tpInteractable = GetComponent<BaseTeleportationInteractable>();
        _tpInteractable.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SetStairTP(StairRotationScript stair, bool allowed)
    {
        foreach (BaseTeleportationInteractable tpInteractable in stair.GetComponentsInChildren<BaseTeleportationInteractable>())
        {
            tpInteractable.enabled = allowed;
        }
    }

    void SetSocketTP(GameObject socket, bool allowed)
    {
        socket.GetComponent<BaseTeleportationInteractable>().enabled = allowed;
    }

    void UpdateTPs()
    {
        if (_middleStair)
        {
            int result = EvaluateMiddleStair();

            if (result == 1)
            {
                SetStairTP(_middleStair, true);
                SetSocketTP(_upperSocketRight, true);
                SetSocketTP(_upperSocketLeft, false);
            }
            else if (result == 2)
            {
                SetStairTP(_middleStair, true);
                SetSocketTP(_upperSocketRight, false);
                SetSocketTP(_upperSocketLeft, true);
            }
            else
            {
                SetStairTP(_middleStair, false);
                SetSocketTP(_upperSocketLeft, false);
                SetSocketTP(_upperSocketRight, false);
            }
        }
        else
        {
            SetSocketTP(_upperSocketLeft, false);
            SetSocketTP(_upperSocketRight, false);
        }

        if (_lowerStairLeft)
        {
            if (EvaluateLowerStair(StairRotation.LEFT))
            {
                SetStairTP(_lowerStairLeft, true);
                SetSocketTP(_lowerSocketLeft, true);
            }
            else
            {
                SetStairTP(_lowerStairLeft, false);
                SetSocketTP(_lowerSocketLeft, false);
            }
        }
        else
        {
            SetSocketTP(_lowerSocketLeft, false);
        }

        if (_lowerStairRight)
        {
            if (EvaluateLowerStair(StairRotation.RIGHT))
            {
                SetStairTP(_lowerStairRight, true);
                SetSocketTP(_lowerSocketRight, true);
            }
            else
            {
                SetStairTP(_lowerStairRight, false);
                SetSocketTP(_lowerSocketRight, false);
            }
        }
        else
        {
            SetSocketTP(_lowerSocketRight, false);
        }
    }

    bool EvaluateLowerStair(StairRotation leftOrRight)
    {
        if (leftOrRight == StairRotation.LEFT)
        {
            StairRotation lowerLeftRot = _lowerStairLeft.GetStairRotation();

            if (lowerLeftRot == StairRotation.LEFT)
            {
                return false;
            }
            else
            {
                if (!_middleStair)
                {
                    return true;
                }
                else if (_middleStair.GetStairRotation() == StairRotation.RIGHT)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        } 
        else
        {
            StairRotation lowerRightRot = _lowerStairRight.GetStairRotation();

            if (lowerRightRot == StairRotation.RIGHT)
            {
                return false;
            }
            else
            {
                if (!_middleStair)
                {
                    return true;
                }
                else if (_middleStair.GetStairRotation() == StairRotation.LEFT)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }

    int EvaluateMiddleStair()
    {

        // 0 -> False
        // 1 -> True for right
        // 2 -> True for left

        StairRotation middleRot = _middleStair.GetStairRotation();

        if (middleRot == StairRotation.RIGHT)
        {
            if (!_upperStairRight)
            {
                return 1;
            }
            else
            {
                if (_upperStairRight.GetStairRotation() == StairRotation.RIGHT)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }
        else
        {
            if (!_upperStairLeft)
            {
                return 2;
            }
            else
            {
                if (_upperStairLeft.GetStairRotation() == StairRotation.LEFT)
                {
                    return 2;
                }
                else
                {
                    return 0;
                }
            }
        }
       

    }
}
