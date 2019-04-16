using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Lean.Touch;
using SO;

public class BattleCell : MonoBehaviour {

    public BattleCellLibrary battleCellLibrary;
    public Image cellSpriteImage;
    public Image cellIconImage;
    public BoxCollider2D cellCollider;
    public string battleCellType;
    public int heightIndex;
    public int widthIndex;
    public bool walkable = true;
    public bool selectable = false;
    public bool visited = false;
    public bool playerOnCell = false;
    public BattleCell parent = null;
    public int distance = 0;
    public CellGameObjectArray2d battleCellArray;
    public IntVariable battleMapHeight;
    public IntVariable battleMapWidth;
    public IntArray2d creaturesMapIntArray;
    public List<BattleCell> neighborCellsList;
    public LayerMask LayerMask;
    public BoolVariable blockRaycast;
    public GameEvent clearSelectableCells;
    public GameObjectVariable PlayerBattleCell;
    public GameEvent movePlayer;
    int index;


    protected virtual void OnEnable()
    {

        LeanTouch.OnFingerTap += OnFingerTap;

    }

    protected virtual void OnDisable()
    {
        LeanTouch.OnFingerTap -= OnFingerTap;

    }

	void Update () {
        if (walkable)
        {
            if (selectable)
            {
                cellSpriteImage.color = Color.red;
            }
            else if (battleCellType == "Obstacle")
            {
                cellSpriteImage.color = Color.black;
            }
            else
            {
                cellSpriteImage.color = Color.white;
            }
        }
    }

    public void OnFingerTap(LeanFinger finger)
    {
        var ray = finger.GetWorldPosition(1.0f);
        var hit = Physics2D.OverlapPoint(ray, LayerMask);

        if (hit != null)
        {
            if (blockRaycast.value == false)
            {
                if (hit.gameObject.tag == "BattleCell" && selectable == true)
                {
                    if (hit.gameObject == this.gameObject)
                    {
                        PlayerMoveOnCell();
                    }
                }
            }
        }
        else
        {
        }
    }

    public void FindNeighbors()
    {
        if (heightIndex != 0)
        {
            if (battleCellArray.cellGameObjectArray[heightIndex - 1, widthIndex].GetComponent<BattleCell>().walkable == true)
            {
            neighborCellsList.Add(battleCellArray.cellGameObjectArray[heightIndex - 1, widthIndex].GetComponent<BattleCell>());
            }
        }
        if (heightIndex != battleMapHeight.value-1)
        {   
            if (battleCellArray.cellGameObjectArray[heightIndex + 1, widthIndex].GetComponent<BattleCell>().walkable == true)
            {
                neighborCellsList.Add(battleCellArray.cellGameObjectArray[heightIndex + 1, widthIndex].GetComponent<BattleCell>());
            }
        }
        if (widthIndex != 0)
        {
            if (battleCellArray.cellGameObjectArray[heightIndex, widthIndex - 1].GetComponent<BattleCell>().walkable == true)
            {
                neighborCellsList.Add(battleCellArray.cellGameObjectArray[heightIndex, widthIndex - 1].GetComponent<BattleCell>());
            }
        }
        if (widthIndex != battleMapWidth.value-1)
        {
            if (battleCellArray.cellGameObjectArray[heightIndex, widthIndex + 1].GetComponent<BattleCell>().walkable == true)
            {
                neighborCellsList.Add(battleCellArray.cellGameObjectArray[heightIndex, widthIndex + 1].GetComponent<BattleCell>());
            }
        }
    }

    public void ResetSelect()
    { 
        neighborCellsList.Clear();
        selectable = false;
        visited = false;
        parent = null;
        distance = 0;
    }

    public void PlayerMoveOnCell()
    {
            for (int i = 0; i < battleMapHeight.value; i++)
            {
                for (int y = 0; y < battleMapWidth.value; y++)
                {
                    creaturesMapIntArray.array[i, y] = 0;
                    battleCellArray.cellGameObjectArray[i, y].GetComponent<BattleCell>().playerOnCell = false;
                }
            }

            creaturesMapIntArray.array[heightIndex, widthIndex] = 1;

            playerOnCell = true;
            PlayerBattleCell.value = this.gameObject;
            movePlayer.Raise();

            clearSelectableCells.Raise();
    }


    public void DisplayBattleCellInfo(string type, int height, int width)
    {
        battleCellType = type;
        heightIndex = height;
        widthIndex = width;

        cellSpriteImage.color = new Color32(255, 255, 255, 255);
        cellIconImage.color = new Color32(255, 255, 255, 0);

        switch (battleCellType)
        {
            case "Empty":
                index = 0;
                cellCollider.enabled = false;
                walkable = false;
                break;
            case "DefaultFloor":
                index = 1;
                cellCollider.enabled = true;
                walkable = true;
                break;
            case "ObstacleWall":
                index = 2;
                cellCollider.enabled = false;
                walkable = false;
                cellSpriteImage.color = new Color32(125, 75, 0, 255);
                break;
        }

        cellSpriteImage.sprite = battleCellLibrary.cellList[index].cellSprite;

        cellIconImage.sprite = battleCellLibrary.cellList[index].cellIcone;


        if (battleCellType == "Empty")
        {
            cellSpriteImage.color = new Color32(255, 255, 255, 0);
            cellIconImage.color = new Color32(0, 0, 0, 0);
            cellCollider.enabled = false;
        }
    }
}
