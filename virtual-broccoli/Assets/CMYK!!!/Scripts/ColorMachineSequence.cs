using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorMachineSequence : MonoBehaviour
{
    [SerializeField]
    private bool _crystalInserted = false;

    [SerializeField]
    private CauldronScript _cauldronScript;

    [SerializeField]
    private CrystalColor _insertedCrystal;

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
                break;
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
        //_mixIteration += 1;
        //_machineState = MachineState.FREECOLOR;
    }


    private void ResetMachine()
    {
        Debug.Log("ResetMachine");
        _crystalInserted = false;
        _mixIteration = 1;

        // Empty display or: "Fill Crystal into slot"

        // Refill White in Cauldron
        _cauldronScript.SetCauldronColorCMYK(new Vector4(0, 0, 0, 0));

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
}
