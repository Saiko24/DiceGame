using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SO;

public class TacticsMoves : MonoBehaviour {

    public IntVariable battleMapHeight;
    public IntVariable battleMapWidth;
    public CellGameObjectArray2d battleCellArray;
    public IntArray2d battleMapIntArray;
    public IntArray2d effectsMapIntArray;
    public IntArray2d creaturesMapIntArray;
    public GameObjectVariable PlayerBattleCell;
    public BattleCell playerCell;
    public List<BattleCell> selectableTiles;
    public PlayerCurrentParameter playerMoves;

    public void FindPlayerCell()
    {
         playerCell = PlayerBattleCell.value.GetComponent<BattleCell>();
    }

    public void FindBattleCellsNeighbors()
    {
        foreach (GameObject tile in battleCellArray.cellGameObjectArray)
        {
            BattleCell t = tile.GetComponent<BattleCell>();
            t.FindNeighbors();
        }
    }

    public void ResetSelectableCells()
    {
        foreach (GameObject tile in battleCellArray.cellGameObjectArray)
        {
            BattleCell t = tile.GetComponent<BattleCell>();
            t.ResetSelect();
        }
    }

    public void FindSelectableCells()
    {
        ResetSelectableCells();
        FindBattleCellsNeighbors();
        FindPlayerCell();

        Queue<BattleCell> process = new Queue<BattleCell>();

        process.Enqueue(playerCell);
        PlayerBattleCell.value.GetComponent<BattleCell>().visited = true;

        while (process.Count > 0)
        {
            BattleCell t = process.Dequeue();

            selectableTiles.Add(t);
            t.selectable = true;

            if (t.distance < playerMoves.ParameterValue)
            {
                foreach (BattleCell cell in t.neighborCellsList)
                {
                    if (!cell.visited)
                    {
                        cell.parent = t;
                        cell.visited = true;
                        cell.distance = 1 + t.distance;
                        process.Enqueue(cell);
                    }
                }
            }
        }
    }
 }
