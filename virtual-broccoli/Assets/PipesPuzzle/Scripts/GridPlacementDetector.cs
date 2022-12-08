using UnityEngine;

public class GridPlacementDetector : MonoBehaviour
{
    [SerializeField]
    private Pipe _placedPipe;

    private Collider _gridBox; 

    private void Awake()
    {
        _gridBox = GetComponent<Collider>();
    }

    private void OnTriggerStay(Collider collider)
    {
        if (collider.CompareTag("Pipe"))
        {
            GameObject colliderObject = collider.gameObject;
            Pipe pipeComponent = colliderObject.GetComponent<Pipe>();
            if (pipeComponent.IsDragged())
            {
                bool centerIsInside = _gridBox.bounds.Contains(colliderObject.transform.position);
                GameObject blueprint = pipeComponent.GetBlueprint();
                //Pipe entered this grid placement during first check and no other pipe is placed already inside
                if (centerIsInside && pipeComponent.GetPlaceGrid() == null)
                {
                    if (_placedPipe == null)
                    {
                        pipeComponent.SetPlaceGrid(_gridBox);
                        blueprint.SetActive(true);
                        blueprint.transform.position = transform.position;
                        _placedPipe = pipeComponent;
                    }
                }
                //Pipe left this grid placement (second check needed because of update by other grid placements)
                else if (!centerIsInside && pipeComponent.GetPlaceGrid() == _gridBox)
                {
                    pipeComponent.SetPlaceGrid(null);
                    pipeComponent.RemoveNeighbors();
                    blueprint.SetActive(false);
                    _placedPipe = null;
                }
            }
        }
    }
}
