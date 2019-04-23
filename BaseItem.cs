using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SO;


public abstract class BaseItem : ScriptableObject
{
    public int id;
    public float weight;
    public enum itemRarity { neutral, common, uncommon, rare, epic, legendary }
    public itemRarity rarity;
    public int price;
    public int itemsMaxCount = 1;
    public bool questItem;
    public bool useInBattle;
    public bool isStackable;
    public string itemName;
    public string itemDescription;
    public Sprite itemIcon;


    public abstract void UseItem();
}
