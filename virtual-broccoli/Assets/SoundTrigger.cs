using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTrigger : MonoBehaviour
{
    [SerializeField] string name;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !string.IsNullOrEmpty(name))
        {
            AudioManager.Instance.PlayBackground(name);
        }
    }
}
