using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomClearController : MonoBehaviour
{
    [SerializeField] private GameObject[] lights;
    [SerializeField] private OpenBoxController openBoxController;
    [SerializeField] private GameObject[] endCrystalObjects;

    private Dictionary<string, GameObject> lightsDict;

    private void Start()
    {
        lightsDict = new Dictionary<string, GameObject>();
        foreach(GameObject light in lights)
        {
            lightsDict.Add(light.name, light);
        }
        ActivateAmbientObjects(false);
    }

    public void ActivateAmbientObjects(bool activate)
    {
        foreach (GameObject ambient in endCrystalObjects)
        {
            ambient.SetActive(activate);
        }
    }

    public void ClearLight(string lightName)
    {
        lightsDict[lightName].SetActive(false);

        if(CheckGamesFinished())
        {
            openBoxController.SetOpen(true);
            ActivateAmbientObjects(true);
        }
    }

    private bool CheckGamesFinished()
    {
        bool check = true;
        foreach (GameObject light in lights)
        {
            if (light.activeSelf)
            {
                check = false;
                break;
            }
        }
        return check;
    }
}
