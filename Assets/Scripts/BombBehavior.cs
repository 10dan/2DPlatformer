using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBehavior : MonoBehaviour {

    Rigidbody2D rb;

    [SerializeField] int numFrags = 200;
    [SerializeField] float bombVel = 2f;
    [SerializeField] GameObject frag;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        rb.Sleep();
    }

    public void Explode() {
        float angleSpacer = 2 * Mathf.PI/numFrags;
        for(int i = 0; i < numFrags; i++) {
            float angle = i * angleSpacer;
            float xVel = Mathf.Cos(angle) * bombVel;
            float yVel = Mathf.Sin(angle) * bombVel;
            GameObject fragInstance = Instantiate(frag, transform.position, Quaternion.identity);
            fragInstance.GetComponent<Rigidbody2D>().velocity = new Vector2(xVel, yVel);
        }
        Destroy(gameObject);
    }
}
