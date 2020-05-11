using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    Rigidbody2D rb;
    [SerializeField] float speed = 10f;
    [SerializeField] float jumpSpeed = 10f;

    private bool onGround = false;
    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        float horizontal = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(horizontal, rb.velocity.y, 0f);
        rb.velocity = movement * speed;

        if (Input.GetKeyDown("space") && onGround == true) {
            Vector3 jumpMovement = new Vector3(0.0f, 1.0f, 0.0f);
            rb.velocity = jumpMovement * jumpSpeed;
        }
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Ground")) {
            onGround = true;
        }
    }

    void OnCollisionExit(Collision collisionInfo) {
        if (collisionInfo.gameObject.CompareTag("Ground")) {
            onGround = false;
        }
    }

}
