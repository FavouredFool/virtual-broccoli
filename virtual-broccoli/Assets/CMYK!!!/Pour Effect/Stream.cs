using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stream : MonoBehaviour
{

    [SerializeField]
    private float _streamDistance = 1.5f;

    [SerializeField]
    private float _streamSpeed = 2f;

    private ParticleSystem _splashParticle = null;

    private Coroutine _pourRoutine;

    private LineRenderer _lr = null;

    private Vector3 _targetPosition = Vector3.zero;

    private void Awake()
    {
        _lr = GetComponent<LineRenderer>();
        _splashParticle = GetComponentInChildren<ParticleSystem>();
    }

    private void Start()
    {
        MoveToPosition(0, transform.position);
        MoveToPosition(1, transform.position);
    }

    public void Begin()
    {
        StartCoroutine(UpdateParticle());
        _pourRoutine = StartCoroutine(BeginPour());
    }

    public void End()
    {
        StopCoroutine(_pourRoutine);
        _pourRoutine = StartCoroutine(EndPour());
    }

    protected IEnumerator EndPour()
    {
        while(!HasReachedPosition(0, _targetPosition))
        {
            AnimateToPosition(0, _targetPosition);
            AnimateToPosition(1, _targetPosition);

            yield return null;
        }

        Destroy(gameObject);
        
    }

    protected IEnumerator BeginPour()
    {
        while (gameObject.activeSelf)
        {
            _targetPosition = FindEndPoint();

            MoveToPosition(0, transform.position);
            AnimateToPosition(1, _targetPosition);

            yield return null;
        }
        
    }

    private Vector3 FindEndPoint()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, Vector3.down);

        Physics.Raycast(ray, out hit, _streamDistance);
        Vector3 endPoint = hit.collider ? hit.point : ray.GetPoint(_streamDistance);

        return endPoint;
    }

    private void MoveToPosition(int index, Vector3 targetPosition)
    {
        _lr.SetPosition(index, targetPosition);
    }

    public void SetStreamColor(Color color)
    {
        _lr.sharedMaterial.color = color;
    }

    public void AnimateToPosition(int index, Vector3 targetPosition)
    {
        Vector3 currentPoint = _lr.GetPosition(index);

        Vector3 newPosition = Vector3.MoveTowards(currentPoint, _targetPosition, Time.deltaTime * _streamSpeed);
        _lr.SetPosition(index, newPosition);
    }

    private bool HasReachedPosition(int index, Vector3 targetPosition)
    {
        Vector3 currentPosition = _lr.GetPosition(index);
        return currentPosition == targetPosition;
    }

    private IEnumerator UpdateParticle()
    {
        while (gameObject.activeSelf)
        {
            _splashParticle.gameObject.transform.position = _targetPosition;

            bool isHitting = HasReachedPosition(1, _targetPosition);
            _splashParticle.gameObject.SetActive(isHitting);

            yield return null;
        }
        
    }

}
