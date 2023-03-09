using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropField<T> : MonoBehaviour where T : Crop
{
    [field: SerializeField] public CropData Data { get; private set; }
    [SerializeField] internal ObjectPool _harvestObjPool;
    [SerializeField] internal Transform _wheatParent;
    [SerializeField] internal float _growTime = 10f;
    [SerializeField] internal int _rows;
    [SerializeField] internal int _columns;
    [SerializeField] internal float _spacing;

    internal List<T> _crops = new List<T>();

    internal T[,] _cropArray;
    internal WaitForSeconds _waitGrow;

    private void Awake()
    {
        _waitGrow = new WaitForSeconds(_growTime);
    }

    public void HarvestCrop(T crop)
    {
        StartCoroutine(GrowWheat(crop));
    }

    internal virtual void InitGrow(T crop)
    {
        StartCoroutine(GrowWheat(crop));
    }
    internal virtual IEnumerator GrowWheat(T crop)
    {
        yield return _waitGrow;

        crop.Reload();
    }
}
