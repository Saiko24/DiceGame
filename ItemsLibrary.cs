using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Items Library", menuName = "Items/ Items Library")]
public class ItemsLibrary : ScriptableObject
{

    public List<BaseItem> ItemList;
}
