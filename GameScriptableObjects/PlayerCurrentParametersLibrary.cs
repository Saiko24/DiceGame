using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player Current Parameters Library", menuName = "Parameters/Player Current Parameters Library")]
public class PlayerCurrentParametersLibrary : ScriptableObject {

    public List<PlayerCurrentParameter> PlayerCurrentParametersList;
}
