using Pixelplacement;
using UnityEngine;

public class FadeSphere : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LeapProvider leapProvider;

    [Header("Settings")]
    [SerializeField, Range(0, 1)] private float fadeTime = 0.25f;
    [SerializeField, Range(-1.0f, 1.0f)] private float fadeAmount = 0.45f;

    private MeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnEnable()
    {
        Tween.ShaderFloat(meshRenderer.material, "_Amount", fadeAmount, fadeTime, 0);
    }

    private void OnDisable()
    {
        Tween.ShaderFloat(meshRenderer.material, "_Amount", -1.0f, fadeTime, 0);
    }
     
    private void Update()
    {
        UpdateMaterial();
    }

    private void UpdateMaterial()
    {
        meshRenderer.material.SetVector("_Direction", FindDirection());
    }

    private Vector3 FindDirection()
    {
        Vector3 cameraOffset = Vector3.up * leapProvider.system.xrOrigin.CameraInOriginSpaceHeight;
        Vector3 curveCameraPosition = leapProvider.CurvePosition + cameraOffset;

        Vector3 direction = (transform.position - curveCameraPosition).normalized;
        return transform.InverseTransformDirection(direction);
    }
}
