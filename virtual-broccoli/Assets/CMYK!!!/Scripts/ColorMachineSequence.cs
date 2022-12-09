using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ColorMachineSequence : MonoBehaviour
{
    [SerializeField]
    private bool _crystalInserted = false;

    [SerializeField]
    private CauldronScript _cauldronScript;

    [SerializeField]
    private SlotScript _slot;

    [SerializeField]
    private CrystalColor _insertedCrystal;

    [SerializeField]
    private bool _resetMix;

    [SerializeField]
    private bool _resetMachine;

    [SerializeField]
    private ScreenScript _screen;

    [SerializeField]
    private ColorPipeScript _colorPipe;

    [SerializeField]
    private float _delayTimeForColorReleaseInSeconds = 3f;

    [SerializeField]
    private float _delayTimeForCaudronRefillInSeconds = 1.5f;

    float _timeStartInSeconds = float.PositiveInfinity;



    public enum MachineState { AWAITCRYSTAL, MIXING, CLOSELID, FREECOLOR, RESETPUZZLE, RESETMACHINE };
    public enum CrystalColor { KEY, CYAN, YELLOW, MAGENTA };

    private MachineState _machineState = MachineState.AWAITCRYSTAL;

    private CrystalColor _activeCrystalColor;
    private CrystalInstruction _activeCrystalInstruction;

    int _mixIteration = 1;
    Vector4 _goalColor;

    // Per crystal, set of instructions
    List<CrystalInstruction> _crystalInstructions = new()
    {
        new KeyInstruction(),
        new CyanInstruction(),
        new YellowInstruction(),
        new MagentaInstruction(),
    };

    public void Start()
    {
        ResetMachine();
    }

    public void Update()
    {
        // Awaitcrystal -> CloseLid -> FREECOLOR -> RESETPUZZLE -> Mixing -> FREECOLOR -> RESETPUZZLE -> Mixing -> FREECOLOR -> RESETMACHINE -> AWAITCRYSTAL

        switch (_machineState)
        {
            case MachineState.AWAITCRYSTAL:
                AwaitCrystal();
                break;
            case MachineState.MIXING:
                Mixing();
                break;
            case MachineState.CLOSELID:
                CloseLid();
                break;
            case MachineState.FREECOLOR:
                FreeColor();
                break;
            case MachineState.RESETPUZZLE:
                ResetPuzzle();
                break;
            case MachineState.RESETMACHINE:
                ResetMachine();
                break;
        }

        if (_resetMix)
        {
            _resetMix = false;
            if (_machineState == MachineState.AWAITCRYSTAL)
            {
                ResetTimer();
                _machineState = MachineState.RESETMACHINE;
            }
            else if (_machineState == MachineState.MIXING)
            {
                ResetTimer();
                _machineState = MachineState.RESETPUZZLE;
            }
            else
            {
                Debug.LogWarning("BUTTON UNAVALIABLE");
            }
        }

        if (_resetMachine)
        {
            _resetMachine = false;
            _machineState = MachineState.RESETMACHINE;
        }
    }

    private void AwaitCrystal()
    {
        // Check if crystal is inserted in socket.
        if (_crystalInserted)
        {
            _activeCrystalColor = _insertedCrystal;
            _activeCrystalInstruction = _crystalInstructions[(int)_activeCrystalColor];

            Debug.Log("Crystal Inserted");
            _machineState = MachineState.CLOSELID;
        }
    }

    private void CloseLid()
    {
        bool slotIsClosed = _slot.CloseLidPerFrame();

        if (slotIsClosed)
        {
            Debug.Log("CLOSED");
            ResetTimer();
            _machineState = MachineState.FREECOLOR;
        }
    }

    private void ResetTimer()
    {
        _timeStartInSeconds = Time.time;
    }

    private void FreeColor()
    {
        //StartCoroutine(_colorPipe.ReleaseColor(_activeCrystalInstruction.GetGoalColors()[_mixIteration - 1]));
        if (Time.time - _timeStartInSeconds < _delayTimeForColorReleaseInSeconds)
        {
            // PARTICLES
            return;
        }

        Debug.Log("Color Freed");

        if (_activeCrystalInstruction.GetGoalColors().Count > _mixIteration)
        {
            ResetTimer();
            _machineState = MachineState.RESETPUZZLE;
        }
        else
        {
            ResetTimer();
            _machineState = MachineState.RESETMACHINE;
        }
    }

    private void ResetPuzzle()
    {
        _cauldronScript.SetCauldronColorCMYK(Vector4.zero);

        if (Time.time - _timeStartInSeconds < _delayTimeForCaudronRefillInSeconds)
        {
            // PARTICLES
            return;
        }

        Debug.Log("Resetting Puzzle");

        // Refill Color in Cauldron
        _cauldronScript.SetCauldronColorCMYK(_activeCrystalInstruction.GetGoalColors()[0]);


        // Set Display
        _screen.SetColorAndText(_activeCrystalInstruction.GetGoalColors()[_mixIteration]);

        _machineState = MachineState.MIXING;
        MixInit();
    }

    private void MixInit()
    {
        Debug.Log("InitMixing");
        _goalColor = _activeCrystalInstruction.GetGoalColors()[_mixIteration];
    }

    private void Mixing()
    {

    }

    public void MixComplete()
    {
        _mixIteration += 1;
        ResetTimer();
        _machineState = MachineState.FREECOLOR;
    }


    private void ResetMachine()
    {
        _crystalInserted = false;
        _mixIteration = 1;

        // Empty display or: "Fill Crystal into slot"
        _screen.ResetColorAndText();

        // Refill White in Cauldron
        _cauldronScript.SetCauldronColorCMYK(new Vector4(0, 0, 0, 0));

        // Open Crystalhatch
        _slot.ResetSlot();

        _machineState = MachineState.AWAITCRYSTAL;
    }

    public Vector4 GetGoalColor()
    {
        return _goalColor;
    }

    public MachineState GetMachineState()
    {
        return _machineState;
    }

    public void SetInsertedCrystal(CrystalColor color)
    {
        if (!_crystalInserted)
        {
            _crystalInserted = true;
            _insertedCrystal = color;
        }
        else
        {
            Debug.LogWarning("MACHINE IN WRONG STATE TO PROCESS NEW CRYSTAL");
        }
        

    }

    public void SetResetButton()
    {
        _resetMix = true;
    }
}
