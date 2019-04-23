using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SO;


public abstract class BaseSkill : ScriptableObject
{
    public int id;
    public int manaCost;
    public int skillRecovery;
    public string skillName;
    public string skillDescription;
    public Sprite skillIcon;


    public abstract void UseSkill();
    public abstract void ChooseTarget();
}
