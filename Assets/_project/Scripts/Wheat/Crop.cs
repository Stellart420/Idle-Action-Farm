using System;
using UnityEngine;

public class Crop : MonoBehaviour
{ 
    public int Health { get; internal set; } = 2;
    public CropData Data { get; internal set; }

    public Action Harvest;

    internal ObjectPool _pool;

    public virtual void Init(CropData data, ObjectPool pool)
    {
        Data = data;
        Health = 2;
        _pool = pool;
    }

    public virtual void Reload()
    {
        Health = 2;
    }

    public virtual void OnHarvest(Vector3 point)
    {
    }
}