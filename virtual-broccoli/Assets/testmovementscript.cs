using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testmovementscript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(new Vector3(-1, 0, 0));
    }

    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collision");
    }
}
