using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Battle Cell Library", menuName = "BattleMap/ Battle Cell Library")]

public class BattleCellLibrary : ScriptableObject
{
    public List<BattleCellSO> cellList;

}
