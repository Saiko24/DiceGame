using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Lean.Touch;
using SO;

public class CellDisplay : MonoBehaviour {

    public CellLibrary cellLibrary;
    public CellGameObjectArray2d cellGameObjectArray2d;
    public Animator animator;
    public Text cellNameText;
    public Text cellDescriptionText;
    public Image cellSpriteImage;
    public Image cellIconImage;
    public bool colliderIsActive;
    public int heightPos;
    public int widthPos;
    public string cellType;
    public BoxCollider2D cellCollider;
    public bool cellIsActive;
    int index;

    public void DisplayCellInfo(string type, int height, int width)
    {
        cellType = type;
        heightPos = height;
        widthPos = width;

        cellCollider.enabled = false;

        switch (cellType)
        {
            case "Empty":
                index = 0;
                break;
            case "Start":
                index = 1;
                break;
            case "End":
                index = 2;
                break;
            case "Disabled":
                index = 3;
                break;
            case "Standart":
                index = 4;
                break;
            case "Event":
                index = 5;
                break;
            case "Camp":
                index = 6;
                break;
            case "Gambling":
                index = 7;
                break;
            case "Library":
                index = 8;
                break;
            case "Quest":
                index = 9;
                break;
            case "Secret":
                index = 10;
                break;
            case "Shop":
                index = 11;
                break;
            case "Smithy":
                index = 12;
                break;
            case "Tavern":
                index = 13;
                break;
            case "Trap":
                index = 14;
                break;
            case "Treasure":
                index = 15;
                break;
        }

        cellSpriteImage.sprite = cellLibrary.cellList[index].cellSprite;
        cellSpriteImage.color = new Color32(255, 255, 255, 255);

        cellIconImage.sprite = cellLibrary.cellList[index].cellIcone;
        cellIconImage.color = new Color32(255, 255, 255, 255);

        cellCollider.enabled = true;

        if (cellType == "Empty")
        {
            cellSpriteImage.color = new Color32(255, 255, 255, 0);
            cellIconImage.color = new Color32(0, 0, 0, 0);
            cellCollider.enabled = false;
        }

        animator.Play("CellShowAnimation");
    }
}
