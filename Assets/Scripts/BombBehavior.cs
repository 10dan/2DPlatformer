using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class BombBehavior : MonoBehaviour {

    Rigidbody2D rb;

    [SerializeField] int numFrags = 200;
    [SerializeField] float bombVel = 2f;
    [SerializeField] GameObject frag;

    AudioSource audio;
    [SerializeField] AudioClip explodeSound;


    void Start() {
        rb = GetComponent<Rigidbody2D>();
        audio = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        rb.Sleep();
    }

    public void Explode() {

        //Disable glow
        GetComponentInChildren<UnityEngine.Experimental.Rendering.Universal.Light2D>().intensity = 0.01f ;

        //Throw frags in radial
        float angleSpacer = 2 * Mathf.PI/numFrags;
        for(int i = 0; i < numFrags; i++) {
            float angle = i * angleSpacer;
            float xVel = Mathf.Cos(angle) * bombVel;
            float yVel = Mathf.Sin(angle) * bombVel;
            GameObject fragInstance = Instantiate(frag, transform.position, Quaternion.identity);
            fragInstance.GetComponent<Rigidbody2D>().velocity = new Vector2(xVel, yVel);
        }

        //Play explosion sound
        audio.PlayOneShot(explodeSound);

        //Shake camera
        GameObject go = GameObject.Find("Main Camera");
        CameraShake shaker = (CameraShake)go.GetComponent(typeof(CameraShake));
        shaker.Shake(0.3f,0.4f);
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Rigidbody2D>().Sleep();
        StartCoroutine(KillThis(3f));
    }

    IEnumerator KillThis(float timeTillDeath) {
        yield return new WaitForSeconds(timeTillDeath);
        Destroy(gameObject);
    }
}
