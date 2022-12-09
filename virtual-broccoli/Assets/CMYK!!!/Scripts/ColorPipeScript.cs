using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPipeScript : MonoBehaviour
{

    public IEnumerator ReleaseColor(Vector4 cmyk)
    {
        yield return new WaitForSeconds(1f);

        Debug.Log(cmyk + " released");
    }

}
