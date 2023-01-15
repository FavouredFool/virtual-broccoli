using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (other.CompareTag("Plate"))
        {
            other.transform.position = transform.position;
            SendCompareInfo(other.name);
        } 
            
            
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log(other.name);
        if (other.CompareTag("Plate")) SendCompareInfo("");
    }

    private void SendCompareInfo(string comparement)
    {
        Debug.Log(comparement);
        GetComponentInParent<SequenceController>().CompareTriggerPlate(name, comparement);
    }
}
