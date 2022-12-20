using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class RoomLoader : MonoBehaviour
{

    public void Start()
    {
        StartCoroutine(LoadSceneAsync(1));
        StartCoroutine(LoadSceneAsync(2));
    }


    IEnumerator LoadSceneAsync(int index)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(index, LoadSceneMode.Additive);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    

 
}
