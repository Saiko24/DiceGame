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
    public GameObjectList enemyBattleCell;
    public EnemiesArray enemiesArray;
    public PlayerBattle player;
    public EnemyBattle enemy;
    public int enemyCount = 2;
    public int obstacleCount = 6;
    public GameEvent movePlayer;
    public GridLayoutGroup gridLayoutGroup;
    public enum battleCellType { Empty, DefaultFloor, ObstacleWall, EnemyOnCell }

    private void Awake()
    {
        enemyBattleCell.gameObjectList.Clear();
    }

    // Use this for initialization
    void Start () {

        battleMapIntArray.array = new int[battleMapHeight.value, battleMapWidth.value];
        creaturesMapIntArray.array = new int[battleMapHeight.value, battleMapWidth.value];
        effectsMapIntArray.array = new int[battleMapHeight.value, battleMapWidth.value];
        battleCellArray.cellGameObjectArray = new GameObject[battleMapHeight.value, battleMapWidth.value];

        enemyCount = 2;

        CellsSpawn();
        PlayerSpawn();
        EnemySpawn();
        EffectsSpawn();
        ObstacleSpawn();

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
        PlayerBattleCell.value.GetComponent<BattleCell>().walkable = true;
        playerGameObj.transform.position = PlayerBattleCell.value.transform.position;
        playerGameObj.GetComponent<PlayerBattle>().playerHeightIndex = battleMapHeight.value - playerHeight;
        playerGameObj.GetComponent<PlayerBattle>().playerWidthIndex = battleMapWidth.value - playerWidth;
    }

    public void EnemySpawn()
    {
        enemiesArray.array.Clear();

        for (int i = 0; i < enemyCount; i++)
        {
            int enemyHeight = Random.Range(1, battleMapHeight.value);
            int enemyWidth = Random.Range(1, battleMapWidth.value);

            creaturesMapIntArray.array[battleMapHeight.value - enemyHeight, battleMapWidth.value - enemyWidth] = 2;
            battleCellArray.cellGameObjectArray[battleMapHeight.value - enemyHeight, battleMapWidth.value - enemyWidth].GetComponent<BattleCell>().enemyOnCell = true;

            GameObject enemyGameObj = Instantiate(enemy.gameObject) as GameObject;
            enemyGameObj.transform.SetParent(creaturesContainer.transform, false);

            enemyGameObj.GetComponent<EnemyBattle>().enemyHeightIndex = battleMapHeight.value - enemyHeight;
            enemyGameObj.GetComponent<EnemyBattle>().enemyWidthIndex = battleMapWidth.value - enemyWidth;
            enemiesArray.array.Add(enemyGameObj.GetComponent<EnemyBattle>());

            enemyBattleCell.gameObjectList.Add(this.battleCellArray.cellGameObjectArray[battleMapHeight.value - enemyHeight, battleMapWidth.value - enemyWidth]);
            enemyBattleCell.gameObjectList[i].GetComponent<BattleCell>().enemyOnCell = true;
            enemyBattleCell.gameObjectList[i].GetComponent<BattleCell>().walkable = false;
            enemyGameObj.transform.position = enemyBattleCell.gameObjectList[i].transform.position;
        }
    }

    public void ObstacleSpawn()
    {
        for (int i = 0; i < obstacleCount; i++)
        {
            int randH = Random.Range(0, battleMapHeight.value);
            int randW = Random.Range(0, battleMapWidth.value);

            if (randH != PlayerBattleCell.value.GetComponent<BattleCell>().heightIndex || randW != PlayerBattleCell.value.GetComponent<BattleCell>().widthIndex)
            {
                battleCellArray.cellGameObjectArray[randH, randW].GetComponent<BattleCell>().DisplayBattleCellInfo(battleCellType.ObstacleWall.ToString(), randH, randW);
            }

        }
    }
}
