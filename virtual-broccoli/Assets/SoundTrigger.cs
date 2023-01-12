using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTrigger : MonoBehaviour
{
    [SerializeField] string name;
    [SerializeField] bool ambient;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !string.IsNullOrEmpty(name))
        {
            AudioManager.Instance.PlayBackground(name);

            if(ambient) RenderSettings.ambientLight = Color.black;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (ambient) RenderSettings.ambientLight = Color.white;
    }
}
