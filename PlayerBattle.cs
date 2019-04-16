using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SO;

public class PlayerBattle : MonoBehaviour {

    public int playerHeightIndex;
    public int playerWidthIndex;
    public List<GameObject> playerDirectionsImages;
    public GameObjectVariable PlayerBattleCell;
    public PlayerCurrentParameter playerMoves;
    public CellGameObjectArray2d battleCellArray;
    public IntArray2d creaturesMapIntArray;
    public string playerDirection;

    public void MovePlayer()
    {
        BattleCell PlayerBattleCellScript = PlayerBattleCell.value.GetComponent<BattleCell>();

        ChooseDirection();

        gameObject.transform.position = PlayerBattleCell.value.transform.position;
        playerHeightIndex = PlayerBattleCellScript.heightIndex;
        playerWidthIndex = PlayerBattleCellScript.widthIndex;
        playerMoves.ParameterValue = 0; 
    }

    public void ChooseDirection()
    {
        BattleCell PlayerBattleCellScript = PlayerBattleCell.value.GetComponent<BattleCell>();

        int tempHeight;
        int tempWidth;

        tempHeight = PlayerBattleCellScript.heightIndex - playerHeightIndex;
        tempWidth = PlayerBattleCellScript.widthIndex - playerWidthIndex;

        if (Mathf.Abs(tempHeight) > Mathf.Abs(tempWidth))
        {
            if (tempHeight < 0)
            {
                RotatePlayerDirection("back");
            }
            else if (tempHeight > 0)
            {
                RotatePlayerDirection("front");
            }
        }
        else if (Mathf.Abs(tempHeight) < Mathf.Abs(tempWidth))
        {
            if (tempWidth < 0)
            {
                RotatePlayerDirection("left");
            }
            else if (tempWidth > 0)
            {
                RotatePlayerDirection("right");
            }
        }
        else
        {
            if (tempHeight < 0 && tempWidth < 0)
            {
                if (playerDirection == "left")
                {
                    RotatePlayerDirection("left");
                }
                else if (playerDirection == "back")
                {
                    RotatePlayerDirection("back");
                }
                else
                {
                    if (Random.Range(0, 2) == 0)
                    {
                        RotatePlayerDirection("left");
                    }
                    else
                    {
                        RotatePlayerDirection("back");
                    }
                }
            }
            else if (tempHeight > 0 && tempWidth > 0)
            {
                if (playerDirection == "right")
                {
                    RotatePlayerDirection("right");
                }
                else if (playerDirection == "front")
                {
                    RotatePlayerDirection("front");
                }
                else
                {
                    if (Random.Range(0, 2) == 0)
                    {
                        RotatePlayerDirection("right");
                    }
                    else
                    {
                        RotatePlayerDirection("front");
                    }
                }
            }
            else if (tempHeight < 0 && tempWidth > 0)
            {
                if (playerDirection == "right")
                {
                    RotatePlayerDirection("right");
                }
                else if (playerDirection == "back")
                {
                    RotatePlayerDirection("back");
                }
                else
                {
                    if (Random.Range(0, 2) == 0)
                    {
                        RotatePlayerDirection("right");
                    }
                    else
                    {
                        RotatePlayerDirection("back");
                    }
                }
            }
            else if (tempHeight > 0 && tempWidth < 0)
            {

                if (playerDirection == "left")
                {
                    RotatePlayerDirection("left");
                }
                else if (playerDirection == "front")
                {
                    RotatePlayerDirection("front");
                }
                else
                {
                    if (Random.Range(0, 2) == 0)
                    {
                        RotatePlayerDirection("left");
                    }
                    else
                    {
                        RotatePlayerDirection("front");
                    }
                }
            }
        }
    }

    public void RotatePlayerDirection(string direction)
    {
        foreach (var item in playerDirectionsImages)
        {
            item.SetActive(false);
        }

        switch (direction)
        {
            case "back":
                playerDirectionsImages[0].SetActive(true);
                playerDirection = "back";
            break;
            case "front":
                playerDirectionsImages[1].SetActive(true);
                playerDirection = "front";
                break;
            case "right":
                playerDirectionsImages[2].SetActive(true);
                playerDirection = "right";
                break;
            case "left":
                playerDirectionsImages[3].SetActive(true);
                playerDirection = "left";
                break;

            default:
                break;
        }
    }
}
