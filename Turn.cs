using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SO;

public class Turn : MonoBehaviour {

    public List<string> turns;
    public StringVariable turn;
    public CellGameObjectArray2d battleCellArray;
    public GameEvent playerTurn;
    public GameEvent enemiesTurn;
    public GameEvent surviorsTurn;
    public TacticsMoves TacticsMoves;
    public EnemiesArray enemiesArray;
    public bool check;
	

    public void NextTurn()
    {
        check = true;

        if (turn.value == "")
        {
            turn.value = turns[0];
        }
        else
        {
            for (int i = 0; i < turns.Count; i++)
            {

                if (turns[i] == turn.value)
                {
                    if (i != turns.Count - 1)
                    {
                        turn.value = turns[i + 1];
                        i = turns.Count;
                    }
                    else
                    {
                        turn.value = turns[0];
                    }
                }

            }
        }
        TacticsMoves.ChangeTurn();
    }

    public void ResetSelectableCells()
    {
        foreach (GameObject tile in battleCellArray.cellGameObjectArray)
        {
            BattleCell t = tile.GetComponent<BattleCell>();
            t.ResetSelect();
        }
    }
}
