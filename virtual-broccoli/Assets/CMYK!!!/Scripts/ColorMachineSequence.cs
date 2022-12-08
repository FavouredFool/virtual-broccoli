using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    private TMP_Text _cmykInfoText;



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

        // Set actice color every frame for lerp
        _cmykInfoText.text = _screen.ConvertCMYKToString(_cauldronScript.GetActiceCauldronColor());


        if (_resetMix)
        {
            _resetMix = false;
            if (_machineState == MachineState.AWAITCRYSTAL)
            {
                _machineState = MachineState.RESETMACHINE;
            }
            else if (_machineState == MachineState.MIXING)
            {
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
        Debug.Log("LidClosed");
        _machineState = MachineState.FREECOLOR;
    }

    private void FreeColor()
    {
        Debug.Log("Freeing Color: " + _activeCrystalInstruction.GetGoalColors()[_mixIteration - 1]);

        if (_activeCrystalInstruction.GetGoalColors().Count > _mixIteration)
        {
            _machineState = MachineState.RESETPUZZLE;
        }
        else
        {
            ResetMachine();
        }        
    }

    private void ResetPuzzle()
    {
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
        _machineState = MachineState.FREECOLOR;
    }


    private void ResetMachine()
    {
        Debug.Log("ResetMachine");
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
