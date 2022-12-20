using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DualWieldManager : MonoBehaviour
{
    [SerializeField]
    private bool _senseGlovesActive = false;

    [SerializeField]
    private bool _isRightHanded = true;

    [Header("Index")]
    [SerializeField]
    private GameObject _rightIndexController;

    [SerializeField]
    private GameObject _leftIndexController;

    [Header("SenseGloves")]
    [SerializeField]
    private GameObject _rightSenseGloves;

    [SerializeField]
    private GameObject _leftSenseGloves;


    void Start()
    {
        if (_senseGlovesActive)
        {
            _leftIndexController.SetActive(_isRightHanded);
            _rightSenseGloves.SetActive(_isRightHanded);
            _rightIndexController.SetActive(!_isRightHanded);
            _leftSenseGloves.SetActive(!_isRightHanded);
        }
        else
        {
            _leftIndexController.SetActive(true);
            _rightIndexController.SetActive(true);
            _leftSenseGloves.SetActive(false);
            _rightSenseGloves.SetActive(false);
        }


    }
}
