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
                _fluidMaterialLists[i][j].SetColor("_Color", _fluidColors[i*j]);
            }
        }
    }

    public void ColorPrimaryMaterialOfColor(CrystalColor crystalColor)
    {
        ColorMaterialByIndex((int)crystalColor, 0);
    }

    public void ColorOtherMaterialsOfColor(CrystalColor crystalColor)
    {
        for(int i = 1; i < _fluidMaterialLists[(int)crystalColor].Count; i++)
        {
            ColorMaterialByIndex((int)crystalColor, i);
        }
    }

    public void ColorMaterialByIndex(int listIndex, int elementIndex)
    {
        _fluidMaterialLists[listIndex][elementIndex].SetColor("_Color", _fluidColors[listIndex * elementIndex]);
    }

    public Material GetBlack()
    {
        return _fluidMaterialLists[0][0];
    }
}
