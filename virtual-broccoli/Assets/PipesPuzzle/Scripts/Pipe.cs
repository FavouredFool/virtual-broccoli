using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    [SerializeField]
    private Dictionary<string, GameObject> _neighborPipeBorders;

    private Collider _placedGrid = null;

    private GameObject _blueprint;

    private void Start()
    {
        _neighborPipeBorders = new Dictionary<string, GameObject>();
        _blueprint = transform.childCount > 2 ? transform.GetChild(2).gameObject : null;
    }

    public void SetPlaceGrid(Collider gridPlacement)
    {
        _placedGrid = gridPlacement;
    }

    public Collider GetPlaceGrid()
    {
        return _placedGrid;
    }

    public bool IsDragged()
    {
        return !CompareTag("Ventil") && ( CompareTag("Pipe") && GetComponent<Rigidbody>().isKinematic && GetComponent<Rigidbody>().useGravity);
    }

    public Dictionary<string, GameObject> GetNeighbors()
    {
        return _neighborPipeBorders;
    }

    public bool AddNeighbor(string key, GameObject gameObject)
    {
        return _neighborPipeBorders.TryAdd(key, gameObject);
    }

    public bool RemoveNeighbor(string key)
    {
        return _neighborPipeBorders.Remove(key);
    }

    public GameObject GetBlueprint()
    {
        return _blueprint;
    }
}
