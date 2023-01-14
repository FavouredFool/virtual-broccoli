using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class EndSceneManager : MonoBehaviour
{
    [SerializeField] private XRSocketInteractor _socket;
    [SerializeField] private Transform _attachment;
    [SerializeField] private Transform _door;
    [SerializeField] private GameObject _playerCanvas;
    //[SerializeField] private bool test;

    [SerializeField] private IXRSelectInteractable _keyXR;
    private RotationController _rotationControllerKey;
    private Image _canvasImage;
    private GameObject _canvasText;
    private bool _startEnding;
    private bool _completeEnding;

    private void Start()
    {
        _canvasImage = _playerCanvas.transform.GetChild(0).GetComponent<Image>();
        _canvasText = _playerCanvas.transform.GetChild(1).gameObject;
        _canvasImage.color = new Color(0, 0, 0, 0);
        _playerCanvas.SetActive(false);
        _canvasText.SetActive(false);
        _startEnding = false;
        _completeEnding = false;
    }

    public void SetKey()
    {

        _keyXR = _socket.GetOldestInteractableSelected();
        _keyXR.transform.position = _attachment.position;
        _keyXR.transform.localRotation = Quaternion.Euler(new Vector3(0, -90, 90));

        XRGrabInteractable grabInteractable = _keyXR.transform.GetComponent<XRGrabInteractable>();
        grabInteractable.enabled = false;

        _rotationControllerKey = _keyXR.transform.GetComponent<RotationController>();
        _rotationControllerKey.setOpen(true);


        _startEnding = true;
        //StartCoroutine(CheckKeyRotationUpdate());
    }

    /*IEnumerator CheckKeyRotationUpdate()
    {
        _rotationControllerKey.setOpen(true);
        while (true)
        {
            if (_rotationControllerKey.GetRotationCheck())
            {
                _door.GetComponent<RotationController>().setOpen(true);
                break;
            }
        }

        _playerCanvas.SetActive(true);
        while (true)
        {
            _canvasImage.color += new Color(0, 0, 0, 0.05f);
            if (_canvasImage.color == Color.black)
            {
                _canvasText.SetActive(true);
                break;
            }
        }
        yield break;
    }*/

    private void Update()
    {
        if(_startEnding)
        {
            if (_rotationControllerKey.GetRotationCheck())
            {
                _door.GetComponent<RotationController>().setOpen(true);
                _playerCanvas.SetActive(true);
                _startEnding = false;
                _completeEnding = true;
            }
        }
        if(_completeEnding)
        {
            _canvasImage.color += new Color(0, 0, 0, 0.001f);
            if (_canvasImage.color == Color.black)
            {
                _canvasText.SetActive(true);
            }
        }
    }
}
