using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollisionHandler : MonoBehaviour {
    [SerializeField] GameObject player;
    [SerializeField] ParticleSystem bulletDestroyParticle;
    [SerializeField] AudioClip[] hitSounds;

    AudioSource audio;
    Collider playerCollider;
    bool isActive = true;
    private void Start() {
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
        }
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "Enemy") {
            int hitSoundIndex = UnityEngine.Random.Range(0, hitSounds.Length);
            audio.PlayOneShot(hitSounds[hitSoundIndex]);
            ContactPoint2D col = collision.GetContact(0);
            Vector2 pos = col.point;
            Instantiate(bulletDestroyParticle, pos, Quaternion.identity);
            isActive = false;

        }
    }
}
