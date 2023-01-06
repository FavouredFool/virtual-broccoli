using UnityEngine;
using System.Collections.Generic;
using static ColorMachine;

public class ColorManager : MonoBehaviour
{
    [SerializeField]
    private List<Material> _keyMaterials;
    [SerializeField]
    private List<Material> _cyanMaterials;
    [SerializeField]
    private List<Material> _magentaMaterials;
    [SerializeField]
    private List<Material> _yellowMaterials;

    private readonly List<List<Material>> _fluidMaterialLists = new();

    private List<Color> _fluidColors = new();

    public void Awake()
    {

        // Due to float-point imprecisions we need to manually reset some colors. This is a terrible hack buuut we don't have the time for a better solution.
        // grey
        _keyMaterials[3].SetColor("_Color", new Color(0.5f, 0.5f, 0.5f));

        // Light cyan
        _cyanMaterials[2].SetColor("_Color", new Color(0.5f, 1f, 1f));
        // Dark cyan
        _cyanMaterials[3].SetColor("_Color", new Color(0.25f, 0.5f, 0.5f));

        // Light magenta
        _magentaMaterials[2].SetColor("_Color", new Color(1f, 0.5f, 1f));
        // Dark Magenta
        _magentaMaterials[3].SetColor("_Color", new Color(0.5f, 0.25f, 0.5f));

        // Light Yellow
        _yellowMaterials[2].SetColor("_Color", new Color(1f, 1f, 0.5f));
        // Dark Yellow
        _yellowMaterials[3].SetColor("_Color", new Color(0.5f, 0.5f, 0.25f));

        _fluidMaterialLists.Add(_keyMaterials);
        _fluidMaterialLists.Add(_cyanMaterials);
        _fluidMaterialLists.Add(_magentaMaterials);
        _fluidMaterialLists.Add(_yellowMaterials);

        foreach(List<Material> materialList in _fluidMaterialLists)
        {
            foreach (Material material in materialList)
            {
                _fluidColors.Add(material.GetColor("_Color"));
                material.SetColor("_Color", Color.white);
            }
        }
    }

    public void OnApplicationQuit()
    {
        for (int i = 0; i < _fluidMaterialLists.Count; i++)
        {
            for (int j = 0; j < _fluidMaterialLists[i].Count; j++)
            {
                ColorMaterialByIndex(i, j);
            }
        }
    }

    public void ColorPrimaryMaterialOfColor(CrystalColor crystalColor)
    {
        for (int i = 0; i < 2; i++)
        {
            ColorMaterialByIndex((int)crystalColor, i);
        }
    }

    public void ColorOtherMaterialsOfColor(CrystalColor crystalColor)
    {
        for(int i = 2; i < _fluidMaterialLists[(int)crystalColor].Count; i++)
        {
            ColorMaterialByIndex((int)crystalColor, i);
        }
    }

    public void ColorMaterialByIndex(int listIndex, int elementIndex)
    {
        _fluidMaterialLists[listIndex][elementIndex].SetColor("_Color", _fluidColors[listIndex * _fluidMaterialLists[listIndex].Count + elementIndex]);
    }
}
