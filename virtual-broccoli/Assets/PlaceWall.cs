using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceWall : MonoBehaviour
{
    [SerializeField]
    private GameObject wall;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(wall.activeSelf ) wall.SetActive(false);
    }
}
