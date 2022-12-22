using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Text;

public class ScreenScript : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _cmykText;

    [SerializeField]
    private Image _cmykImage;

    public void SetColorAndText(Vector4 cmykColor)
    {
        _cmykImage.color = CMYKUtilites.ConvertCMYKToRGB(cmykColor);

        _cmykText.text = ConvertCMYKToString(cmykColor);
    }

    public void ResetColorAndText()
    {
        _cmykImage.color = Color.white;
        _cmykText.text = "";
    }

    public string ConvertCMYKToString(Vector4 cmykColor)
    {
        string cyan = (Mathf.Round(cmykColor[0] * 1000f) / 10f).ToString("F1");
        string magenta = (Mathf.Round(cmykColor[1] * 1000f) / 10f).ToString("F1");
        string yellow = (Mathf.Round(cmykColor[2] * 1000f) / 10f).ToString("F1");
        string key = (Mathf.Round(cmykColor[3] * 1000f) / 10f).ToString("F1");


        return string.Format("C:{0,7}%\nM:{1,7}%\nY:{2,7}%\nK:{3,7}%", cyan, magenta, yellow, key);
    }
}
