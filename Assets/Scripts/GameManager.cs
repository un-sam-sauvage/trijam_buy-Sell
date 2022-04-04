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

    public int basePlayerMoney;
    private static GameManager _gameManager;

    public static GameManager Instance => _gameManager;

    private HousePlacer _housePlacer;
    [SerializeField] private GameObject panelEndGame;
  

    private void Awake()
    {
        _gameManager = this;
        _housePlacer = FindObjectOfType<HousePlacer>();
    }

    void Start()
    {
        _sellManager = SellManager.Instance;
        SetUpPlayerMoney(0);
        _housePlacer.onPlayerLoose += PlayerGameOver;
    }
    
    public void SetUpPlayerMoney(int value)
    {
        basePlayerMoney = basePlayerMoney + value;
        moneyText.text = basePlayerMoney.ToString();
       
    }

    private void PlayerGameOver()
    {
        panelEndGame.SetActive(true);
    }

    private void OnDisable()
    {
        _housePlacer.onPlayerLoose -= PlayerGameOver;
    }
}
