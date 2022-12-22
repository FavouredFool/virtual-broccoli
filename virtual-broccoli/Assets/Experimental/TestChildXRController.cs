using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;

public class TestChildXRController : XRController
{



    public void SetActivated()
    {
        activateUsage = InputHelpers.Button.Trigger;
    }
}
