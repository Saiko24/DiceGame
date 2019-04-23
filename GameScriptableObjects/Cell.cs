using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Cell", menuName = "Map/Cell")]
public class Cell : ScriptableObject
{

    public string cellName;

    public string cellDescription;

    public Sprite cellSprite;

    public Sprite cellIcone;

    public bool colliderIsActive;

}
