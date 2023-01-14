using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DualWieldManager : MonoBehaviour
{
    // Profiles:
    // 1: Only Controllers
    // 2: Right Hand SenseGlove
    // 3: Left Hand SenseGlove
    // 4: Only SenseGloves

    [SerializeField, Range(1, 4)]
    private int _profile = 1;


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

    bool _lc;
    bool _rc;
    bool _ls;
    bool _rs;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _profile = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _profile = 2;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _profile = 3;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            _profile = 4;
        }

        switch(_profile)
        {
            case 1:
                _lc = true;
                _rc = true;
                _ls = false;
                _rs = false;
                break;
            case 2:
                _lc = true;
                _rc = false;
                _ls = false;
                _rs = true;
                break;
            case 3:
                _lc = false;
                _rc = true;
                _ls = true;
                _rs = false;
                break;
            case 4:
                _lc = false;
                _rc = false;
                _ls = true;
                _rs = true;
                break;
            default:
                Debug.LogWarning("FEHLER: Falsches Profil");
                break;
        }

        _leftIndexController.SetActive(_lc);
        _rightIndexController.SetActive(_rc);
        _leftSenseGloves.SetActive(_ls);
        _rightSenseGloves.SetActive(_rs);
    }
}
