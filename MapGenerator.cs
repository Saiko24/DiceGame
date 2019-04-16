using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SO;

public class MapGenerator : MonoBehaviour {

    public IntVariable height;
    public IntVariable width;
    public FloatVariable MapGeneratorDelay;
    public FloatVariable VisualizeDelay;
    public BoolVariable GenerateDone;
    public CellGameObjectArray2d cellArray;
    public IntArray2d mapIntArray;
    public BoolVariable VisualizeMapGenerate;
    public FloatVariable fillPercent;
    public Vector2Variable playerPosition;
    public Seed randSeed;
    string valueString;
    string arrayString;
    public GameObject cell;
    public enum cellType { Empty, Start, End, Disabled, Standart, Event, Camp, Gambling, Library, Quest, Secret, Shop, Smithy, Tavern, Trap, Treasure }
    public int start;
    public int end;
    private bool firstInstantiateDone = false;

    void Start () {

        mapIntArray.array = new int[height.value, width.value];
        cellArray.cellGameObjectArray = new GameObject[height.value, width.value];

        for (int i = 0; i < height.value; i++)
        {
            for (int y = 0; y < width.value; y++)
            {
                mapIntArray.array[i, y] = 0;
            }
        }

        CellsInstantiate();
        PathFind();
    }
	
 

    public void ArrayDebugLog()
    {
        for (int i = 0; i < height.value; i++)
        {
            for (int y = 0; y < width.value; y++)
            {
                valueString = valueString + " " + mapIntArray.array[i, y].ToString();

                arrayString = arrayString + string.Format("({0},{1})", i, y);
            }
            valueString = valueString + "\n";
            arrayString = arrayString + "\n";

            if (i == height.value - 1)
            {
                Debug.Log(valueString);
                Debug.Log(arrayString);
            }
        }
    }

   public void PathFind()
    {
        GenerateDone.value = false;

        Random.InitState(randSeed.value);

        start = Random.Range(0, width.value);
        end = Random.Range(0, width.value);

        mapIntArray.array[height.value - 1, start] = 1;
        mapIntArray.array[0, end] = 2;

        playerPosition.value.x = height.value - 1;
        playerPosition.value.y = start;

        int pointHeight = height.value - 2;
        int pointWidth = start;

        List<string> directionList = new List<string>();

        mapIntArray.array[pointHeight, pointWidth] = 4;

        StartCoroutine(PathFindDebug(pointHeight, pointWidth, directionList));
    }

    IEnumerator PathFindDebug(int pointHeight, int pointWidth, List<string> directionList )
    {
        if (VisualizeMapGenerate.value)
        {
            cellArray.cellGameObjectArray[height.value - 1, start].GetComponent<CellDisplay>().cellSpriteImage.color = new Color32(0, 255, 0, 255);
            cellArray.cellGameObjectArray[0, end].GetComponent<CellDisplay>().cellSpriteImage.color = new Color32(255, 0, 0, 255);
        }

        float directionIndex;
        string directionString ="";
        int percentOfCells = Mathf.RoundToInt((height.value * width.value) * fillPercent.value);

        while (mapIntArray.array[1, end] == 0)
        {
            directionList.AddRange(new string[] { "up", "down", "right", "left" });

            if ( pointHeight == 1)
            {
                directionList.Remove("up");
            }
            if (pointHeight == height.value - 2 )
            {
                directionList.Remove("down");
            }
            if (pointWidth == 0 )
            {
                directionList.Remove("left");
            }
            if (pointWidth == width.value - 1 )
            {
                directionList.Remove("right");
            }

            directionIndex = Random.Range(0f, 1f);

            for (int i = 0; i < directionList.Count; i++)
            {
                if (directionList[i] == "up" )
                {
                    if (directionIndex <= 0.5)
                    {
                        directionString = "up";
                        break;
                    }
                }
                else
                {
                    directionString = directionList[Random.Range(0, directionList.Count)];
                }
            }

            switch (directionString)
            {
                case "up":  pointHeight -= 1;
                    break;
                case "down":
                    pointHeight += 1;
                    break;
                case "left":
                    pointWidth -= 1;
                    break;
                case "right":
                    pointWidth += 1;
                    break;
            }

            mapIntArray.array[pointHeight, pointWidth] = 4;
            directionList.RemoveRange(0, directionList.Count);

            if (VisualizeMapGenerate.value)
            {
                cellArray.cellGameObjectArray[pointHeight, pointWidth].GetComponent<CellDisplay>().cellSpriteImage.color = new Color32(0, 0, 255, 255);

                yield return new WaitForSeconds(VisualizeDelay.value);

                for (int i = 0; i < height.value; i++)
                {
                    for (int y = 0; y < width.value; y++)
                    {
                        if (mapIntArray.array[i, y] == 4)
                        {
                            cellArray.cellGameObjectArray[i, y].GetComponent<CellDisplay>().cellSpriteImage.color = new Color32(255, 255, 255, 255);
                        }
                    }
                }
            }
        }

        int cellCount=0;
        List<Vector2> currentCells = new List<Vector2>();
        List<Vector2> emptyCells = new List<Vector2>();
        List<Vector2> mainCells = new List<Vector2>();
        Vector2 cellIndex = new Vector2();

        for (int i = 1; i < height.value-1; i++)
        {
            for (int y = 0; y < width.value; y++)
            {
                if (mapIntArray.array[i, y] == 4)
                {
                    cellIndex = new Vector2(i, y);
                    currentCells.Add(cellIndex);
                    cellCount++;
                }
                if (mapIntArray.array[i, y] == 0)
                {
                    cellIndex = new Vector2(i, y);
                    emptyCells.Add(cellIndex);
                }
            }
        }

        foreach (var item in currentCells)
        {
            mainCells.Add(item);
        }

        if (cellCount < percentOfCells)
        {
            while (cellCount < percentOfCells)
            {
                cellIndex = mainCells[Random.Range(0, mainCells.Count)];

                pointHeight = Mathf.RoundToInt(cellIndex.x);
                pointWidth = Mathf.RoundToInt(cellIndex.y);

                for (int z = 0; z < 3; z++)
                {

                    directionList.AddRange(new string[] { "up", "down", "right", "left" });

                    if (pointHeight == 1 || pointHeight == 2 || pointHeight == 3)
                    {
                        directionList.Remove("up");
                    }

                    if (pointHeight == height.value - 2 || pointHeight == height.value - 3 || pointHeight == height.value - 4)
                    {
                        directionList.Remove("down");
                    }

                    if (pointWidth == 0 || pointWidth == 1 || pointWidth == 2)
                    {
                        directionList.Remove("left");
                    }

                    if (pointWidth == width.value - 1 || pointWidth == width.value - 2 || pointWidth == width.value - 3 )
                    {
                        directionList.Remove("right");
                    }

                    directionString = directionList[Random.Range(0, directionList.Count)];


                    for (int u = 0; u < 3; u++)
                    {
                        switch (directionString)
                        {
                            case "up":
                                pointHeight -= 1;

                                break;
                            case "down":
                                pointHeight += 1;
                                break;
                            case "left":
                                pointWidth -= 1;
                                break;
                            case "right":
                                pointWidth += 1;
                                break;
                        }

                        mapIntArray.array[pointHeight, pointWidth] = 4;

                        if (VisualizeMapGenerate.value)
                        {
                            cellArray.cellGameObjectArray[pointHeight, pointWidth].GetComponent<CellDisplay>().cellSpriteImage.color = new Color32(0, 0, 255, 255);
                        }
                    }

                        directionList.RemoveRange(0, directionList.Count);

                    if (VisualizeMapGenerate.value)
                    {
                        yield return new WaitForSeconds(VisualizeDelay.value);

                        for (int i = 0; i < height.value; i++)
                        {
                            for (int y = 0; y < width.value; y++)
                            {
                                if (mapIntArray.array[i, y] == 4)
                                {
                                    cellArray.cellGameObjectArray[i, y].GetComponent<CellDisplay>().cellSpriteImage.color = new Color32(255, 255, 255, 255);
                                }
                            }
                        }

                    }

                }

                cellCount = 0;
                currentCells.Clear();
                emptyCells.Clear();

                for (int i = 1; i < height.value-1; i++)
                {
                    for (int y = 0; y < width.value; y++)
                    {
                        if (mapIntArray.array[i, y] == 4)
                        {
                            cellCount++;
                        }
                        if (mapIntArray.array[i, y] == 0)
                        {
                            emptyCells.Add(cellIndex);
                        }
                    }
                }
            }

        }

        RandomRoomChoose();
        StartCoroutine(PathBuildCoroutine());
    }

    IEnumerator PathBuildCoroutine()
    {
        string cellTypeTemp = "";

        for (int i = 0; i < height.value; i++)
        {
            for (int y = 0; y < width.value; y++)
            {
                switch (mapIntArray.array[i, y])
                {
                    case 0:
                        cellTypeTemp = cellType.Empty.ToString();
                        break;
                    case 1:
                        cellTypeTemp = cellType.Start.ToString();
                        break;
                    case 2:
                        cellTypeTemp = cellType.End.ToString();
                        break;
                    case 3:
                        cellTypeTemp = cellType.Disabled.ToString();
                        break;
                    case 4:
                        cellTypeTemp = cellType.Standart.ToString();
                        break;
                    case 5:
                        cellTypeTemp = cellType.Event.ToString();
                        break;
                    case 6:
                        cellTypeTemp = cellType.Camp.ToString();
                        break;
                    case 7:
                        cellTypeTemp = cellType.Gambling.ToString();
                        break;
                    case 8:
                        cellTypeTemp = cellType.Library.ToString();
                        break;
                    case 9:
                        cellTypeTemp = cellType.Quest.ToString();
                        break;
                    case 10:
                        cellTypeTemp = cellType.Secret.ToString();
                        break;
                    case 11:
                        cellTypeTemp = cellType.Shop.ToString();
                        break;
                    case 12:
                        cellTypeTemp = cellType.Smithy.ToString();
                        break;
                    case 13:
                        cellTypeTemp = cellType.Tavern.ToString();
                        break;
                    case 14:
                        cellTypeTemp = cellType.Trap.ToString();
                        break;
                    case 15:
                        cellTypeTemp = cellType.Treasure.ToString();
                        break;
                }

                cellArray.cellGameObjectArray[i, y].GetComponent<CellDisplay>().DisplayCellInfo(cellTypeTemp, i, y);

                if (mapIntArray.array[i, y] != 0)
                {
                    yield return new WaitForSeconds(MapGeneratorDelay.value);
                }
            }
        }

        GenerateDone.value = true;
    }


    public void RandomRoomChoose()
    {
        for (int i = 0; i < height.value; i++)
        {
            for (int y = 0; y < width.value; y++)
            {
                if (mapIntArray.array[i, y] == 4)
                {
                    float rand = Random.Range(0f, 1f);
                    if (rand>0.25f)
                    {
                    mapIntArray.array[i, y] = Random.Range(4, 16);
                    }
                }
            }
        }
    }

    public void ClearAndGenerateMap()
    {
        if (firstInstantiateDone == true && GenerateDone.value == true)
        {
            for (int i = 0; i < height.value; i++)
            {
                for (int y = 0; y < width.value; y++)
                {
                    mapIntArray.array[i, y] = 0;
                    cellArray.cellGameObjectArray[i, y].GetComponent<CellDisplay>().DisplayCellInfo(cellType.Empty.ToString(), i, y);
                }
            }
            PathFind();
        }
    }

    public void CellsInstantiate()
    {
        for (int i = 0; i < height.value; i++)
        {
            for (int y = 0; y < width.value; y++)
            {
                if (mapIntArray.array[i, y] == 0)
                {
                    GameObject mapCell = Instantiate(cell) as GameObject;
                    mapCell.transform.SetParent(gameObject.transform, false);

                    mapCell.GetComponent<CellDisplay>().DisplayCellInfo(cellType.Empty.ToString(), i, y);
                    cellArray.cellGameObjectArray[i, y] = mapCell;
                }
            }
        }

        firstInstantiateDone = true;
    }
}
