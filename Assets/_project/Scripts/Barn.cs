using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Barn : MonoBehaviour
{
    [SerializeField] private float _getItemWait = 0.15f;
    [SerializeField] private Transform _takePoint;
    [SerializeField] private ObjectPool _coinsPool;

    private PlayerInventory _currentInventory;
    private WaitForSeconds _wait;
    private void Awake()
    {
        _wait = new WaitForSeconds(_getItemWait);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerController controller))
        {
            _currentInventory = controller.Inventory;
            StartCoroutine(GetItem());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out PlayerController controller))
        {
            _currentInventory = null;
            StopCoroutine(GetItem());
        }
    }

    IEnumerator GetItem()
    {
        while (_currentInventory != null && _currentInventory.RemoveItemFromInventory(out InventoryItem item))
        {
            FlyObjectInBarn(item);
            yield return _wait;
        }
    }
    public void FlyObjectInBarn(InventoryItem item)
    {
        item.transform.SetParent(null);

        var targetPosition = _takePoint.position;
        item.transform.DOJump(targetPosition, 2.5f, 1, 1f).OnComplete(() =>
        {
            FlyCoin(item.Price);
            Destroy(item.gameObject);
        });
    }

    private void FlyCoin(int price)
    {
        // ¬ычисл€ем позицию цели в мировых координатах
        // ѕреобразуем позицию RectTransform в мировые координаты
        var rect = GUIController.Instance.CoinsCountText.rectTransform;
        Vector3 worldPos = rect.TransformPoint(rect.rect.center);
        // ѕреобразуем мировые координаты в экранные координаты
        Vector3 targetPosition;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(rect, worldPos,Camera.main, out targetPosition);

        //Vector3 targetPosition = Camera.main.ScreenToWorldPoint(GUIController.Instance.CoinsCountText.transform.position);

        GameObject coin = _coinsPool.GetObject();
        coin.transform.position = _takePoint.position;
        coin.transform.DOMove(targetPosition, 0.5f).OnComplete(() =>
        {
            GameController.Instance.AddCoins(price);
            _coinsPool.ReturnObject(coin);
        });

    }
}
