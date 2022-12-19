using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTrigger : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> walls;
    [SerializeField]
    private bool appear;
    [SerializeField]
    private bool disappear;

    private void OnTriggerEnter(Collider other)
    {

        if (walls != null)
        {
            foreach (GameObject wall in walls)
            {
                if (appear && !wall.activeSelf)
                {
                    wall.SetActive(true);
                }
                else if (disappear && wall.activeSelf)
                {
                    wall.SetActive(false);
                }
            }
        }
    }
}
