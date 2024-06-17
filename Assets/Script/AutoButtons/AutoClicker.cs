using TMPro;
using UnityEngine;

public class AutoClicker : MonoBehaviour
{
    [Header("UI Settings")]
    public TextMeshProUGUI nameTxt;
    public TextMeshProUGUI upgradeTxt;
    public TextMeshProUGUI descTxt;
    public TextMeshProUGUI priceTxt;

    public ShopItemSO itemSO;

    private long curValue;
    private long curPrice;
    private long curUpgrade;

    void Awake()
    {
        curValue = itemSO.initAmount;
        curPrice = itemSO.priceInitAmount;
        UISetting();
    }

    public void Buy()
    {
        if (CheckMoney())
        {
            GameManager.Instance.CurMoney -= curPrice;
            if (curUpgrade == 0 && itemSO.isClick == false)
            {
                GameManager.Instance.autoClickers.Add(this);
                curPrice += itemSO.priceWeightAmount + curPrice;

                curUpgrade++;
            }
            else if (curUpgrade == 0 && itemSO.isClick == true) // 저장 때문에
            {
                GameManager.Instance.clickUpgrade.Add(this);
                GameManager.Instance.ClickPower += curValue;
                curValue += itemSO.weightAmount;
                curPrice += itemSO.priceWeightAmount + curPrice;

                curUpgrade++;
            }
            else if (itemSO.isClick == true)
            {
                GameManager.Instance.ClickPower += curValue;

                // 클릭은 좀 판정이 다름
                curValue += itemSO.weightAmount;
                curPrice += itemSO.priceWeightAmount + curPrice;

                curUpgrade++;
            }
            else
            {
                curValue += curValue;
                curPrice += itemSO.priceWeightAmount + curPrice;

                curUpgrade++;
            }
        }
        else
        {
            Debug.Log("돈이 부족합니다!");
        }
        GameManager.Instance.CheckAllAutoAmount();
        UISetting();
    }

    public long CheckValue()
    {
        return curValue;
    }

    public long CheckUpgrade()
    {
        return curUpgrade;
    }

    public long CheckPrice()
    {
        return curPrice;
    }

    public bool CheckMoney()
    {
        if (GameManager.Instance.CurMoney >= curPrice)
        {
            Debug.Log("구매 완료");
            return true;
        }
        return false;
    }

    private void UISetting()
    {
        nameTxt.text = itemSO._name;
        upgradeTxt.text = $"+{curUpgrade}";
        descTxt.text = $"{itemSO._desc} 가격 :";
        priceTxt.text = $"{curPrice}";
    }

    public void LoadData(long _curValue ,long _curUpgrade, long _curPrice)
    {
        curValue = _curValue;
        curUpgrade = _curUpgrade;
        curPrice = _curPrice;
        UISetting();
        if (itemSO.isClick == true)
        {
            if (!GameManager.Instance.clickUpgrade.Contains(this))
            {
                GameManager.Instance.clickUpgrade.Add(this);
            }
        }
        else if (itemSO.isClick == false)
        {
            if (!GameManager.Instance.autoClickers.Contains(this))
            {
                GameManager.Instance.autoClickers.Add(this);
            }
        }
    }
}
