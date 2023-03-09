using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    public int CurrentCoins 
    { 
        get => _currentCoins;
        set
        {
            _currentCoins = value;
            GUIController.Instance.SetCoins(_currentCoins);
        }
    }

    public Action<int> ChangeCoinsCount;

    private int _currentCoins;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        CurrentCoins = 0;
        PlayerController.Instance.Init();
    }

    public void AddCoins(int amount = 1)
    {
        CurrentCoins += amount;
    }
}
