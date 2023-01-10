using System.Collections.Generic;
using UnityEngine;

public class PipeManager : MonoBehaviour
{
    [SerializeField] private GameObject _puzzleStart;

    [SerializeField] private GameObject _puzzleEnd;

    [SerializeField] private GameObject _crystal;

    private static bool _solved = false;

    private static readonly int _threshold = 10;

    private static readonly int _comparisonValue = 45;

    private static GameObject puzzleStart;
    private static GameObject puzzleEnd;
    private static GameObject crystal;

    private void Start()
    {
        puzzleStart = _puzzleStart;
        puzzleEnd = _puzzleEnd;
        crystal = _crystal;
        crystal.SetActive(false);
    }

    public static void CheckFinalState()
    {
        List<GameObject> checkedPipes = new() { puzzleStart };
        GameObject current = null;
        Dictionary<string, GameObject> startNeighbors = puzzleStart.GetComponent<Pipe>().GetNeighbors();
        if (startNeighbors.Count == 1)
        {
            foreach (KeyValuePair<string, GameObject> pair in startNeighbors)
            {
                current = pair.Value;
                checkedPipes.Add(current);
                break;
            }
            _solved = TraversePipeNeighbors(current, checkedPipes);
            
            if (_solved && !crystal.activeSelf)
            {
                Debug.Log("Solved cyan room");
                crystal.SetActive(true);
            }
        }
    }

    private static bool TraversePipeNeighbors(GameObject current, List<GameObject> checkedPipes)
    {
        bool foundNewNeighbor = false;
        GameObject checkedNeighborPipe;

        foreach (KeyValuePair<string, GameObject> pair in current.GetComponent<Pipe>().GetNeighbors())
        {
            checkedNeighborPipe = pair.Value;
            if (!checkedPipes.Contains(checkedNeighborPipe))
            {
                foundNewNeighbor = true;
                current.GetComponent<Pipe>().ChangeLightState(true);
                if (puzzleEnd.Equals(checkedNeighborPipe))
                {
                    return true;
                }
                checkedPipes.Add(checkedNeighborPipe);

                if (TraversePipeNeighbors(checkedNeighborPipe, checkedPipes))
                {
                    return true;
                }
            }
        }

        if (!foundNewNeighbor)
        {
            current.GetComponent<Pipe>().ChangeLightState(false);
        }
        return false;
    }

    public static bool CheckValidRotation(GameObject selectedPipe)
    {
        float y = selectedPipe.transform.localEulerAngles.y;
        float z = selectedPipe.transform.localEulerAngles.z;

        float restAngleY = y % _comparisonValue;
        float restAngleZ = z % _comparisonValue;

        /*if (!(restAngleY <= _threshold || restAngleY >= (_comparisonValue - _threshold)))
        {
            Debug.Log("invalid Y: " + y);
            return false;
        }

        if (!(restAngleZ <= _threshold || restAngleZ >= (_comparisonValue - _threshold)))
        {
            Debug.Log("invalid Z: " + z);
            return false;
        }*/

        int correctedY = (int)(restAngleY <= _threshold ? y - restAngleY : _comparisonValue * ((((int)y) / _comparisonValue) + 1));
        int correctedZ = (int)(restAngleZ <= _threshold ? z - restAngleZ : _comparisonValue * ((((int)z) / _comparisonValue) + 1));
        return CheckAbsoluteAngleDifference(correctedY, correctedZ);
    }

    private static bool CheckAbsoluteAngleDifference(int firstAngle, int secondAngle)
    {
        int max = Mathf.Max(firstAngle, secondAngle);
        int min = Mathf.Min(firstAngle, secondAngle);
        if (max <= 180 || min < 90)
        {
            firstAngle = firstAngle >= 180 ? firstAngle -= 360 : firstAngle;
            secondAngle = secondAngle >= 180 ? secondAngle -= 360 : secondAngle;
        }

        //return Mathf.Abs(firstAngle - secondAngle) == 90;
        return true;
    }
}