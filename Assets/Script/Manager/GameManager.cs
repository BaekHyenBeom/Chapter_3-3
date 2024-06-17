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

        // 초기 세팅
    void Start()
    {
        ClickPower += 10; // 초기값;
        curClickPowerTxt.text = $"{ClickPower}";
        curAutoPowerTxt.text = $"{TotalAutoPower}/s";
        StartAutoClicker();

        Invoke("Load", 0.5f);
    }

        // 자동 관련

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

    public void Save()
    {
        saveManager.Save();
    }

    public void Load()
    {
        saveManager.Load();
        Invoke("InitPanel", 0.1f);
    }

    // 이유는 모르겠지만... Hierarchy창에서 Active로 되어있지 않으면 로딩이 제대로 안되는 문제가 있다.
    // 로딩창으로 가려주기 까지 해줘야겠다.
    public void InitPanel()
    {
        AbilityPanel.SetActive(true);
        ShopView.SetActive(false);
        Achievement.SetActive(false);
        LoadingScene.SetActive(false);
    }
}
