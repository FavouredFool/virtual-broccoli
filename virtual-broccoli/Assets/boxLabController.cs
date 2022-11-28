using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxLabController : MonoBehaviour
{

    [SerializeField]
    private GameObject _sphere;
    [SerializeField]
    private GameObject _startPoint;
    [SerializeField]
    private Transform _holes;

    private void Awake()
    {
        foreach (Transform childTransform in _holes)
        {
            childTransform.GetChild(0).GetComponent<HoleControl>().setController(this);
            childTransform.GetChild(1).GetComponent<HoleControl>().setController(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject getSphere()
    {
        return _sphere;
    }

    public void sphereReset()
    {
        _sphere.transform.position = _startPoint.transform.position;
    }
}
