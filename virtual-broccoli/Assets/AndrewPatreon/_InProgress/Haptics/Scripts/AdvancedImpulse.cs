using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.InputSystem.XR.Haptics;

public class AdvancedImpulse : MonoBehaviour
{
    [SerializeField] private InputActionReference hapticReference = null;

    private void Awake()
    {
        TryBufferImpulse();
    }

    #region Impulse
    public void TryHapticImpulse(float intensity, float duration)
    {
        /*If the current controller is valid, send an impulse*/
        if (hapticReference.action.activeControl?.device is XRControllerWithRumble rumbleController)
            SendHapticImpulse(rumbleController, intensity, duration);
    }

    private void SendHapticImpulse(XRControllerWithRumble controller, float intensity, float duration)
    {
        /*Create a command, and send it to the controller*/
        SendHapticImpulseCommand impulseCommand = SendHapticImpulseCommand.Create(0, intensity, duration);
        controller.ExecuteCommand(ref impulseCommand);
    }
    #endregion

    #region Buffer (Not working)
    public void TryBufferImpulse()
    {
        /*If the current controller is valid, send an impulse*/
        if (hapticReference.action.activeControl?.device is XRControllerWithRumble rumbleController)
            SendHapticBuffer(rumbleController);
    }

    private void SendHapticBuffer(XRControllerWithRumble controller)
    {
        byte[] samples = new byte[100];

        /*Alternate between lowest and highest possible value*/
        for (int i = 0; i < samples.Length; i++)
            samples[i] = i % 2 == 0 ? byte.MinValue : byte.MaxValue;

        /*Create a command, and send it to the controller*/
        SendBufferedHapticCommand bufferCommand = SendBufferedHapticCommand.Create(samples);
        controller.ExecuteCommand(ref bufferCommand);
    }
    #endregion
}
