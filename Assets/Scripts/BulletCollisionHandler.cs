using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollisionHandler : MonoBehaviour {
    [SerializeField] GameObject player;
    Collider playerCollider;
    private void Start() {
        playerCollider = player.GetComponent<Collider>();
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "Enemy") {
            print("Hit enemy");
        }
    }
}
