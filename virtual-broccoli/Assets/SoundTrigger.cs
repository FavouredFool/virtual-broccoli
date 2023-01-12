using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTrigger : MonoBehaviour
{
    [SerializeField] string roomName;
    [SerializeField] bool ambient;

    private void OnTriggerEnter(Collider other)
    {
        

        if (other.CompareTag("Player") && !string.IsNullOrEmpty(roomName))
        {
            Debug.Log("Entered " + roomName);

            AudioManager.Instance.PlayBackground(roomName);

            if(ambient) RenderSettings.ambientLight = Color.black;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        

        if (other.CompareTag("Player") && ambient)
        {
            Debug.Log("Exited " + roomName);
            RenderSettings.ambientLight = Color.white;
        }
        
    }
}
