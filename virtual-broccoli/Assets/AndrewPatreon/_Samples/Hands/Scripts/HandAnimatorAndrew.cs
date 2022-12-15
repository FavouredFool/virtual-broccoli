using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
public class HandAnimatorAndrew : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float speed = 10.0f;

    [Header("Input")]
    [SerializeField] private InputActionReference thumbAction;
    [SerializeField] private InputActionReference pointAction;
    [SerializeField] private InputActionReference gripAction;

    private List<BaseInputApplier> inputAppliers = null;

    private void Awake()
    {
        SetupAppliers();
    }

    private void SetupAppliers()
    {
        Animator animator = GetComponent<Animator>();

        inputAppliers = new List<BaseInputApplier>
        {
            new LayerInputApplier(thumbAction.action, animator, "Thumb Layer", true),
            new LayerInputApplier(pointAction.action, animator, "Point Layer", true),
            new ParameterInputApplier(gripAction.action, animator, "Grip", false)
        };
    }

    private void Update()
    {
        UpdateAppliers();
    }

    private void UpdateAppliers()
    {
        foreach (BaseInputApplier applier in inputAppliers)
            applier.Apply(speed);
    }
}
