using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerInventory : PlayerComponent
{
    [SerializeField] private int _maxStackSize = 40; // максимальный размер стака
    [SerializeField] private Transform _inventoryParent;
    [SerializeField] private float _spaceItem = 0.05f;

    private int _currentStackSize = 0; // текущий размер стака

    private List<InventoryItem> _inventoryItems = new List<InventoryItem>();

    public Action<int, int> UpdateInventory;

    public override void Init(PlayerController controller)
    {
        base.Init(controller);

        UpdateInventory?.Invoke(_inventoryItems.Count, _maxStackSize);
    }
    public bool AddItemToInventory()
    {
        if (_currentStackSize >= _maxStackSize) return false;

        _currentStackSize++;
        return true;
    }

    public void FlyObjectInStack(HarvestItem item)
    {
        item.RB.useGravity = false;
        item.RB.isKinematic = true;
        var startScale = item.transform.localScale;
        item.transform.localScale = startScale;
        item.transform.localPosition = item.transform.localPosition;

        var targetPositionInStack = new Vector3(0f, _spaceItem * _currentStackSize);
        var targetPosition = targetPositionInStack + _inventoryParent.position;
        // Задаем начальные значения анимации
        item.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        item.transform.DOMove(targetPosition + Vector3.up * 2f, 0.5f).SetEase(Ease.InOutQuad);
        item.transform.DORotate(Vector3.zero, 0.5f).SetEase(Ease.InOutQuad);
        item.transform.DOScale(startScale * 0.5f, 0.5f).SetEase(Ease.InOutQuad).OnComplete(() =>
        {
            item.transform.SetParent(_inventoryParent);
            item.transform.DOLocalMove(targetPositionInStack, 0.25f).SetEase(Ease.InOutQuad).OnComplete(()=>
            {
                item.Return();
                var inventoryItem = Instantiate(item.Data.InventoryItem, targetPositionInStack, Quaternion.identity, _inventoryParent);
                inventoryItem.transform.localPosition = targetPositionInStack;
                inventoryItem.transform.localRotation = Quaternion.identity;
                inventoryItem.Init(item.Data.Price);
                _inventoryItems.Add(inventoryItem);
                UpdateInventory?.Invoke(_inventoryItems.Count, _maxStackSize);
            });
        });
    }

    public bool RemoveItemFromInventory(CropData itemName, int amount = 1)
    {
        //// проверяем, есть ли блок в инвентаре
        //if (_inventory.ContainsKey(itemName))
        //{
        //    // проверяем, достаточно ли блоков для удаления
        //    if (_inventory[itemName] >= amount)
        //    {
        //        // удаляем блок из инвентаря
        //        _inventory[itemName] -= amount;

        //        // уменьшаем текущий размер стака
        //        _currentStackSize -= amount;

        //        return true;
        //    }
        //    else
        //    {
        //        Debug.Log("Not enough items to remove from inventory!");
        //        return false;
        //    }
        //}
        //else
        //{
        //    Debug.Log("Item not found in inventory!");
        return false;
        //}
    }


    public bool RemoveItemFromInventory(out InventoryItem item)
    {
        item = null;

        if (_currentStackSize <= 0) return false;

        item = _inventoryItems.LastOrDefault();
        _inventoryItems.Remove(item);
        _currentStackSize--;
        UpdateInventory?.Invoke(_inventoryItems.Count, _maxStackSize);
        return true;
    }
}
