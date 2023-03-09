
using UnityEngine;

[CreateAssetMenu(fileName = "NewCropData", menuName = "CropData", order = 1)]
public class CropData : ScriptableObject
{
    public Crop Crop;
    public HarvestItem HarvestItem;
    public InventoryItem InventoryItem;
    public int Price = 1;
}
