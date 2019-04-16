using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SO;

[CreateAssetMenu(fileName = "New Cell GameEvent Library", menuName = "Map/Cell GameEvent Library")]
public class CellGameEventLibrary : ScriptableObject {

    public List<GameEvent> CellGameEventsList;
}
