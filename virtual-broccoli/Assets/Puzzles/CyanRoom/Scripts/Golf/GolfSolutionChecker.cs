using UnityEngine;

public class GolfSolutionChecker : MonoBehaviour
{
    [SerializeField] private int _secondsToStayInHole;
    private int possibleEndTime;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Golfball"))
        {
            possibleEndTime = Mathf.FloorToInt(Time.time + _secondsToStayInHole);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (Time.time >= possibleEndTime && other.CompareTag("Golfball"))
        {
            other.gameObject.GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Golfball"))
        {
            Material mat = other.gameObject.GetComponent<MeshRenderer>().material;
            if (mat.IsKeywordEnabled("_EMISSION"))
            {
                mat.DisableKeyword("_EMISSION");
            }
        }
    }
}
