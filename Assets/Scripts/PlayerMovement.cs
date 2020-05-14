using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    Rigidbody2D rb;
    AudioSource audio;

    [SerializeField] AudioClip[] gunSounds;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject flashEffect;
    [SerializeField] ParticleSystem hitParticle;

    [SerializeField] float particleScale = 1000f;
    [SerializeField] float particleVelThresh = 20f; //If vel less than this no particles.

    [SerializeField] float speed = 10f;
    [SerializeField] float jumpSpeed = 300f;
    [SerializeField] float bulletVel = 100f;
    [SerializeField] float flashDistance = 1.5f;



    private Vector2 v;

    void Start() {
        retrieveComponents();
    }

    private void retrieveComponents() {
        rb = GetComponent<Rigidbody2D>();
        audio = GetComponent<AudioSource>();
    }

    void Update() {
        processMovement();
        processJump();
        processGuns();
        processFlash();
    }

    private void processMovement() {
        Vector2 v = rb.velocity;
        float horizontal = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector3(horizontal * speed, v.y);
        rb.velocity = movement;
    }

    private void processJump() {
        if (Input.GetButtonDown("Jump")) {
            Vector2 v = rb.velocity;
            Vector3 jumpMovement = new Vector3(v.x, 1.0f * jumpSpeed);
            rb.velocity = jumpMovement;
        }
    }

    private void processGuns() {
        if (Input.GetButtonDown("Fire1")) {
            Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouse.z = 0;
            GameObject createdBullet = Instantiate(bullet, transform.position, Quaternion.identity);
            float angle = Mathf.Atan2(mouse.y - transform.position.y, mouse.x - transform.position.x);
            float xVel = Mathf.Cos(angle) * bulletVel;
            float yVel = Mathf.Sin(angle) * bulletVel;
            createdBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(xVel, yVel);

            int randomSoundIndex = UnityEngine.Random.Range(0,gunSounds.Length);
            audio.PlayOneShot(gunSounds[randomSoundIndex]);
        }
    }

    private void processFlash() {
        if (Input.GetKeyDown("f")) {
            Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mouse2d = mouse;
            Vector2 pos = transform.position;
            float angle = Mathf.Atan2(mouse2d.y - pos.y, mouse2d.x - pos.x);
            float x = Mathf.Cos(angle) * flashDistance;
            float y = Mathf.Sin(angle) * flashDistance;

            Instantiate(flashEffect, transform.position, Quaternion.identity);
            transform.position = new Vector2(pos.x + x, pos.y + y);
            Instantiate(flashEffect, transform.position, Quaternion.identity);
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
