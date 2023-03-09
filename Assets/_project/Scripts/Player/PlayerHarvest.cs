using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHarvest : PlayerComponent
{
    [SerializeField] private Tool _tool;

    private List<Crop> _crops = new List<Crop>();

    public void CanUseTool(bool is_can)
    {
        _tool.IsWorking = is_can;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out Crop crop))
        {
            if (crop.Health <= 0)
            {
                if (_crops.Contains(crop))
                    _crops.Remove(crop);

                if (_crops.Count <= 0)
                    Controller.Animation.SetHarvesting(false);

                return;
            }

            if (!_crops.Contains(crop))
                _crops.Add(crop);

            Controller.Animation.SetHarvesting(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Crop crop))
        {

            if (_crops.Contains(crop))
                _crops.Remove(crop);

            if (_crops.Count <= 0) 
                Controller.Animation.SetHarvesting(false);
        }
    }
}
