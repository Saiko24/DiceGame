using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SO;

public class Shop : MonoBehaviour {

    public List<GameObject> shopCellsArray;
    public IntArrayList addingItemsList;
    public ItemsLibrary itemsLibrary;
    public GameEvent updateInventory;
    public GameEvent callHelpTip;
    public IntVariable helpTipIndex;
    public IntArrayList inentoryItemsId;
    public PlayerCurrentParameter weight;
    public PlayerCurrentParameter gold;
    public GameEvent addItem;

    // Use this for initialization
    void Start () {
        foreach (var item in shopCellsArray)
        {
            item.GetComponent<ItemDisplay>().cellNotEmpty = true;
            item.GetComponent<ItemDisplay>().item = itemsLibrary.ItemList[Random.Range(0, itemsLibrary.ItemList.Count)];
        }
	}

    public void OnEnable()
    {
        foreach (var item in shopCellsArray)
        {
            item.GetComponent<ItemDisplay>().cellNotEmpty = true;
            item.GetComponent<ItemDisplay>().item = itemsLibrary.ItemList[Random.Range(0, itemsLibrary.ItemList.Count)];
        }
    }

    public bool CheckInventoryWeight(ItemDisplay itemWeight)
    {
        if ((weight.ParameterValue + itemWeight.item.weight) <= weight.MaxValue.ParameterValue)
        {
            Debug.Log("Weight check true");
            return true;
        }
        else
        {
            Debug.Log("Weight check false");
            return false;
        }
    }

    public void BuyItem(ItemDisplay item)
    {
        if (CheckInventoryWeight(item))
        {
            if (item.itemsCountInt > 1)
            {
                item.itemsCountInt--;
            }
            else
            {
                item.cellNotEmpty = false;
            }

            gold.ParameterValue -= item.item.price;
            addingItemsList.arrayList.Add(item.item.id);
            addItem.Raise();
        }
        else
        {
            helpTipIndex.value = 4;
            callHelpTip.Raise();
        }
    }
}
