using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class RoomLoader : MonoBehaviour
{

    public void Start()
    {
        StartCoroutine(LoadSceneAsync("CyanRoom"));
        StartCoroutine(LoadSceneAsync("MagentaRoom"));
        StartCoroutine(LoadSceneAsync("YellowRoom"));
    }


    IEnumerator LoadSceneAsync(string name)
    {
        Scene loadingScene = SceneManager.GetSceneByName(name);
  
        if (loadingScene.buildIndex == -1 || !loadingScene.isLoaded)
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);

            while (!asyncLoad.isDone)
            {
                yield return null;
            }
        }
    }

    

 
}
