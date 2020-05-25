using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour {
    [SerializeField] ParticleSystem onDeath;
    [SerializeField] AudioClip[] hitSounds;
    [SerializeField] ParticleSystem bulletDestroyParticle;
    [SerializeField] float recoverySpeed = 0.04f;
    [SerializeField] int numLives = 3;
    AudioSource audio;
    float lastHit = 0f; //Time since last time hit.

    private void Awake() {
        audio = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Bullet" || collision.gameObject.tag == "Bomb") {
            lastHit = 0f;
            numLives--;

            ContactPoint2D col = collision.GetContact(0);
            Vector2 pos = col.point;
            if (numLives >= 1) {
                int hitSoundIndex = UnityEngine.Random.Range(0, hitSounds.Length);
                audio.pitch = UnityEngine.Random.Range(0.7f, 1.5f);
                audio.PlayOneShot(hitSounds[hitSoundIndex]);
                Instantiate(bulletDestroyParticle, pos, Quaternion.identity);
            } else {
                Instantiate(onDeath, pos, Quaternion.identity);
                GetComponent<SpriteRenderer>().enabled = false;
                GetComponent<CircleCollider2D>().enabled = false;
                Destroy(gameObject);

            }

        }
    }

    private void Update() {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        float r = Mathf.Lerp(0, 1, lastHit);
        float g = Mathf.Lerp(1, 0, lastHit);
        float b = Mathf.Lerp(1, 0, lastHit);
        Color currentColor = new Color(r, g, b);
        renderer.color = currentColor;
        lastHit = Mathf.Clamp(lastHit + recoverySpeed, 0, 1);
    }
}
