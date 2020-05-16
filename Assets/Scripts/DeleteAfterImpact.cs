using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteAfterImpact : MonoBehaviour {
    private void OnCollisionEnter2D(Collision2D collision) {
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<Rigidbody2D>().Sleep();
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<ParticleSystem>().Stop();
    }
}
