using UnityEngine;

public class ArrowMarker : MonoBehaviour
{
    private void Start()
    {
        //if emission may be activated
        GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");
    }
}
