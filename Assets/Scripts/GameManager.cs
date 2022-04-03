using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private SellManager _sellManager;
    [SerializeField]
    private TextMeshProUGUI moneyText;

    [SerializeField] private int basePlayerMoney;
    private static GameManager _gameManager;

    public static GameManager Instance => _gameManager;


    private void Awake()
    {
        _gameManager = this;
    }

    void Start()
    {
        _sellManager = SellManager.Instance;
        SetUpPlayerMoney(0);
    }
    
    public void SetUpPlayerMoney(int value)
    {
        basePlayerMoney = basePlayerMoney + value;
        moneyText.text = basePlayerMoney.ToString();
    }
}
