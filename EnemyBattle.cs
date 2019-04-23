using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SO;

public class EnemyBattle : MonoBehaviour
{
    public int enemyHeightIndex;
    public int enemyWidthIndex;
    public int enemyMoves;
    public int enemyDamage;
    public int enemyHealth;
    public CellGameObjectArray2d battleCellArray;
    public PlayerCurrentParameter playerHealth;

     void Start()
    {
        enemyDamage = 15;
        enemyHealth = 30;
    }

    public void EnemyMoves(BattleCell cell)
    {
        battleCellArray.cellGameObjectArray[enemyHeightIndex, enemyWidthIndex].GetComponent<BattleCell>().enemyOnCell = false;
        battleCellArray.cellGameObjectArray[enemyHeightIndex, enemyWidthIndex].GetComponent<BattleCell>().walkable = true;
        enemyHeightIndex = cell.heightIndex;
        enemyWidthIndex = cell.widthIndex;
        gameObject.transform.position = new Vector3(cell.transform.position.x, cell.transform.position.y, -3);
        cell.enemyOnCell = true;
        cell.walkable = false;
    }

    public void EnemyAttack()
    {
        if (enemyHeightIndex != 12)
        {
            if (battleCellArray.cellGameObjectArray[enemyHeightIndex + 1, enemyWidthIndex].GetComponent<BattleCell>().playerOnCell == true)
            {
                Debug.Log("Enemy attack!");
                playerHealth.currentParameterValue -= enemyDamage;
            }
        }

        if (enemyHeightIndex != 0)
        {
            if (battleCellArray.cellGameObjectArray[enemyHeightIndex - 1, enemyWidthIndex].GetComponent<BattleCell>().playerOnCell == true)
            {
                Debug.Log("Enemy attack!");
                playerHealth.currentParameterValue -= enemyDamage;
            }
        }

        if (enemyWidthIndex != 6)
        {
            if (battleCellArray.cellGameObjectArray[enemyHeightIndex, enemyWidthIndex + 1].GetComponent<BattleCell>().playerOnCell == true)
            {
                Debug.Log("Enemy attack!");
                playerHealth.currentParameterValue -= enemyDamage;
            }
        }

        if (enemyWidthIndex != 0)
        {
            if (battleCellArray.cellGameObjectArray[enemyHeightIndex, enemyWidthIndex - 1].GetComponent<BattleCell>().playerOnCell == true)
            {
                Debug.Log("Enemy attack!");
                playerHealth.currentParameterValue -= enemyDamage;
            }
        }
    }
}
