
using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    public int Price { get; private set; }
    public void Init(int price)
    {
        Price = price;
    }
}
