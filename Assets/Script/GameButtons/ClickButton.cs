using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickButton : MonoBehaviour
{
    public void Click()
    {
        GameManager.Instance.EarnMoney(GameManager.Instance.clickPower);
    }
}
