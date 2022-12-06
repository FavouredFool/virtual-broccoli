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

    private float _customTime = 0;

    private void Start()
    {
        foreach (Transform transform in _transformList)
        {
            InstantiatePotion(transform);
        }
    }

    private void Update()
    {
        List<Transform> transformListWithNoChildren = _transformList.Where(e => e.childCount != 0).ToList();
        Transform frontTransformWithPotion = null;

        if (transformListWithNoChildren.Count != 0)
        {
            frontTransformWithPotion = _transformList.Where(e => e.localPosition.z == transformListWithNoChildren.Max(e => e.localPosition.z)).FirstOrDefault();
        }

        


        Transform lastTransform = _transformList.Where(e => e.localPosition.z == _transformList.Min(e => e.localPosition.z)).FirstOrDefault();

        if (frontTransformWithPotion != null)
        {
            // TODO: THIS IS NOT PERFORMANT, DUNNO IF IT'S A BIG DEAL
            if (frontTransformWithPotion.childCount > 0)
            {
                frontTransformWithPotion.GetChild(0).GetComponent<PotionScript>().ChangeGrabableState(true);
            }


            if (frontTransformWithPotion.localPosition.z > _endTransform.localPosition.z - 0.1f)
            {
                return;
            }
        }



        _customTime += Time.deltaTime;

        for (int i = 0; i < _transformList.Count; i++)
        {
            float basicRepeat = ((float)i) / (_transformList.Count);
            float t = (basicRepeat + _customTime * _speed) % 1f;

            _transformList[i].localPosition = Vector3.Lerp(_startTransform.localPosition, _endTransform.localPosition, t);
        }

        if (lastTransform != null && lastTransform.childCount == 0)
        {
            InstantiatePotion(lastTransform);
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
