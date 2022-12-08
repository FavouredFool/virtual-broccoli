using UnityEngine;

public class RemoveBlueprintCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pipe"))
        {
            Pipe pipeComponent = other.gameObject.GetComponent<Pipe>();
            if (pipeComponent.GetPlaceGrid() != null)
            {
                Debug.Log("Steppd out to fast");
                pipeComponent.GetPlaceGrid().GetComponent<GridPlacementDetector>().RemovePipeReference();
                pipeComponent.SetPlaceGrid(null);
                pipeComponent.RemoveNeighbors();
                pipeComponent.GetBlueprint().SetActive(false);
            }
        }
    }
}
