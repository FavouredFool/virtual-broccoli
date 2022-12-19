using UnityEngine;

public class WheelIconScript : MonoBehaviour
{
    [SerializeField]
    private Texture _iconImage;

    void Start()
    {
        GetComponent<Renderer>().material.SetTexture("_BaseMap", _iconImage);
    }
}
