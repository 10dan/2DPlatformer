using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollisionHandler : MonoBehaviour {
    DeleteAfterTime deleteScript;
    Collider playerCollider;
    bool isActive = true;

    private void Start() {
        deleteScript = GetComponent<DeleteAfterTime>();
    }
    private void Update() {
        if (!isActive) {
            //Disable renderer, collider, and rigid body, not trail.
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            CircleCollider2D cc = GetComponent<CircleCollider2D>();
            Rigidbody2D rb2d = GetComponent<Rigidbody2D>();
            sr.enabled = false;
            cc.enabled = false;
            rb2d.velocity = new Vector2(0f, 0f);
            rb2d.gravityScale = 0f;
            deleteScript.playParticle = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag != "Ground") {
            isActive = false;
        }
    }
}
