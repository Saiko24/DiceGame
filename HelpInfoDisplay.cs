using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SO;

public class HelpInfoDisplay : MonoBehaviour {

    public GameObjectVariable itemForHelpPanel;
    public List<GameObject> helpPanels;
    public Camera cam;

    public void PositionHelpInfo()
    {
        Vector3 screenPos = cam.WorldToScreenPoint(itemForHelpPanel.value.transform.position);

        int screenHeightHalf = Screen.height / 2;

        Vector2 top = new Vector2(0, 0);
        top.y = -screenHeightHalf/2;

        Vector2 bottom = new Vector2(0, 0);
        bottom.y = screenHeightHalf / 2;

        if (itemForHelpPanel.value.tag == "InventoryCell" || itemForHelpPanel.value.tag == "ShopCell")
        {
            helpPanels[0].GetComponent<HelpItemDisplay>().item = itemForHelpPanel.value.GetComponent<ItemDisplay>().item;
            if (screenPos.y > screenHeightHalf)
            {
                helpPanels[0].transform.localPosition = top;
            }
            else
            {
                helpPanels[0].transform.localPosition = bottom;
            }

            helpPanels[0].SetActive(true);
        }
    }
}
