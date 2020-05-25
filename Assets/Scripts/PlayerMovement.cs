using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {

    Rigidbody2D rb;
    AudioSource audio;

    GameObject bomb;
    Animator anim;
    int jumpCount = 0;
    int maxJumps = 2;
    int lives = GlobalVars.initLives;
    bool hasCharge = false;
    bool isDead = false;

    [SerializeField] AudioClip[] gunSounds;
    [SerializeField] AudioClip[] bloodSounds;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject flashEffect;
    [SerializeField] ParticleSystem hitParticle;
    [SerializeField] ParticleSystem bloodParticle;
    [SerializeField] GameObject bombPrefab;

    [SerializeField] float speed = 10f;

    [SerializeField] float fallMultiplier = 2.5f;
    [SerializeField] float lowJumpMultiplyer = 2f;
    [SerializeField] float jumpVelocity = 10f;

    [SerializeField] float bombVel = 10f;
    [SerializeField] float bulletVel = 100f;
    [SerializeField] float bulletPosOffset = 10f;
    [SerializeField] float flashDistance = 1.5f;




    void Awake() {
        retrieveComponents();
    }

    private void retrieveComponents() {
        rb = GetComponent<Rigidbody2D>();
        audio = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }

    void Update() {
        if (!isDead) {
            processMovement();
            processJump();
            processGuns();
            processFlash();
            processCharge();
        }
    }


    private void processMovement() {
        Vector2 v = rb.velocity;
        float horizontal = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector3(horizontal * speed, v.y);
        rb.velocity = movement;
        anim.SetFloat("Speed", horizontal);
    }

    private void processJump() {
        if (Input.GetButtonDown("Jump") && jumpCount < maxJumps) {
            anim.SetTrigger("Jump");
            GetComponent<Rigidbody2D>().velocity = Vector2.up * jumpVelocity;
            jumpCount++;
        }
        if (rb.velocity.y < 0) {
            rb.velocity += Vector2.up * Physics2D.gravity.y * fallMultiplier * Time.deltaTime;
        } else if (rb.velocity.y > 0 && !Input.GetButton("Jump")) {
            rb.velocity += Vector2.up * Physics2D.gravity.y * lowJumpMultiplyer * Time.deltaTime;
        }
    }

    private void processGuns() {
        if (Input.GetButtonDown("Fire1")) {
            Vector3 mouse3d = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mouse = mouse3d;
            float angle = Mathf.Atan2(mouse.y - transform.position.y, mouse.x - transform.position.x);
            float xVel = Mathf.Cos(angle) * bulletVel;
            float yVel = Mathf.Sin(angle) * bulletVel;
            //offset so bullet doesnt spawn inside the player
            float xOffset = Mathf.Cos(angle) * bulletPosOffset;
            float yOffset = Mathf.Sin(angle) * bulletPosOffset;
            Vector3 bullsetSpawnPos = transform.position + new Vector3(xOffset, yOffset, 0);
            GameObject createdBullet = Instantiate(bullet, bullsetSpawnPos, Quaternion.identity);
            createdBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(xVel, yVel);

            int randomSoundIndex = UnityEngine.Random.Range(0, gunSounds.Length);
            audio.pitch = UnityEngine.Random.Range(0.5f, 1.5f);
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

            //Shake camera
            GameObject go = GameObject.Find("Main Camera");
            CameraShake shaker = (CameraShake)go.GetComponent(typeof(CameraShake));
            shaker.Shake(0.1f, 0.1f);
        }
    }

    private void processCharge() {
        if (Input.GetButtonDown("Fire2")) {
            if (hasCharge == false) {
                hasCharge = true;
                Vector3 mouse3d = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 mouse = mouse3d;
                float angle = Mathf.Atan2(mouse.y - transform.position.y, mouse.x - transform.position.x);
                float xVel = Mathf.Cos(angle) * bombVel;
                float yVel = Mathf.Sin(angle) * bombVel;
                bomb = Instantiate(bombPrefab, transform.position, Quaternion.identity);
                Rigidbody2D rbBomb = bomb.GetComponent<Rigidbody2D>();
                rbBomb.velocity = new Vector2(xVel, yVel);
                rbBomb.angularVelocity = UnityEngine.Random.Range(50f, 300f);
            } else {
                hasCharge = false;
                BombBehavior bombBehavior = bomb.GetComponent<BombBehavior>();
                bombBehavior.Explode();
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        switch (collision.gameObject.tag) {
            case ("Ground"):
                jumpCount = 0;
                break;
            case ("Bullet or Bomb"):
                takeDamage();
                break;
            case ("Enemy"):
                takeDamage();
                break;
        }


    }

    private void takeDamage() {
        //Check if dead.
        lives--;
        if(lives < 1) {
            isDead = true;
        }

        //play ouch sound
        audio.Stop();
        int randomSoundIndex = UnityEngine.Random.Range(0, bloodSounds.Length);
        audio.pitch = UnityEngine.Random.Range(0.5f, 1.5f);
        audio.PlayOneShot(bloodSounds[randomSoundIndex]);
    
        //Make blood effect
        Instantiate(bloodParticle, transform.position, Quaternion.identity);

        //Shake camera
        GameObject go = GameObject.Find("Main Camera");
        CameraShake shaker = (CameraShake)go.GetComponent(typeof(CameraShake));
        shaker.Shake(0.1f, 0.5f);
        GameObject.Find("Text").GetComponent<CanvasOperator>().SetText("Lives:" + lives.ToString());
    }
}
