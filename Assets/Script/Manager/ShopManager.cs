using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : Singleton<ShopManager>
{
    [Header("ItemSO List")]
    public List<ShopItemSO> shopItems;
    public GameObject prefabs;
    public GameObject parent;

    void Start()
    {
        foreach (ShopItemSO item in shopItems)
        {
            GameObject obj = Instantiate(prefabs, parent.transform);
            if (obj.TryGetComponent<AutoClicker>(out AutoClicker component))
            {
                component.itemSO = item;
            }
        }
    }
}
