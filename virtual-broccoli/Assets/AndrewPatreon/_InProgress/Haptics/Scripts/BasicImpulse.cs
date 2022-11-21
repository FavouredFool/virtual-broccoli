using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BasicImpulse : MonoBehaviour
{
    private XRController controller = null;

    private void Awake()
    {
        controller = GetComponent<XRController>();
    }

    public void TryControllerHaptics()
    {
        /*XR Toolkit implementation*/
        controller.SendHapticImpulse(0.5f, 1.0f);
    }
}
