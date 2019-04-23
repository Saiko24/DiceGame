using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Utilities;

namespace SO
{
    [CreateAssetMenu(menuName = "SceneChange")]
    public class SceneChange : ScriptableObject
    {
        public BoolVariable raycastBlock;
        public BoolVariable consoleRaycastBlock;

        public void Raise(string sceneName)
        {
            Debug.Log("Scene Change");
            raycastBlock.value = false;
            consoleRaycastBlock.value = false;
            SceneManager.LoadScene(sceneName);
        }
    }
}
