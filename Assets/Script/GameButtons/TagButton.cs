using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagButton : MonoBehaviour
{
    [Header("Open Panel")]
    public GameObject OpenPanel;

    [Header("Close Panel")]
    public List<GameObject> ClosePanels;

    public void ChangeTab()
    {
        foreach(GameObject panel in ClosePanels)
        {
            panel.SetActive(false);
        }
        if (OpenPanel == null) { return; }
        OpenPanel.SetActive(true);
    }
}
