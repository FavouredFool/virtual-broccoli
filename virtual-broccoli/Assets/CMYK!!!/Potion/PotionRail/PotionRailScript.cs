using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PotionRailScript : MonoBehaviour
{
    [SerializeField]
    private GameObject _potionBlueprint;

    [SerializeField]
    private Material _potionMaterial;

    [SerializeField]
    private Transform _startTransform;

    [SerializeField]
    private Transform _endTransform;

    [SerializeField]
    private List<Transform> _transformList;

    [SerializeField]
    private float _speed = 0.1f;

    [SerializeField]
    private float _distanceToOtherPotion = 1f;

    private void Awake()
    {
        for (int i = 0; i < _transformList.Count; i++)
        {
            _transformList[i].localPosition = new Vector3(0, 0, _endTransform.localPosition.z - 0.1f - i * _distanceToOtherPotion + 0.1f);

            InstantiatePotion(_transformList[i]);
        }
    }

    private void Update()
    {
        Transform frontTransformWithPotion = _transformList.Where(e => e.localPosition.z == _transformList.Max(e => e.localPosition.z)).FirstOrDefault();
        
        float endZ;
        bool changeGrappable;


        for (int i = 0; i < _transformList.Count; i++)
        {
            if (frontTransformWithPotion.localPosition == _transformList[i].localPosition)
            {
                endZ = _endTransform.localPosition.z - 0.1f;
                changeGrappable = true;
            }
            else
            {
                endZ = _endTransform.localPosition.z - _distanceToOtherPotion - 0.1f;
                changeGrappable = false;
            }

            if (_transformList[i].localPosition.z > endZ)
            {
                if (_transformList[i].childCount > 0)
                {
                    if (changeGrappable)
                    {
                        frontTransformWithPotion.GetChild(0).GetComponent<PotionScript>().ChangeGrabableState(true);
                    }
                }
                else
                {
                    _transformList[i].localPosition = _startTransform.localPosition;
                    InstantiatePotion(_transformList[i]);
                }

                continue;
            }
            _transformList[i].localPosition = Vector3.MoveTowards(_transformList[i].localPosition, _endTransform.localPosition, Time.deltaTime * _speed);
        }

    }

    private void InstantiatePotion(Transform parent)
    {
        PotionScript potion = Instantiate(_potionBlueprint, parent).GetComponent<PotionScript>();
        potion.transform.localPosition = Vector3.zero;
        potion.SetFluidMaterial(_potionMaterial);
        potion.ChangeGrabableState(false);
    }


}
