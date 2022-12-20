using UnityEngine;
using SG;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class HandGestureTracker : MonoBehaviour
{
    public SG_BasicGesture _TPAimGesture;

    public InputActionAsset _inputAsset;

    private bool _isTPAiming = false;

    private void Update()
    {
        if (_TPAimGesture.GestureMade)
        {
            Debug.Log("tp aim start");
            _isTPAiming = true;
            
        }

        if (_TPAimGesture.GestureStopped)
        {
            Debug.Log("tp aim stop");
            _isTPAiming = false;

            Teleport();

            
        }

        if (_isTPAiming)
        {
            Debug.Log("aiming");
        }

        
    }

    private void Teleport()
    {
    }
}
