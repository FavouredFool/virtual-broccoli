using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private Sound[] background, sfx;
    [SerializeField] private AudioSource sourceBG, sourceSFX;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
    }

    public void PlayBackground(string name)
    {
        Sound sound = System.Array.Find(background, x => x.name == name);

        if (sound == null)
        {
            Debug.Log("No clip found");
        } 
        else
        {
            sourceBG.clip = sound.clip;
            sourceBG.Play();
        }
    }

    public void PlaySFX(string name)
    {
        Sound sound = System.Array.Find(sfx, x => x.name == name);

        if (sound == null)
        {
            Debug.Log("No clip found");
        }
        else
        {
            sourceSFX.PlayOneShot(sound.clip);
        }
    }
}
 