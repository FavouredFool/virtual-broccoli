using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    [SerializeField]
    private Dictionary<string, GameObject> _neighborPipeBorders;

    private GameObject _placedGrid = null;

    private GameObject _blueprint;

    private void Start()
    {
        _neighborPipeBorders = new Dictionary<string, GameObject>();
        Transform blueprintTransform = transform.parent.gameObject.transform.Find("Blueprint");
        _blueprint = blueprintTransform != null ? blueprintTransform.gameObject : null;
    }

    public void SetPlaceGrid(GameObject gridPlacement)
    {
        _placedGrid = gridPlacement;
    }

    public GameObject GetPlaceGrid()
    {
        return _placedGrid;
    }

    public bool IsDragged()
    {
        return !CompareTag("Ventil") && (CompareTag("PipeRotateOnly") || CompareTag("Pipe")) && GetComponent<Rigidbody>().isKinematic && GetComponent<Rigidbody>().useGravity;
    }

    public Dictionary<string, GameObject> GetNeighbors()
    {
        return _neighborPipeBorders;
    }

    public void AddNeighbor(string key, GameObject gameObject)
    {
        _neighborPipeBorders.TryAdd(key, gameObject);
    }

    public void RemoveNeighbor(string key, GameObject gameObject)
    {
        if (_neighborPipeBorders.ContainsValue(gameObject))
        {
            _neighborPipeBorders.Remove(key);
        }
    }

    public void RemoveNeighbors()
    {
        _neighborPipeBorders.Clear();
    }

    public GameObject GetBlueprint()
    {
        return _blueprint;
    }
}
