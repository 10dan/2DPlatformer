using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPlatform : MonoBehaviour {
    [SerializeField] float glowSpeed = 1f;
    [SerializeField] float intensityModifier = 2f;
    [SerializeField] float initialIntensity = 1f;
    [SerializeField] AudioClip endNoise;

    UnityEngine.Experimental.Rendering.Universal.Light2D light;
    void Start() {
        light = GetComponentInChildren<UnityEngine.Experimental.Rendering.Universal.Light2D>();
    }

    void Update() {
        float value = initialIntensity + Mathf.Sin(Time.time * glowSpeed) * intensityModifier;
        light.intensity = value;
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.collider.tag == "Player") {
            GameObject.Find("LevelComplete").GetComponent<EnableText>().SetTextVisible(true);
            GlobalVars.currentLevel += 1;
            SoundManager.Instance.Play(endNoise);
            StartCoroutine(Delay(2f));
        }
    }

    IEnumerator Delay(float delay) {
        yield return new WaitForSeconds(delay);
        LevelController.LoadLevel(GlobalVars.currentLevel);
    }
}
