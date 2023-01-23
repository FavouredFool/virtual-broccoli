using UnityEngine;

public class GolfResetBall : MonoBehaviour
{
    [SerializeField] private GameObject _golfBall;

    private BallPositionChecker ballPositionChecker;

    private void Start()
    {
        ballPositionChecker = _golfBall.GetComponent<BallPositionChecker>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Golfball"))
        {
            _golfBall.transform.position = ballPositionChecker.GetLastMovelessPosition();
        }
    }
}
