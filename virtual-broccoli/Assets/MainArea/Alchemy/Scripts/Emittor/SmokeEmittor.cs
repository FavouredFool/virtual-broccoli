using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeEmittor : MonoBehaviour
{
    [SerializeField]
    ParticleSystem _ps;

    public void ReleaseSmoke(Vector4 cmyk)
    {
        ParticleSystem.MainModule settings = _ps.main;
        settings.startColor = new ParticleSystem.MinMaxGradient(CMYKUtilites.ConvertCMYKToRGB(cmyk));

        _ps.Play();
    }


}
