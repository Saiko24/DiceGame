using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New current stat", menuName = "Stats/Current Stat")]
public class Stats : ScriptableObject
{
    public int currentStatValue;

    public string currentStatName;

    public string currentStatDescription;

    public Sprite currentStatSprite;
}
