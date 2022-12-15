using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using colorKit;

public class CMYKUtilites : MonoBehaviour
{
    public static Color ConvertCMYKToRGB(Vector4 cmyk)
    {
        float r;
        float g;
        float b;

        r = (1 - cmyk.x) * (1 - cmyk.w);
        g = (1 - cmyk.y) * (1 - cmyk.w);
        b = (1 - cmyk.z) * (1 - cmyk.w);

        return new Color(r, g, b, 1);
    }

    public static Vector4 ConvertRGBToCMYK(Color rgb)
    {
        if (rgb.r == 0 && rgb.g == 0 && rgb.b == 0)
            return new Vector4(0, 0, 0, 1);
        else if (rgb.r == 1 && rgb.g == 1 && rgb.b == 1)
            return new Vector4 ( 0, 0, 0, 0 );

        float cyan = 1 - rgb.r;
        float magenta = 1 - rgb.g;
        float yellow = 1 - rgb.b;
        float black = Mathf.Min(cyan, magenta, yellow);
        cyan = (cyan - black) / (1 - black);
        magenta = (magenta - black) / (1 - black);
        yellow = (yellow - black) / (1 - black);

        return new Vector4(cyan, magenta, yellow, black);
    }

    public static Vector4 MixColorsCMYK(Vector4 color1, Vector4 color2)
    {
        float[] quants = { 1, 1 };
        List<float[]> colors = new() { Vector4ToFloatArray(color1), Vector4ToFloatArray(color2) };

        float[] mixedColor = mixingMethods.mixColors(colors, quants, mixingMethod.colorAveraging, false);

        return FloatArrayToVector4(mixedColor);
    }

    public static Vector4 FloatArrayToVector4(float[] floatArray)
    {
        return new Vector4(floatArray[0], floatArray[1], floatArray[2], floatArray[3]);
    }

    public static float[] Vector4ToFloatArray(Vector4 vector4)
    {
        float[] floatArray = { vector4.x, vector4.y, vector4.z, vector4.w };
        return floatArray;
    }


}
