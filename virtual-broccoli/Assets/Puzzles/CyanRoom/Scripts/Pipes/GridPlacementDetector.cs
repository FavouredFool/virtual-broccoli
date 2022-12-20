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
                //Pipe left this grid placement (second check needed because of update by other grid placements)
                if (!centerIsInside && pipeComponent.GetPlaceGrid() == gameObject)
                {
                    pipeComponent.SetPlaceGrid(null);
                    pipeComponent.RemoveNeighbors();
                    blueprint.SetActive(false);
                    _placedPipe = null;
                }
                //Pipe entered this grid placement during first check
                else if (centerIsInside && pipeComponent.GetPlaceGrid() != gameObject)
                {
                    //Pipe was in another grid placement before and was not removed right
                    if (pipeComponent.GetPlaceGrid() != null)
                    {
                        pipeComponent.GetPlaceGrid().GetComponent<GridPlacementDetector>().RemovePipeReference();
                    }

                    if (_placedPipe == null)
                    {
                        pipeComponent.SetPlaceGrid(this.gameObject);
                        blueprint.SetActive(true);
                        blueprint.transform.position = transform.position;
                        _placedPipe = pipeComponent;
                    }
                }
            }
        }
    }

    public void RemovePipeReference()
    {
        _placedPipe = null;
    }
}
