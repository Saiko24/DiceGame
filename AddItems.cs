using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SO;
public class AddItems : MonoBehaviour {

    public IntArrayList inentoryItemsId;
    public IntArrayList addingItemsList;
    public GameEvent updateInventory;

    public void ItemsAdd()
    {
        foreach (var item in addingItemsList.arrayList)
        {
            inentoryItemsId.arrayList.Add(item);
        }

        addingItemsList.arrayList.Clear();

        updateInventory.Raise();
    }
}
