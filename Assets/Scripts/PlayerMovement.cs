using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    Rigidbody2D rb;
    [SerializeField] GameObject bullet;
    [SerializeField] ParticleSystem hitParticle;
    [SerializeField] float particleScale = 1000f;
    [SerializeField] float particleVelThresh = 20f; //If vel less than this no particles.

    [SerializeField] float speed = 10f;
    [SerializeField] float jumpSpeed = 300f;
    [SerializeField] float bulletVel = 100f;



    private Vector2 v;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        v = rb.velocity;
        float horizontal = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector3(horizontal * speed, v.y);
        rb.velocity = movement;

        if (Input.GetButtonDown("Jump")) {
            v = rb.velocity;
            Vector3 jumpMovement = new Vector3(v.x, 1.0f * jumpSpeed);
            rb.velocity = jumpMovement;
        }

        if (Input.GetButtonDown("Fire1")) {
            Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouse.z = 0;
            GameObject createdBullet = Instantiate(bullet, transform.position, Quaternion.identity);
            float angle = Mathf.Atan2(mouse.y - transform.position.y, mouse.x - transform.position.x) ;
            float xVel = Mathf.Cos(angle) * bulletVel;
            float yVel = Mathf.Sin(angle) * bulletVel;
            createdBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(xVel, yVel);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        ContactPoint2D contact = collision.GetContact(0);
        Vector2 pos = contact.point;
        if (v.sqrMagnitude > particleVelThresh) {
            float x = v.sqrMagnitude / particleScale;
            Vector2 newScale = new Vector2(x, x);
            hitParticle.transform.localScale = newScale;
            Instantiate(hitParticle, pos, Quaternion.identity);
        }
    }
}
