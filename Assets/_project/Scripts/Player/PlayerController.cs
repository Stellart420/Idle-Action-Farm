using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    [field: SerializeField] public PlayerInventory Inventory { get; private set; }
    [field: SerializeField] public PlayerMovement Movement { get; private set; }
    [field: SerializeField] public PlayerAnimation Animation { get; private set; }
    [field: SerializeField] public PlayerHarvest Harvest { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void Init()
    {
        Inventory.UpdateInventory += GUIController.Instance.SetInventoryCount;
        Movement.Init(this);
        Inventory.Init(this);
        Animation.Init(this);
        Harvest.Init(this);
    }
}
