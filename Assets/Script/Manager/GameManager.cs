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
    // public List<GameObject> �� autoClicker �������̽� ���� �������� ��..
    public TextMeshProUGUI curAutoPowerTxt;

    [Header("UI Objects")]
    public TextMeshProUGUI curMoneyTxt;

        // �ʱ� ����
    void Start()
    {
        curClickPowerTxt.text = $"{clickPower}";
        curAutoPowerTxt.text = $"{totalAutoPower}/s";
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
