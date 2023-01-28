using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalHideoutController : MonoBehaviour
{
    [SerializeField] private float moveDuration;
    [SerializeField] private AnimationCurve movementCurve;
    [SerializeField] private GameObject movingObject;
    [SerializeField] private GameObject endpositionObject;
    [SerializeField] private GameObject crystal;
    [SerializeField] private GameObject pLight;
    [SerializeField] private GameObject sLight;

    private bool _move;
    private Vector3 _startposition;
    private Vector3 _endposition;
    private float _elapsedTime;

    private void Start()
    {
        _move = false;
        _startposition = movingObject.transform.position;
        _endposition = endpositionObject.transform.position;
        crystal.SetActive(false);
        pLight.SetActive(false);
        sLight.SetActive(false);
    }

    private void Update()
    {
        if(_move)
        {
            _elapsedTime += Time.deltaTime;
            float percentageCompletedTime = _elapsedTime / moveDuration;
            movingObject.transform.position = Vector3.Lerp(_startposition, _endposition, movementCurve.Evaluate(percentageCompletedTime));

            if (movingObject.transform.position == _endposition) ActivateMovement(false);
        }
    }

    public void ActivateMovement(bool move)
    {
        _move = move;
        if (move)
        {
            pLight.SetActive(true);
            sLight.SetActive(true);
            crystal.SetActive(true);
        }
    }
}
