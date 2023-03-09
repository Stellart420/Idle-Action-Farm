using EzySlice;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Plane = EzySlice.Plane;

public class WheatField : CropField<Wheat>
{ 
    private void Start()
    {
        _cropArray = new Wheat[_rows, _columns];

        // Populate field with wheat
        for (int row = 0; row < _rows; row++)
        {
            for (int col = 0; col < _columns; col++)
            {
                Vector3 position = new Vector3(row * _spacing, 0, col * _spacing) + _wheatParent.position;
                Wheat wheat = Instantiate(Data.Crop).GetComponent<Wheat>();
                wheat.transform.position = position;
                wheat.transform.SetParent(_wheatParent);
                _cropArray[row, col] = wheat;
                _crops.Add(wheat);
                wheat.Init(Data, _harvestObjPool);
                wheat.Harvest += () => InitGrow(wheat);
            }
        }

        //_crops = _wheatParent.GetComponentsInChildren<Wheat>().ToList();

        //_crops.ForEach(crop =>
        //{
        //    crop.Init(Data);
        //    crop.Harvest += () => InitGrow(crop);
        //});
    }

    internal override IEnumerator GrowWheat(Wheat crop)
    {
        return base.GrowWheat(crop);
    }
}