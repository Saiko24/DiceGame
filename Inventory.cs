using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SO;

public class Inventory : MonoBehaviour {

    public List<GameObject> inventoryCells;
    public List<GameObject> inventoryCellsLists;
    public int inventoryListsCount;
    public int inventoryCurrentList;
    public IntArrayList inentoryItemsId;
    public ItemsLibrary itemsLibrary;
    public IntArrayList addingItemsList;
    public GameObject inventoryCellsContainer;
    public GameObject inventoryCellsList;
    public GameObject inventoryCell;
    public Text inventoryListsCountText;
    public Button leftArrow;
    public Button rightArrow;
    public PlayerCurrentParameter Weight;

    // Use this for initialization
    void Awake () {

        UpdateInventory();
    }

    private void OnEnable()
    {
        UpdateInventory();

        InventoryNavigateUpdate();
    }

    // Update is called once per frame
    void Update () {
        inventoryListsCountText.text = string.Format("{0}/{1}", inventoryCurrentList, inventoryListsCount);
    }

    public void InventoryNavigateUpdate()
    {
        inventoryListsCount = inventoryCellsLists.Count;

        inventoryCurrentList = 1;

        foreach (var item in inventoryCellsLists)
        {
            item.SetActive(false);
        }

        inventoryCellsLists[inventoryCurrentList - 1].SetActive(true);

        leftArrow.interactable = true;
        rightArrow.interactable = true;

        if (inventoryCurrentList == 1)
        {
            leftArrow.interactable = false;
        }

        if (inventoryCurrentList == inventoryCellsLists.Count)
        {
            rightArrow.interactable = false;
        }
    }

    public void UpdateInventory()
    {
        for (int i = 0; i < inventoryCells.Count; i++)
        {
            inventoryCells[i].GetComponent<ItemDisplay>().cellNotEmpty = false;
            inventoryCells[i].GetComponent<ItemDisplay>().indexOfInventoryItem = 0;
            inventoryCells[i].GetComponent<ItemDisplay>().item = null;
            Weight.ParameterValue = 0;
        }

        for (int i = 0; i < inentoryItemsId.arrayList.Count; i++)
        {
            for (int u = 0; u < itemsLibrary.ItemList.Count; u++)
            {
                if (inentoryItemsId.arrayList[i] == itemsLibrary.ItemList[u].id)
                {
                    bool sameItemFound = false;

                    if (itemsLibrary.ItemList[u].isStackable == true)
                    {
                        for (int y = 0; y < inventoryCells.Count; y++)
                        {
                            if (inventoryCells[y].GetComponent<ItemDisplay>().cellNotEmpty && sameItemFound == false)
                            {
                                if (inventoryCells[y].GetComponent<ItemDisplay>().item.id == itemsLibrary.ItemList[u].id && 
                                    inventoryCells[y].GetComponent<ItemDisplay>().itemsCountInt< itemsLibrary.ItemList[u].itemsMaxCount)
                                {
                                    inventoryCells[y].GetComponent<ItemDisplay>().itemsCountInt++;
                                    Weight.ParameterValue += itemsLibrary.ItemList[u].weight;
                                    sameItemFound = true;
                                }
                            }
                        }
                    }

                    if (!sameItemFound)
                    {
                        for (int y = 0; y < inventoryCells.Count; y++)
                        {
                            if (inventoryCells[y].GetComponent<ItemDisplay>().cellNotEmpty == false)
                            {
                                inventoryCells[y].GetComponent<ItemDisplay>().cellNotEmpty = true;
                                inventoryCells[y].GetComponent<ItemDisplay>().indexOfInventoryItem = i;
                                inventoryCells[y].GetComponent<ItemDisplay>().itemsCountInt = 1;
                                inventoryCells[y].GetComponent<ItemDisplay>().item = itemsLibrary.ItemList[u];
                                Weight.ParameterValue += itemsLibrary.ItemList[u].weight;

                                y = inventoryCells.Count;
                            }

                            if (y == inventoryCells.Count-1)
                            {
                                AddNewInventoryList();
                            }
                        }
                    }
                }
            }
        }
    }

    public void LeftArrow()
    {
        inventoryCurrentList--;

        foreach (var item in inventoryCellsLists)
        {
            item.SetActive(false);
        }

        inventoryCellsLists[inventoryCurrentList-1].SetActive(true);

        if (inventoryCurrentList == 1)
        {
            leftArrow.interactable = false;
        }

        rightArrow.interactable = true;
    }

    public void RightArrow()
    {
        inventoryCurrentList++;

        foreach (var item in inventoryCellsLists)
        {
            item.SetActive(false);
        }

        inventoryCellsLists[inventoryCurrentList-1].SetActive(true);

        if (inventoryCurrentList == inventoryCellsLists.Count)
        {
            rightArrow.interactable = false;
        }

        leftArrow.interactable = true;
    }

    public void AddNewInventoryList()
    {
        GameObject cellList = Instantiate(inventoryCellsList) as GameObject;
        cellList.transform.SetParent(inventoryCellsContainer.transform, false);
        inventoryCellsLists.Add(cellList);

        for (int i = 0; i < 30; i++)
        {
            GameObject cell = Instantiate(inventoryCell) as GameObject;
            cell.transform.SetParent(cellList.transform, false);
            inventoryCells.Add(cell);
        }
    }
}