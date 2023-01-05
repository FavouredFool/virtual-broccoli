using System.Collections.Generic;
using UnityEngine;

public class MovePipes : MonoBehaviour
{
    [SerializeField]
    private GameObject _puzzleStart;

    [SerializeField]
    private GameObject _puzzleEnd;

    private static bool _solved = false;

    private static readonly int _threshold = 10;

    private static readonly int _comparisonValue = 45;

    private static GameObject puzzleStart;
    private static GameObject puzzleEnd;

    public void OnAfterDeserialize()
    {
        puzzleStart = _puzzleStart;
        puzzleEnd = _puzzleEnd;
    }

    public static void CheckFinalState()
    {
        List<GameObject> checkedPipes = new() { puzzleStart };
        GameObject current = null;
        Dictionary<string, GameObject> startNeighbors = puzzleEnd.GetComponent<Pipe>().GetNeighbors();
        if (startNeighbors.Count == 1)
        {
            foreach (KeyValuePair<string, GameObject> pair in startNeighbors)
            {
                Debug.Log("Start gefunden");
                current = pair.Value;
                checkedPipes.Add(current);
                break;
            }
            _solved = TraversePipeNeighbors(current, checkedPipes);
            Debug.Log("________________________________________________________________________________________________");
        }
    }

    private static bool TraversePipeNeighbors(GameObject current, List<GameObject> checkedPipes)
    {
        GameObject checkedNeighborPipe;
        foreach (KeyValuePair<string, GameObject> pair in current.GetComponent<Pipe>().GetNeighbors())
        {
            checkedNeighborPipe = pair.Value;
            if (!checkedPipes.Contains(checkedNeighborPipe))
            {
                Debug.Log("Pipe: " + current.name + " -> neighbor: " + checkedNeighborPipe.name);
                if (puzzleEnd.Equals(checkedNeighborPipe))
                {
                    Debug.Log("Ende");
                    return true;
                }
                checkedPipes.Add(checkedNeighborPipe);
                TraversePipeNeighbors(checkedNeighborPipe, checkedPipes);
            }
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