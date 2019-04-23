using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Lean.Touch;
using SO;

public class ItemDisplay : MonoBehaviour {

    public BaseItem item;
    public BaseSkill skill;
    public int indexOfInventoryItem;
    public int itemsCountInt;
    public bool cellNotEmpty;
    public Image itemImage;
    public Image rarityImage;
    public GameObject itemsCount;
    public Text itemsCountText;
    public GameObject cellItem;
    public GameObject price;
    public Text itemPrice;
    public Text skillManacost;
    public IntArrayList inventoryItemsId;
    public GameEvent updateInventory;
    public bool itemEventVisible;
    public GameObject itemEvent;
    public Button buyButton;
    public BoxCollider2D itemCollider;
    public BoolVariable blockRaycast;
    public LayerMask LayerMask;
    public PlayerCurrentParameter gold;

    protected virtual void OnEnable()
    {

        LeanTouch.OnFingerTap += OnFingerTap;

    }

    protected virtual void OnDisable()
    {
        LeanTouch.OnFingerTap -= OnFingerTap;

    }

        public void OnFingerTap(LeanFinger finger)
    {

        var ray = finger.GetWorldPosition(1.0f);
        var hit = Physics2D.OverlapPoint(ray, LayerMask);

        if (hit != null)
        {
            if (hit.gameObject.tag == "InventoryCell" || hit.gameObject.tag == "SkillCell")
            {

                if (cellNotEmpty)
                {
                    if (hit.gameObject == this.gameObject)
                    {
                        if (this.itemEventVisible == false)
                        {
                            itemEventVisible = true;
                            itemEvent.SetActive(true);
                        }
                        else
                        {
                            itemEventVisible = false;
                            itemEvent.SetActive(false);
                        }
                    }
                    else
                    {
                        itemEventVisible = false;
                        itemEvent.SetActive(false);
                    }
                }
            }
        }
        else
        {
            if (gameObject.tag != "DailyRewardCell" && gameObject.tag != "ShopCell")
            {

                itemEvent.SetActive(false);
                itemEventVisible = false;
            }
        }
    }

    // Use this for initialization
    void Start () {

        if (skill != null || item !=null)
        {
            cellNotEmpty = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (cellNotEmpty)
        {
            if (gameObject.tag != "SkillCell")
            {
                itemCollider.enabled = true;
                cellItem.SetActive(true);

                itemImage.sprite = item.itemIcon;

                switch (item.rarity)
                {
                    case BaseItem.itemRarity.neutral:
                        rarityImage.color = new Color32(255, 255, 255, 0);
                        break;

                    case BaseItem.itemRarity.common:
                        rarityImage.color = new Color32(255, 255, 255, 255);
                        break;

                    case BaseItem.itemRarity.uncommon:
                        rarityImage.color = new Color32(0, 255, 0, 255);
                        break;

                    case BaseItem.itemRarity.rare:
                        rarityImage.color = new Color32(0, 0, 255, 255);
                        break;

                    case BaseItem.itemRarity.epic:
                        rarityImage.color = new Color32(255, 0, 255, 255);
                        break;

                    case BaseItem.itemRarity.legendary:
                        rarityImage.color = new Color32(255, 125, 0, 255);
                        break;

                }

                if (itemPrice != null)
                {
                    price.SetActive(true);
                    buyButton.gameObject.SetActive(true);

                    itemPrice.text = item.price.ToString();

                    if ((gold.ParameterValue - item.price) >= 0)
                    {
                        itemPrice.color = new Color32(255, 255, 255, 255);
                        buyButton.GetComponentInChildren<Text>().color = new Color32(255, 255, 255, 255);
                        buyButton.interactable = true;
                    }
                    else
                    {
                        itemPrice.color = new Color32(255, 0, 0, 255);
                        buyButton.GetComponentInChildren<Text>().color = new Color32(125, 125, 125, 125);
                        buyButton.interactable = false;
                    }
                }

                if (item.isStackable)
                {
                    if (itemsCountInt > 1)
                    {
                        itemsCount.SetActive(true);
                    }
                    else if (gameObject.tag == "DailyRewardCell")
                    {
                        itemsCount.SetActive(true);
                        itemsCountInt = item.itemsMaxCount;
                    }
                    else
                    {
                        itemsCount.SetActive(false);
                    }

                    string s = itemsCountInt.ToString();

                    if (item.itemsMaxCount >= 1000000)
                    {
                        s = s.Remove(s.Length - 6, 6);
                        itemsCountText.text = s + "m";
                    }
                    else if (item.itemsMaxCount >= 10000)
                    {
                        s = s.Remove(s.Length - 3, 3);
                        itemsCountText.text = s + "k";
                    }
                    else
                    {
                        itemsCountText.text = s;
                    }
                }
                else
                {
                    itemsCountInt = 1;
                    itemsCount.SetActive(false);
                }
            }
            else
            {
                itemCollider.enabled = true;
                cellItem.SetActive(true);
                itemImage.sprite = skill.skillIcon;
                skillManacost.text = skill.manaCost.ToString();
            }
        }
        else
        {
            itemCollider.enabled = false;
            cellItem.SetActive(false);

            if (gameObject.tag == "ShopCell")
            {
                buyButton.gameObject.SetActive(false);
                price.SetActive(false);
            }

        }

    }

    public void UpdateIconDisplay ()
    {



    }

    //public void BuyItem()
    //{
    //    Debug.Log("BUY");
    //    //Сделать проверку на достаточность золота и веса инвентаря
    //    inventoryItemsId.arrayList.Add(item.id);
    //    item.UseItem();
    //    DeleteItem();
    //}

    public void UseItem()
    {
        Debug.Log("USE");
        if (item != null)
        {
            item.UseItem();
            DeleteItem();
        }

        if (skill != null)
        {
            skill.ChooseTarget();
        }
    }

    public void UseSkill()
    {
        skill.UseSkill();
    }

    public void DeleteItem()
    {
        Debug.Log("DELETE");
        cellNotEmpty = false;

        if (item.isStackable)
        {
            int temp = 0;

            for (int i = 0; i < inventoryItemsId.arrayList.Count; i++)
            {
                if (inventoryItemsId.arrayList[i] == item.id)
                {
                    temp = i;
                }
            }

            inventoryItemsId.arrayList.RemoveAt(temp);
        }
        else
        {
            inventoryItemsId.arrayList.RemoveAt(indexOfInventoryItem);
        }

        updateInventory.Raise();
    }
}
