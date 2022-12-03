using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using colorKit;
using static ColorMachineSequence;

public class CauldronScript : MonoBehaviour
{
    [SerializeField]
    MeshRenderer _meshRenderer = null;

    [SerializeField]
    ColorMachineSequence _machineSequence;

    bool _blend = false;

    [SerializeField, Range(0f, 0.1f)]
    float _blendSpeed = 0.05f;

    Vector4 _mixedColor;
    Vector4 _oldColor;
    float _lerpValue = 0;


    public void Start()
    {
        _mixedColor = CMYKUtilites.ConvertRGBToCMYK(_meshRenderer.material.color);
        _oldColor = _mixedColor;
    }

    private void Update()
    {
        if (_machineSequence.GetMachineState() != MachineState.MIXING)
        {
            return;
        }

        if (!_blend)
        {
            return;
        }
        

        LerpColorOnBlend();
    }

    private void TestIfGoalColorReached()
    {
        if (_mixedColor == _machineSequence.GetGoalColor())
        {
            // End this part of the cycle
            _machineSequence.MixComplete();
        }
    }

    private void LerpColorOnBlend()
    {
        _lerpValue += _blendSpeed;

        float[] lerpedColor = colorLerping.colorLerp(CMYKUtilites.Vector4ToFloatArray(_oldColor), CMYKUtilites.Vector4ToFloatArray(_mixedColor), _lerpValue);

        _meshRenderer.material.color = CMYKUtilites.ConvertCMYKToRGB(CMYKUtilites.FloatArrayToVector4(lerpedColor));

        if (_lerpValue >= 1)
        {
            _oldColor = _mixedColor;
            _blend = false;
            _lerpValue = 0;

            Debug.Log(_mixedColor);
            Debug.Log(CMYKUtilites.ConvertCMYKToRGB(_mixedColor));
            TestIfGoalColorReached();
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        Vector4 colorCMYK = CMYKUtilites.ConvertRGBToCMYK(other.GetComponent<MeshRenderer>().material.color);

        Vector4 oldColor = CMYKUtilites.ConvertRGBToCMYK(_meshRenderer.material.color);

        _mixedColor = CMYKUtilites.MixColorsCMYK(colorCMYK, oldColor);

        _blend = true;

        Destroy(other.gameObject);
    }

    public void SetCauldronColorCMYK(Vector4 colorCMYK)
    {
        _meshRenderer.material.color = CMYKUtilites.ConvertCMYKToRGB(colorCMYK);
        _oldColor = colorCMYK;
    }
}
