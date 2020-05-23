using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteAfterTime : MonoBehaviour {
    [SerializeField] int deathTimer = 3;
    [SerializeField] ParticleSystem fx;
    public bool playParticle = false;
    private void Awake() {
        Invoke("DestroyThis", deathTimer);
    }

    private void DestroyThis() {
        if (playParticle) {
            Instantiate(fx, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
