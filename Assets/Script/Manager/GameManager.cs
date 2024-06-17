using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private long curMoney;
    public long CurMoney
    {
        get { return curMoney; }
        set
        {
            curMoney = value;
            ChangecurMoneyUI();
        }
    }

    [Header("Click Settings")]
    public long clickPower;
    public TextMeshProUGUI curClickPowerTxt;

    [Header("Auto Settings")]
    public long totalAutoPower;
    // public List<GameObject> 그 autoClicker 인터페이스 만들어서 모아줘야할 듯..
    public TextMeshProUGUI curAutoPowerTxt;

    [Header("UI Objects")]
    public TextMeshProUGUI curMoneyTxt;

        // 초기 세팅
    void Start()
    {
        curClickPowerTxt.text = $"{clickPower}";
        curAutoPowerTxt.text = $"{totalAutoPower}/s";
    }

        // 돈 획득
    public void EarnMoney(long value)
    {
        CurMoney += value;
    }

        // UI 관련
    private void ChangecurMoneyUI()
    {
        curMoneyTxt.text = CurMoney.ToString();
    }
}
