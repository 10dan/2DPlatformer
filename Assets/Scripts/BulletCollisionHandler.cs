using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollisionHandler : MonoBehaviour {
    [SerializeField] GameObject player;
    [SerializeField] ParticleSystem bulletDestroyParticle;
    [SerializeField] AudioClip[] hitSounds;

    DeleteAfterTime deleteScript;
    AudioSource audio;
    Collider playerCollider;
    bool isActive = true;
    private void Start() {
        deleteScript = GetComponent<DeleteAfterTime>();
        audio = GetComponent<AudioSource>();
        playerCollider = player.GetComponent<Collider>();
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
        if(collision.gameObject.tag == "Enemy") {
            //Choose hit sound
            int hitSoundIndex = UnityEngine.Random.Range(0, hitSounds.Length);
            audio.pitch = UnityEngine.Random.Range(0.7f, 1.5f);
            audio.PlayOneShot(hitSounds[hitSoundIndex]);

            ContactPoint2D col = collision.GetContact(0);
            Vector2 pos = col.point;
            Instantiate(bulletDestroyParticle, pos, Quaternion.identity);
            isActive = false;

        }
    }
}
