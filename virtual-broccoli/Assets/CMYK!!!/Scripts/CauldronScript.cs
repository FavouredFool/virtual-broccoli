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
    ColorMachine _machine;

    bool _blend = false;

    [SerializeField, Range(1f, 10f)]
    float _blendSpeed = 5f;

    Vector4 _mixedColor;
    Vector4 _oldColor;
    float _lerpValue = 0;

    Vector4 _activeCauldronColor;
    Vector4 _oldActiveCauldronColor;


    public void Start()
    {
        _mixedColor = CMYKUtilites.ConvertRGBToCMYK(_meshRenderer.material.color);
        _oldColor = _mixedColor;
        _activeCauldronColor = _mixedColor;
        _oldActiveCauldronColor = _mixedColor;

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
        if (_machine.GetState() is MixingState && _mixedColor == _machine.GetGoalColor())
        {
            // End this part of the cycle
            _machine.MixComplete();
        }
    }

    private void LerpColorOnBlend()
    {
        _lerpValue += (_blendSpeed * Time.deltaTime);

        float[] lerpedColor = colorLerping.colorLerp(CMYKUtilites.Vector4ToFloatArray(_oldActiveCauldronColor), CMYKUtilites.Vector4ToFloatArray(_mixedColor), _lerpValue);

        _activeCauldronColor = CMYKUtilites.FloatArrayToVector4(lerpedColor);

        _meshRenderer.material.color = CMYKUtilites.ConvertCMYKToRGB(_activeCauldronColor);

        if (_lerpValue >= 1)
        {
            _blend = false;
            _lerpValue = 0;

            _oldColor = _mixedColor;
            _oldActiveCauldronColor = _activeCauldronColor;

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
        _oldColor = _mixedColor;
        _oldActiveCauldronColor = _activeCauldronColor;
        _lerpValue = 0;

        _blend = true;

        Destroy(other.gameObject);
    }


    public void SetCauldronColorCMYK(Vector4 colorCMYK)
    {
        _meshRenderer.material.color = CMYKUtilites.ConvertCMYKToRGB(colorCMYK);
        _oldColor = colorCMYK;
        _mixedColor = colorCMYK;
        _blend = true;
    }

    public Vector4 GetActiceCauldronColor()
    {
        return _activeCauldronColor;
    }
}
