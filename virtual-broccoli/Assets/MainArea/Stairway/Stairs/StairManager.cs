using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairManager : MonoBehaviour
{
    public enum StairRotation { RIGHT, LEFT };

    public enum StairColor { KEY, CYAN, MAGENTA, YELLOW, WHITE };

    [SerializeField]
    private Transform _stairParent;

    [SerializeField]
    private Transform _notchParent;

    [SerializeField]
    private Transform _leverParent;

    [SerializeField]
    private List<Material> _materials;

    public void Awake()
    {
        foreach (Transform stairRotationTransform in _stairParent)
        {
            global::StairRotationScript stairRotation = stairRotationTransform.GetComponent<global::StairRotationScript>();

            if (stairRotation == null)
            {
                Debug.LogWarning("yikes");
                break;
            } 

            stairRotation.SetStairManager(this);
        }

        foreach (Transform stairLeverTransform in _leverParent)
        {
            StairLever stairLever = stairLeverTransform.GetComponent<StairLever>();

            if (stairLever == null)
            {
                Debug.LogWarning("yikes");
                break;
            }

            stairLever.SetStairManager(this);
        }
    }


    public List<global::StairRotationScript> GetAllStairs()
    {
        List<global::StairRotationScript> stairList = new();

        foreach (Transform stairRotationTransform in _stairParent)
        {
            global::StairRotationScript stairRotation = stairRotationTransform.GetComponent<global::StairRotationScript>();

            if (stairRotation == null)
            {
                Debug.LogWarning("yikes");
                break;
            }

            stairList.Add(stairRotation);
        }

        return stairList;
    }


    public Material StairColorToMaterial(StairColor color)
    {
        switch (color)
        {
            case StairColor.KEY:
                return _materials[0];
            case StairColor.CYAN:
                return _materials[1];
            case StairColor.MAGENTA:
                return _materials[2];
            case StairColor.YELLOW:
                return _materials[3];
            case StairColor.WHITE:
                return _materials[4];
            default:
                Debug.LogError("Yikes");
                return null;
        }
    }

    public Material GetMaterialByIndex(int index)
    {
        return _materials[index];
    }

    public Transform GetStairParent()
    {
        return _stairParent;
    }

    public Transform GetNotchParent()
    {
        return _notchParent;
    }
}
