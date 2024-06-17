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
    public List<AutoClicker> clickUpgrade;

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
    public GameObject AbilityPanel;
    public GameObject ShopView;
    public GameObject Achievement;
    public GameObject LoadingScene;
        
    public SaveAndLoad saveManager;

        // �ʱ� ����
    void Start()
    {
        ClickPower += 10; // �ʱⰪ;
        curClickPowerTxt.text = $"{ClickPower}";
        curAutoPowerTxt.text = $"{TotalAutoPower}/s";
        StartAutoClicker();

        Invoke("Load", 0.5f);
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

    public void Save()
    {
        saveManager.Save();
    }

    public void Load()
    {
        saveManager.Load();
        Invoke("InitPanel", 0.1f);
    }

    // ������ �𸣰�����... Hierarchyâ���� Active�� �Ǿ����� ������ �ε��� ����� �ȵǴ� ������ �ִ�.
    // �ε�â���� �����ֱ� ���� ����߰ڴ�.
    public void InitPanel()
    {
        AbilityPanel.SetActive(true);
        ShopView.SetActive(false);
        Achievement.SetActive(false);
        LoadingScene.SetActive(false);
    }
}
