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
    private long clickPower;
    public long ClickPower
    {
        get { return clickPower; }
        set
        {
            clickPower = value;
            curClickPowerTxt.text = $"{clickPower}";
        }
    }
    public TextMeshProUGUI curClickPowerTxt;

    [Header("Auto Settings")]
    private long totalAutoPower;
    public long TotalAutoPower
    {
        get { return totalAutoPower; }
        set
        {
            totalAutoPower = value;
            curAutoPowerTxt.text = $"{totalAutoPower}/s";
        }
    }
    public List<AutoClicker> autoClickers;
    public TextMeshProUGUI curAutoPowerTxt;
    private Coroutine autoCoroutine;

    [Header("UI Objects")]
    public TextMeshProUGUI curMoneyTxt;

        // �ʱ� ����
    void Start()
    {
        ClickPower += 10; // �ʱⰪ;
        curClickPowerTxt.text = $"{ClickPower}";
        curAutoPowerTxt.text = $"{TotalAutoPower}/s";
        StartAutoClicker();
    }

        // �ڵ� ����

    public void CheckAllAutoAmount()
    {
        long totalValue = 0;
        foreach(AutoClicker auto in autoClickers)
        {
            totalValue += auto.CheckValue();
        }
        TotalAutoPower = totalValue;
    }

    public void StartAutoClicker()
    {
        if (autoCoroutine == null)
        {
            autoCoroutine = StartCoroutine(AutoClickerActivate());
        }
    }

    IEnumerator AutoClickerActivate()
    {
        while(true)
        {
            CurMoney += TotalAutoPower;
            yield return new WaitForSeconds(1f);
        }
    }

        // �� ȹ��
    public void EarnMoney(long value)
    {
        CurMoney += value;
    }

        // UI ����
    private void ChangecurMoneyUI()
    {
        curMoneyTxt.text = CurMoney.ToString();
    }
}
