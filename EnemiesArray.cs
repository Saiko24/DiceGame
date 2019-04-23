using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SO
{
    //[CreateAssetMenu(fileName = "New Enemy Array", menuName = "Enemies / EnemiesArray")]
    [CreateAssetMenu(fileName = "New Enemy Array", menuName = "Enemies/EnemiesArray")]
    public class EnemiesArray : ScriptableObject
    {
        public List<EnemyBattle> array;
    }
}