using EzySlice;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheat : Crop
{
    [field: SerializeField] public Material Material { get; private set; }
    [field: SerializeField] public GameObject Renderer { get; private set; }
    [SerializeField] private float _cutUpper = 1f;

    [SerializeField] private float _wait = 1f;
    private float _waitX;
    private SlicedHull _slicedWheat;
    private GameObject _bottomPart;
    private GameObject _topPart;
    private bool _firstInit = true;
    public override void Init(CropData data, ObjectPool pool)
    {
        base.Init(data, pool);
        if (_firstInit)
        {
            _slicedWheat = Renderer.Slice(Renderer.transform.position + Vector3.up * _cutUpper, Vector3.up);

            _topPart = _slicedWheat.CreateUpperHull(Renderer);
            _topPart.transform.SetParent(transform, false);

            _bottomPart = _slicedWheat.CreateLowerHull(Renderer);
            _bottomPart.transform.SetParent(transform, false);
            Renderer.GetComponent<MeshRenderer>().enabled = false;
        }

        _bottomPart.SetActive(true);
        _topPart.SetActive(true);
    }

    public override void OnHarvest(Vector3 point)
    {
        if (_waitX > 0) return;
        if (Health <= 0) return;

        Health--;


        if (_topPart.activeSelf)
        {
            _topPart.SetActive(false);
        }
        else
        {
            _bottomPart.SetActive(false);
        }

        var wheatItem = _pool.GetObject().GetComponent<WheatItem>();
        wheatItem.transform.position = point;
        wheatItem.transform.rotation = Quaternion.identity;
        wheatItem.Init(Data, _pool);
        wheatItem.Drop();

        if (Health == 0)
        {
            Harvest?.Invoke();
        }
        _waitX = _wait;
    }

    public override void Reload()
    {
        base.Reload();

        _bottomPart.SetActive(true);
        _topPart.SetActive(true);
    }

    private void Update()
    {
        if (_waitX > 0) _waitX -= Time.deltaTime;
    }
}