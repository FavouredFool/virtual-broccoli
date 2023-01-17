using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class EndSceneManager : MonoBehaviour
{
    [SerializeField] private XRSocketInteractor _socket;
    [SerializeField] private Transform _door;
    [SerializeField] private GameObject _playerFadeQuad;
    [SerializeField] private GameObject _QuadText;
    [SerializeField] private RotationController _rotationControllerKey;
    
    private FadeScreen _fadeScreen;
    private IXRSelectInteractable _keyXR;

    private void Start()
    {
        _fadeScreen = _playerFadeQuad.GetComponent<FadeScreen>();
        _QuadText.SetActive(false);
    }

    public void SetKey()
    {
        _keyXR = _socket.GetOldestInteractableSelected();
        /* XRGrabInteractable grabInteractable = _keyXR.transform.GetComponent<XRGrabInteractable>();
        grabInteractable.enabled = false; */

        _rotationControllerKey.setOpen(true);

        StartCoroutine(CheckKeyRotationUpdate());
    }

    IEnumerator CheckKeyRotationUpdate()
    {
        
        while (!_rotationControllerKey.GetRotationCheck())
        {
            yield return null;
        }

        yield return new WaitForSeconds(1f);
        _door.GetComponent<RotationController>().setOpen(true);
        _fadeScreen.gameObject.SetActive(true);
        _fadeScreen.FadeIn();

        while (!_fadeScreen.GetFaded())
        {
            yield return null;
        }

        _QuadText.SetActive(true);

        yield break;
    }
}
