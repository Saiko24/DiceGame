using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SO;

public class HelpItemDisplay : MonoBehaviour {

    public BaseItem item;
    public Image itemImage;
    public Image rarityImage;
    public Text itemName;
    public Text itemWeight;
    public Text itemPrice;
    public Text itemDescription;

    // Update is called once per frame
    void Update() {
        if (item != null)
        {
            itemImage.sprite = item.itemIcon;
            itemName.text = item.itemName;
            itemWeight.text = string.Format("Вес: {0}", item.weight);
            itemPrice.text = string.Format("Цена: {0}", item.price);
            itemDescription.text = string.Format("Описание: {0}", item.itemDescription);

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
                default:
                    break;
            }
        }
    }
}
