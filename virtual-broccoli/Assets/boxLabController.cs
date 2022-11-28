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
    [SerializeField]
    private bool _startPosition;

    private void Awake()
    {
        foreach (Transform childTransform in _holes)
        {
            childTransform.GetChild(0).GetComponent<ResetTrigger>().setController(this);
            childTransform.GetChild(1).GetComponent<ResetTrigger>().setController(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (_startPosition) sphereReset();
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
