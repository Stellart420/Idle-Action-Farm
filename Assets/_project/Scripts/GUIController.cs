using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GUIController : MonoBehaviour
{
    public static GUIController Instance { get; private set; }

    [field: SerializeField] public Joystick Joystick { get; private set; }
    [field: SerializeField] public TextMeshProUGUI CoinsCountText { get; private set; }

    [SerializeField] private RectTransform _coins;
    [SerializeField] private TextMeshProUGUI _inventoryCountText;
    [SerializeField] private int _coinsChangeValue = 1;

    private int _currentCoins = 0;
    private int _viewCoins = 0;
    private Vector3 _initCoinsPos;

    private void Awake()
    {
        Instance = this;
        _initCoinsPos = _coins.anchoredPosition;
    }

    public void SetInventoryCount(int inventoryCount, int maxSize)
    {
        _inventoryCountText.text = $"{inventoryCount} / {maxSize}";
    }

    public void SetCoins(int count)
    {
        _coins.DOShakePosition(0.1f, new Vector3(5, 5, 0), 10, 90, false, true).OnComplete(() => _coins.anchoredPosition = _initCoinsPos); ;
        _currentCoins = count;
    }

    public void SetCoinsCountText(int count)
    {
        CoinsCountText.text = count.ToString();
        _viewCoins = count;
    }

    private void Update()
    {
        if (_currentCoins != _viewCoins)
        {
            var coins = _viewCoins;
            coins += _coinsChangeValue;
            if (coins > _currentCoins)
                coins = _currentCoins;

            SetCoinsCountText(coins);
        }
    }
}
