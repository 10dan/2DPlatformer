using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour {
    [SerializeField] ParticleSystem onDeath;
    [SerializeField] float recoverySpeed = 0.04f;
    float lastHit = 0f; //Time since last time hit.

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.name == "bullet(Clone)") {
            lastHit = 0f;
        }
    }

    private void Update() {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        float r = Mathf.Lerp(0, 1, lastHit);
        float g = Mathf.Lerp(0, 1, lastHit);
        float b = Mathf.Lerp(0, 1, lastHit);
        Color currentColor = new Color(r, g, b);
        renderer.color = currentColor;
        lastHit = Mathf.Clamp(lastHit + recoverySpeed, 0, 1);
    }
}
