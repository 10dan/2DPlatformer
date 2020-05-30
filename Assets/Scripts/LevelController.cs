using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour {

    public static void LoadLevel(int id) {
        int numLevels = SceneManager.sceneCountInBuildSettings;
        if (id >= numLevels) {
            SceneManager.LoadScene(0);
        } else {
            SceneManager.LoadScene(id);
        }
        GlobalVars.isPlaying = true;
    }
}
