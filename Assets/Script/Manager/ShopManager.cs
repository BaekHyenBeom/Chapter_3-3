using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface AutoClicker
{
    public void Purchase();
    public void Upgrade();
    public void CheckValue();
}

public class ShopManager : Singleton<ShopManager>
{
}
