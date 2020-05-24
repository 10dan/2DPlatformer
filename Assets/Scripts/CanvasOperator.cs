using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasOperator : MonoBehaviour {
    Text canvasText;

    void Start() {
        canvasText = GetComponent<Text>();
        canvasText.text = "Lives: " + GlobalVars.initLives;
    }

    public void SetText(string textIn) {
        canvasText.text = textIn;
    }
}
