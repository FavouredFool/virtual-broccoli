using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using colorKit;
using lerpKit;

public class CauldronScript : MonoBehaviour
{
    [SerializeField]
    MeshRenderer _meshRenderer = null;

    bool _blend = false;

    [SerializeField, Range(0f, 0.1f)]
    float _blendSpeed = 0.05f;

    Vector4 _goalColor;
    Vector4 _oldColor;
    float _lerpValue = 0;

    public void Start()
    {
        _goalColor = CMYKManager.ConvertRGBToCMYK(_meshRenderer.material.color);
        _oldColor = _goalColor;
    }

    private void Update()
    {
        if (!_blend)
        {
            return;
        }

        _lerpValue += _blendSpeed;

        Debug.Log(_oldColor);
        Debug.Log(_goalColor);
        float[] lerpedColor = colorLerping.colorLerp(CMYKManager.Vector4ToFloatArray(_oldColor), CMYKManager.Vector4ToFloatArray(_goalColor), _lerpValue);

        _meshRenderer.material.color = CMYKManager.ConvertCMYKToRGB(CMYKManager.FloatArrayToVector4(lerpedColor));

        if (_lerpValue >= 1)
        {
            _oldColor = _goalColor;
            _blend = false;
            _lerpValue = 0;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        Vector4 colorCMYK = CMYKManager.ConvertRGBToCMYK(other.GetComponent<MeshRenderer>().material.color);

        Vector4 oldColor = CMYKManager.ConvertRGBToCMYK(_meshRenderer.material.color);

        _goalColor = CMYKManager.MixColorsCMYK(colorCMYK, oldColor);

        _blend = true;

        Destroy(other.gameObject);
    }
}
