using System.Collections.Generic;
using UnityEngine;

public class PipeManager : MonoBehaviour
{
    [SerializeField] private GameObject _puzzleStart;

    [SerializeField] private GameObject _puzzleEnd;

    [SerializeField] private GameObject _crystal;

    private static bool _solved = false;

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

        current.GetComponent<Pipe>().ChangeLightState(foundNewNeighbor);
        return false;
    }
}