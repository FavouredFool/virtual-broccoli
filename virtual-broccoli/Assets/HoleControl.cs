using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleControl : MonoBehaviour
{
    private boxLabController _controller;
    [SerializeField]
    private GameObject wall;
    [SerializeField]
    private bool appear;
    [SerializeField]
    private bool disappear;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void setController(boxLabController controller)
    {
        _controller = controller;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.Equals(_controller.getSphere()))
        {
            _controller.sphereReset();
        }

        if(wall != null)
        {
            if (appear && !wall.activeSelf)
            {
                wall.SetActive(true);
            } 
            else if (disappear && wall.activeSelf)
            {
                wall.SetActive(false);
            }
        }
    }

}
