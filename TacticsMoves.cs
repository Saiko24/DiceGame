using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SO;

public class TacticsMoves : MonoBehaviour {

    public StringVariable turn;
    public IntVariable battleMapHeight;
    public IntVariable battleMapWidth;
    public CellGameObjectArray2d battleCellArray;
    public IntArray2d battleMapIntArray;
    public IntArray2d effectsMapIntArray;
    public IntArray2d creaturesMapIntArray;
    public GameObjectVariable PlayerBattleCell;
    public BattleCell playerCell;
    public BattleCell currentCell;
    public EnemiesArray EnemiesArray;
    public PlayerCurrentParameter playerMoves;
    public StringVariable battlePhase;
    public GameEvent blockRollDice;

    // Use this for initialization
    void Start () {
        turn.value = "player";
        battlePhase.value = "move";
    }

    public void ChangeTurn()
    {
        FindSelectableCells();
    }

    public void ResetSelectableCells()
    {
        foreach (GameObject tile in battleCellArray.cellGameObjectArray)
        {
            BattleCell t = tile.GetComponent<BattleCell>();
            t.ResetSelect();
        }
    }

    public void FindBattleCellsNeighbors()
    {
        foreach (GameObject tile in battleCellArray.cellGameObjectArray)
        {
            BattleCell t = tile.GetComponent<BattleCell>();
            t.FindNeighbors();
        }
    }

    public void FindPlayerCell()
    {
         playerCell = PlayerBattleCell.value.GetComponent<BattleCell>();
    }

    public void GetCurrentCell(int i)
    {
        foreach (var item in battleCellArray.cellGameObjectArray)
        {
            if (item.GetComponent<BattleCell>().heightIndex == EnemiesArray.array[i].enemyHeightIndex && item.GetComponent<BattleCell>().widthIndex == EnemiesArray.array[i].enemyWidthIndex)
            {
                currentCell = item.GetComponent<BattleCell>();
            }
        }
    }

    public void FindSelectableCells()
    {
        if (turn.value == "player")
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

                if (!t.playerOnCell)
                {
                    t.selectable = true;
                }

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
        else if (turn.value == "enemies")
        {
            for (int i = 0; i < EnemiesArray.array.Count; i++)
            {

                ResetSelectableCells();
                FindBattleCellsNeighbors();
                FindPlayerCell();
                GetCurrentCell(i);

                Queue<BattleCell> process = new Queue<BattleCell>();

                process.Enqueue(currentCell);
                currentCell.GetComponent<BattleCell>().visited = true;

                while (process.Count > 0)
                {
                    BattleCell t = process.Dequeue();

                    foreach (BattleCell cell in t.neighborCellsList)
                    {
                        if (!cell.visited && !cell.enemyOnCell)
                        {
                            cell.parent = t;
                            cell.visited = true;
                            cell.distance = 1 + t.distance;
                            process.Enqueue(cell);
                        }
                    }
                }

                List<BattleCell> cellsAroundPlayer = new List<BattleCell>();

                if (playerCell.heightIndex != 12)
                {
                    if (battleCellArray.cellGameObjectArray[playerCell.heightIndex + 1, playerCell.widthIndex].GetComponent<BattleCell>().walkable == true)
                    {
                        cellsAroundPlayer.Add(battleCellArray.cellGameObjectArray[playerCell.heightIndex + 1, playerCell.widthIndex].GetComponent<BattleCell>());
                    }
                }

                if (playerCell.heightIndex != 0)
                {
                    if (battleCellArray.cellGameObjectArray[playerCell.heightIndex - 1, playerCell.widthIndex].GetComponent<BattleCell>().walkable == true)
                    {
                        cellsAroundPlayer.Add(battleCellArray.cellGameObjectArray[playerCell.heightIndex - 1, playerCell.widthIndex].GetComponent<BattleCell>());
                    }
                }

                if (playerCell.widthIndex != 6)
                {
                    if (battleCellArray.cellGameObjectArray[playerCell.heightIndex, playerCell.widthIndex + 1].GetComponent<BattleCell>().walkable == true)
                    {
                        cellsAroundPlayer.Add(battleCellArray.cellGameObjectArray[playerCell.heightIndex, playerCell.widthIndex + 1].GetComponent<BattleCell>());
                    }
                }

                if (playerCell.widthIndex != 0)
                {
                    if (battleCellArray.cellGameObjectArray[playerCell.heightIndex, playerCell.widthIndex - 1].GetComponent<BattleCell>().walkable == true)
                    {
                        cellsAroundPlayer.Add(battleCellArray.cellGameObjectArray[playerCell.heightIndex, playerCell.widthIndex - 1].GetComponent<BattleCell>());
                    }
                }


                if (cellsAroundPlayer.Count > 0)
                {
                    int tempMin = cellsAroundPlayer[0].distance;
                    BattleCell minDistanceCell = cellsAroundPlayer[0];

                    for (int x = 0; x < cellsAroundPlayer.Count; x++)
                    {
                        if (tempMin > cellsAroundPlayer[x].distance)
                        {
                            tempMin = cellsAroundPlayer[x].distance;
                            minDistanceCell = cellsAroundPlayer[x];
                        }
                    }

                    bool farCellFound = false;
                    BattleCell parentCell = minDistanceCell;

                    while (!farCellFound)
                    {
                        if (parentCell.distance > EnemiesArray.array[0].enemyMoves)
                        {
                            parentCell = parentCell.parent;
                        }
                        else
                        {
                            farCellFound = true;
                        }
                    }

                    EnemiesArray.array[i].EnemyMoves(parentCell);
                }

                EnemiesArray.array[i].EnemyAttack();
            }

            battlePhase.value = "move";
            blockRollDice.Raise();
        }
    }
 }
