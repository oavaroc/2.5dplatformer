using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField]
    private TextMeshProUGUI _CoinCount;
    [SerializeField]
    private TextMeshProUGUI _LivesCount;

    private int _Coins = 0;
    private int _Lives = 0;

    public void AddCoins(int coin)
    {
        Debug.Log("Adding coin: " + coin);
        _Coins += coin;
        _CoinCount.text = _Coins.ToString();
    }

    public int GetCoins()
    {
        return _Coins;
    }

    public int UpdateLives(int lives)
    {
        Debug.Log("Updating lives: " + lives);
        _Lives += lives;
        _LivesCount.text = _Lives.ToString();
        return _Lives;
    }
}
