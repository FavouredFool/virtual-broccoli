using UnityEngine;
using System.Collections.Generic;

public class ColorMachine : StateMachine
{
    public enum CrystalColor { KEY, CYAN, MAGENTA, YELLOW, NONE };

    [SerializeField]
    private CrystalColor _activeCrystal = CrystalColor.NONE;

    [SerializeField]
    private bool _resetMix;

    [SerializeField]
    private bool _resetMachine;

    [SerializeField]
    private float _colorReleaseTimeInSeconds = 3f;

    [SerializeField]
    private float _cauldronRefillTimeInSeconds = 1.5f;

    [SerializeField]
    private ColorManager _colorManager;

    [SerializeField]
    private CauldronScript _cauldronScript;

    [SerializeField]
    private SlotScript _slot;

    [SerializeField]
    private ScreenScript _screen;

    [SerializeField]
    private SmokeEmittor _emittor;

    int _mixIteration = 1;

    readonly List<CrystalInstruction> _crystalInstructions = new()
    {
        new KeyInstruction(),
        new CyanInstruction(),
        new MagentaInstruction(),
        new YellowInstruction(),
    };

    private void Start()
    {
        SetState(new ResetMachineState(this));
    }

    private void Update()
    {
        UpdateState();

        if (_resetMachine)
        {
            _resetMachine = false;
            SetState(new ResetMachineState(this));
        }
    }


    public CrystalColor GetActiveCrystal()
    {
        return _activeCrystal;
    }

    public SlotScript GetSlot()
    {
        return _slot;
    }

    public List<CrystalInstruction> GetCrystalInstructions()
    {
        return _crystalInstructions;
    }

    public CrystalInstruction GetActiveCrystalInstruction()
    {
        return GetCrystalInstructions()[(int)GetActiveCrystal()];
    }

    public int GetMixIteration()
    {
        return _mixIteration;
    }

    public CauldronScript GetCauldron()
    {
        return _cauldronScript;
    }

    public float GetCauldronRefillTime()
    {
        return _cauldronRefillTimeInSeconds;
    }
    public float GetColorReleaseTime()
    {
        return _colorReleaseTimeInSeconds;
    }

    public ScreenScript GetScreen()
    {
        return _screen;
    }

    public void SetActiveCrystal(CrystalColor activeCrystal)
    {
        _activeCrystal = activeCrystal;
    }

    public void SetMixIteration(int mixIteration)
    {
        _mixIteration = mixIteration;
    }

    public bool GetResetMix()
    {
        return _resetMix;
    }

    public void SetResetMix(bool resetMix)
    {
        _resetMix = resetMix;
    }

    public SmokeEmittor GetSmokeEmittor()
    {
        return _emittor;
    }

    public ColorManager GetColorManager()
    {
        return _colorManager;
    }
}
