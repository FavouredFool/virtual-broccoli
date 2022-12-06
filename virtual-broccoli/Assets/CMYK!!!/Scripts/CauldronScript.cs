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

    Vector4 _activeCauldronColor;


    public void Start()
    {
        _mixedColor = CMYKUtilites.ConvertRGBToCMYK(_meshRenderer.material.color);
        _oldColor = _mixedColor;
        _activeCauldronColor = _mixedColor;

        Debug.Log(CMYKUtilites.ConvertRGBToCMYK(new Color(0, 0.5f, 0.5f, 1)));
    }

    private void Update()
    {
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

        _activeCauldronColor = CMYKUtilites.FloatArrayToVector4(lerpedColor);

        _meshRenderer.material.color = CMYKUtilites.ConvertCMYKToRGB(_activeCauldronColor);

        if (_lerpValue >= 1)
        {
            _oldColor = _mixedColor;
            _blend = false;
            _lerpValue = 0;

            Debug.Log(_mixedColor.ToString("F4"));
            //Debug.Log(CMYKUtilites.ConvertCMYKToRGB(_mixedColor));
            TestIfGoalColorReached();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        PotionScript potion = other.GetComponent<PotionScript>();

        if (potion == null)
        {
            Debug.Log("inserted wrong item in cauldron");
            return;
        }

        Vector4 colorCMYK = CMYKUtilites.ConvertRGBToCMYK(potion.GetColor());

        _mixedColor = CMYKUtilites.MixColorsCMYK(colorCMYK, _oldColor);

        _blend = true;

        Destroy(other.gameObject);
    }


    public void SetCauldronColorCMYK(Vector4 colorCMYK)
    {
        _meshRenderer.material.color = CMYKUtilites.ConvertCMYKToRGB(colorCMYK);
        _oldColor = colorCMYK;
        _mixedColor = colorCMYK;
        _activeCauldronColor = colorCMYK;
    }

    public Vector4 GetActiceCauldronColor()
    {
        return _activeCauldronColor;
    }
}
