using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SO;

public class FillBattleMap : MonoBehaviour {

    public GameObject cell;
    public GameObject creaturesContainer;
    public IntVariable battleMapHeight;
    public IntVariable battleMapWidth;
    public CellGameObjectArray2d battleCellArray;
    public IntArray2d battleMapIntArray;
    public IntArray2d effectsMapIntArray;
    public IntArray2d creaturesMapIntArray;
    public GameObjectVariable PlayerBattleCell;
    public PlayerBattle player;
    public GameEvent movePlayer;
    public GridLayoutGroup gridLayoutGroup;
    public enum battleCellType { Empty, DefaultFloor, ObstacleWall }

    // Use this for initialization
    void Start () {

        battleMapIntArray.array = new int[battleMapHeight.value, battleMapWidth.value];
        creaturesMapIntArray.array = new int[battleMapHeight.value, battleMapWidth.value];
        effectsMapIntArray.array = new int[battleMapHeight.value, battleMapWidth.value];
        battleCellArray.cellGameObjectArray = new GameObject[battleMapHeight.value, battleMapWidth.value];

        CellsSpawn();
        PlayerSpawn();
        ObstacleSpawn();
        EnemySpawn();
        EffectsSpawn();
    }
	
    public void CellsSpawn()
    {
        for (int i = 0; i < battleMapHeight.value; i++)
        {
            for (int y = 0; y < battleMapWidth.value; y++)
            {
                GameObject mapCell = Instantiate(cell) as GameObject;
                mapCell.transform.SetParent(this.gameObject.transform, false);

                gridLayoutGroup.CalculateLayoutInputHorizontal();
                gridLayoutGroup.CalculateLayoutInputVertical();
                gridLayoutGroup.SetLayoutHorizontal();
                gridLayoutGroup.SetLayoutVertical();
                battleCellArray.cellGameObjectArray[i, y] = mapCell;
                mapCell.GetComponent<BattleCell>().DisplayBattleCellInfo(battleCellType.DefaultFloor.ToString(), i, y);
                battleMapIntArray.array[i, y] = 0;
                creaturesMapIntArray.array[i, y] = 0;
                effectsMapIntArray.array[i, y] = 0;
            }
        }
    }

    public void PlayerSpawn()
    {
        int playerHeight = 3;
        int playerWidth = 3;

        creaturesMapIntArray.array[battleMapHeight.value - playerHeight, battleMapWidth.value - playerWidth] = 1;
        battleCellArray.cellGameObjectArray[battleMapHeight.value - playerHeight, battleMapWidth.value - playerWidth].GetComponent<BattleCell>().playerOnCell = true;
        PlayerBattleCell.value = battleCellArray.cellGameObjectArray[battleMapHeight.value - playerHeight, battleMapWidth.value - playerWidth];

        GameObject playerGameObj = Instantiate(player.gameObject) as GameObject;
        playerGameObj.transform.SetParent(creaturesContainer.transform, false);

        PlayerBattleCell.value = this.battleCellArray.cellGameObjectArray[battleMapHeight.value - playerHeight, battleMapWidth.value - playerWidth];
        PlayerBattleCell.value.GetComponent<BattleCell>().playerOnCell = true;
        playerGameObj.transform.position = PlayerBattleCell.value.transform.position;
        playerGameObj.GetComponent<PlayerBattle>().playerHeightIndex = battleMapHeight.value - playerHeight;
        playerGameObj.GetComponent<PlayerBattle>().playerWidthIndex = battleMapWidth.value - playerWidth;
    }

    public void ObstacleSpawn()
    {
        int g = 0;

        for (int i = 0; g < 6; i++)
        {
            Debug.Log("obs");
            int randI = Random.Range(0, battleMapHeight.value);
            int randY = Random.Range(0, battleMapWidth.value);

            if (randI != PlayerBattleCell.value.GetComponent<BattleCell>().heightIndex || randY != PlayerBattleCell.value.GetComponent<BattleCell>().widthIndex)
            {
                g++;
                Debug.Log("1");
                battleCellArray.cellGameObjectArray[randI, randY].GetComponent<BattleCell>().DisplayBattleCellInfo(battleCellType.ObstacleWall.ToString(), randI, randY);
            }
        }
    }
}
