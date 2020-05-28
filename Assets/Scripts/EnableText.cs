using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnableText : MonoBehaviour {
    Text text;

    void Start() {
        text = GetComponent<Text>();
        text.enabled = false;
    }

    public void SetTextVisible(bool state) {
        text.enabled = state;
    }
}
