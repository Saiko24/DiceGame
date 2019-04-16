using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Lean.Touch;
using SO;

public class CellEvent : MonoBehaviour {

    public CellGameEventLibrary cellGameEventList;
    public CellGameObjectArray2d cellGameObjectArray;
    public PlayerCurrentParameter moves;
    public PlayerCurrentParameter food;
    public PlayerCurrentParameter health;
    public IntVariable mapHeight;
    public IntVariable mapWidth;
    public BoolVariable generateDone;
    public Vector2Variable playerPosition;
    public IntArray2d mapIntArray;
    public GameEvent callHelpTip;
    public IntVariable helpTipIndex;
    public BoxCollider2D cellCollider;
    public Animator animator;
    public LayerMask LayerMask;
    public bool playerOnCell;
    public BoolVariable blockRaycast;
    public CellDisplay CellDisplay;
    public CellDisplay[,] CellDisplayList;
    public bool check;

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

        playerOnCell = false;

        if (hit != null)
        {
            if (blockRaycast.value == false)
            {
                if (hit.gameObject.tag == "Cell" && CellDisplay.cellIsActive)
                {
                    if (hit.gameObject == this.gameObject)
                    {
                        playerOnCell = true;

                        playerPosition.value.x = CellDisplay.heightPos;
                        playerPosition.value.y = CellDisplay.widthPos;

                        CheckAvailableCells();

                        moves.ParameterValue -= 1;

                        if (moves.ParameterValue == 0)
                        {
                            if (food.ParameterValue == 0)
                            {
                                health.ParameterValue -= 3;
                            }
                            else
                            {
                                food.ParameterValue -= 1;

                                if (food.ParameterValue == 0)
                                {
                                    helpTipIndex.value = 0;
                                    callHelpTip.Raise();
                                }
                            }

                            string cellType = hit.gameObject.GetComponent<CellDisplay>().cellType;

                            if (cellType == "Event")
                            {
                                cellGameEventList.CellGameEventsList[0].Raise();
                            }
                            else if (cellType == "Standart")
                            {
                                cellGameEventList.CellGameEventsList[9].Raise();
                            }
                            else if (cellType == "Shop")
                            {
                                cellGameEventList.CellGameEventsList[1].Raise();
                            }
                        }
                        else
                        {

                        }
                    }
                }
            }
        }
        else
        {
        }
    }

    public void CheckAvailableCells()
    {
        for (int i = 0; i < mapHeight.value; i++)
        {
            for (int y = 0; y < mapWidth.value; y++)
            {
                CellDisplayList[i, y] = cellGameObjectArray.cellGameObjectArray[i, y].GetComponent<CellDisplay>();
                CellDisplayList[i, y].cellIsActive = false;
                CellDisplayList[i, y].animator.Play("New State");
            }
        }

        int playerX = Mathf.RoundToInt(playerPosition.value.x);
        int playerY = Mathf.RoundToInt(playerPosition.value.y);

        if (playerX != 0)
        {
            if (mapIntArray.array[playerX - 1, playerY] != 0)
            {
                CellDisplayList[playerX - 1, playerY].cellIsActive = true;
                CellDisplayList[playerX - 1, playerY].animator.Play("CellActiveAnimation");
            }
        }

        if (playerX != mapHeight.value - 1)
        {
            if (mapIntArray.array[playerX + 1, playerY] != 0)
            {
                CellDisplayList[playerX + 1, playerY].cellIsActive = true;
                CellDisplayList[playerX + 1, playerY].animator.Play("CellActiveAnimation");
            }
        }

        if (playerY != 0)
        {
            if (mapIntArray.array[playerX, playerY - 1] != 0)
            {
                CellDisplayList[playerX, playerY - 1].cellIsActive = true;
                CellDisplayList[playerX, playerY - 1].animator.Play("CellActiveAnimation");
            }
        }

        if (playerY != mapWidth.value - 1)
        {
            if (mapIntArray.array[playerX, playerY + 1] != 0)
            {
                CellDisplayList[playerX, playerY + 1].cellIsActive = true;
                CellDisplayList[playerX, playerY + 1].animator.Play("CellActiveAnimation");
            }
        }
        CellDisplayList[playerX, playerY].animator.Play("CellPlayerAnimation");
    }

    // Use this for initialization
    void Start () {

        CellDisplay = this.gameObject.GetComponent<CellDisplay>();

        CellDisplayList = new CellDisplay[mapHeight.value, mapWidth.value];

        check = true;
    }


    // Update is called once per frame
    void Update()
    {
        if (generateDone.value)
        {
            if (check)
            {
                if (CellDisplay.heightPos == playerPosition.value.x && CellDisplay.widthPos == playerPosition.value.y)
                {
                    CellDisplay.cellIsActive = false;
                    CheckAvailableCells();
                    animator.Play("CellPlayerAnimation");
                }
                check = false;
            }
        }
        else
        {
            check = true;
        }
    }
}
