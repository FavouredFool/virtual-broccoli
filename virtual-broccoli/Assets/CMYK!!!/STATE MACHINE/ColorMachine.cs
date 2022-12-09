using UnityEngine;
using System.Collections.Generic;

public class ColorMachine : StateMachine
{
    public enum CrystalColor { KEY, CYAN, YELLOW, MAGENTA, NONE };

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
    private CauldronScript _cauldronScript;

    [SerializeField]
    private SlotScript _slot;

    [SerializeField]
    private ScreenScript _screen;

    int _mixIteration = 1;

    Vector4 _goalColor = Vector4.zero;

    readonly List<CrystalInstruction> _crystalInstructions = new()
    {
        new KeyInstruction(),
        new CyanInstruction(),
        new YellowInstruction(),
        new MagentaInstruction(),
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

    public void MixComplete()
    {
        _mixIteration += 1;

        SetState(new FreeColorState(this));
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

    public Vector4 GetGoalColor()
    {
        return _goalColor;
    }

    public void SetGoalColor(Vector4 goalColor)
    {
        _goalColor = goalColor;
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
}
