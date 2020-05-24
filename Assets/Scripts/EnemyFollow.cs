using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour {
    GameObject player;
    Rigidbody2D rb;
    [SerializeField] float forceMultiplier = 3f;
    [SerializeField] float visionRange = 100f;

    private void Start() {
        player = GameObject.Find("player");
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        Vector2 playerPos = player.transform.position;
        Vector2 pos = transform.position;
        Vector2 dir = playerPos - pos;
        if (dir.magnitude < visionRange) {
            rb.AddForce(dir * forceMultiplier);
        }
    }
}
