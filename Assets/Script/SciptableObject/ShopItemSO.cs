using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeSO", menuName = "UpgradeSO")]
public class ShopItemSO : ScriptableObject
{
    [Header("Name&Desc Settings")]
    public string _name;
    public string _desc;

    [Header("Value Settings")]
    public long initAmount;
    public long weightAmount;
    public long priceInitAmount;
    public long priceWeightAmount;

    [Header("Click or Auto")]
    public bool isClick;
}
