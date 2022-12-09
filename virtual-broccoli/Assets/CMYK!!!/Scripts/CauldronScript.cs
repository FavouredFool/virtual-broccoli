using UnityEngine;
using colorKit;

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
    Vector4 _oldMixedColor;
    float _lerpValue = 0;

    Vector4 _activeCauldronColor;
    Vector4 _oldActiveCauldronColor;


    public void Start()
    {
        _mixedColor = CMYKUtilites.ConvertRGBToCMYK(_meshRenderer.material.color);
        _oldMixedColor = _mixedColor;
        _activeCauldronColor = _mixedColor;
        _oldActiveCauldronColor = _mixedColor;
    }

    private void Update()
    {
        if (!_blend)
        {
            return;
        }

        LerpColorOnBlend();
    }

    private void LerpColorOnBlend()
    {
        _lerpValue += (_blendSpeed * Time.deltaTime);
        Debug.Log(_lerpValue);

        float[] lerpedColor = colorLerping.colorLerp(CMYKUtilites.Vector4ToFloatArray(_oldActiveCauldronColor), CMYKUtilites.Vector4ToFloatArray(_mixedColor), _lerpValue);

        _activeCauldronColor = CMYKUtilites.FloatArrayToVector4(lerpedColor);
        _meshRenderer.material.color = CMYKUtilites.ConvertCMYKToRGB(_activeCauldronColor);

        if (_lerpValue >= 1)
        {
            _blend = false;
            _lerpValue = 0;

            _oldMixedColor = _mixedColor;
            _oldActiveCauldronColor = _activeCauldronColor;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        PotionScript potion = other.GetComponent<PotionScript>();

        if (potion == null)
        {
            return;
        }

        Vector4 colorCMYK = CMYKUtilites.ConvertRGBToCMYK(potion.GetColor());

        _mixedColor = CMYKUtilites.MixColorsCMYK(colorCMYK, _oldMixedColor);

        _oldMixedColor = _mixedColor;
        _oldActiveCauldronColor = _activeCauldronColor;

        _lerpValue = 0;
        _blend = true;

        Destroy(other.gameObject);
    }


    public void SetCauldronColorCMYK(Vector4 colorCMYK)
    {
        _oldMixedColor = colorCMYK;
        _mixedColor = colorCMYK;
        _oldActiveCauldronColor = _activeCauldronColor;

        _lerpValue = 0;
        _blend = true;
    }

    public Vector4 GetActiceCauldronColor()
    {
        return _activeCauldronColor;
    }
}
