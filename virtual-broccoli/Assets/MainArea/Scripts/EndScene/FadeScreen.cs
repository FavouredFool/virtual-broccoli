using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeScreen : MonoBehaviour
{
    [SerializeField] public float fadeDuration = 2;
    [SerializeField] private Color fadeColor;
    [SerializeField] private AnimationCurve movementCurve;
    [SerializeField] private bool fadeOnStart;

    private Renderer _render;
    private bool _faded;

    // Start is called before the first frame update
    void Start()
    {
        _render = GetComponent<Renderer>();
        if (fadeOnStart) FadeOut();
    }

    public void FadeIn()
    {
        _faded = false;
        Fade(0, 1);
    }

    public void FadeOut()
    {
        Fade(1, 0);
    }

    public void Fade(float alphaIn, float alphaOut)
    {
        StartCoroutine(CheckKeyRotationUpdate(alphaIn, alphaOut));
    }

    IEnumerator CheckKeyRotationUpdate(float alphaIn, float alphaOut)
    {
        float timer = 0;
        while(timer <= fadeDuration)
        {
            Color newColor = fadeColor;
            float percentageCompletedTime = timer / fadeDuration;
            newColor.a = Mathf.Lerp(alphaIn, alphaOut, movementCurve.Evaluate(percentageCompletedTime));

            _render.material.SetColor("_Color", newColor);
            timer += Time.deltaTime;
            yield return null;
        }

        Color otherColor = fadeColor;
        otherColor.a = alphaOut;
        _render.material.SetColor("_Color", otherColor);

        if (alphaOut == 0)
        {
            gameObject.SetActive(false);
        }
        else
        {
            _faded = true;
        }
        yield break;
    }

    public bool GetFaded()
    {
        return _faded;
    }
}
