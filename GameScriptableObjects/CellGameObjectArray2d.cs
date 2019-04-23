using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SO
{
    [CreateAssetMenu(fileName = "New Cell GameObjectArray2d", menuName = "Map/CellGameObjectArray2d")]
    public class CellGameObjectArray2d : ScriptableObject
    {
        public GameObject[,] cellGameObjectArray;
    }
}

