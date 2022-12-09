using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static ColorMachine;

public class SphereDisplay : MonoBehaviour
{
    [SerializeField]
    CrystalColor _color;

    [SerializeField, Range(0, 1)]
    float fillHeight;

    [SerializeField]
    MeshRenderer _meshRenderer;

    [SerializeField]
    CauldronScript _cauldron;

    [SerializeField]
    TMP_Text _text;

    float _fillValue = 0;

    private void Start()
    {

    }

    private void Update()
    {
        _fillValue = GetAppropriateValueFromColor(_cauldron.GetActiceCauldronColor());

        string valueString = (Mathf.Round(_fillValue * 1000f) / 10f).ToString("F1");
        _text.text = valueString + "%";
        _meshRenderer.material.SetFloat("_Fill", _fillValue);
    }


    private float GetAppropriateValueFromColor(Vector4 cmyk)
    {
        switch (_color)
        {
            case CrystalColor.KEY:
                return cmyk[3];
            case CrystalColor.CYAN:
                return cmyk[0];
            case CrystalColor.MAGENTA:
                return cmyk[1];
            case CrystalColor.YELLOW:
                return cmyk[2];
        }

        Debug.LogWarning("FEHLER");
        return -1f;
    }
}
