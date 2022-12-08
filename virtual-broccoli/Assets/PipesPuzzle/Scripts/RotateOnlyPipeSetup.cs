using UnityEngine;

public class RotateOnlyPipeSetup : MonoBehaviour
{
    [SerializeField]
    private GameObject boxAround;

    void Start()
    {
        foreach (Transform child in transform)
        {
            child.transform.position = boxAround.transform.position;
            if (child.CompareTag("PipeRotateOnly"))
            {
                Pipe pipeComponent = child.gameObject.GetComponent<Pipe>();
                pipeComponent.SetPlaceGrid(boxAround);
            }
        }
    }
}
