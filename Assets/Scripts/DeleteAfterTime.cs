using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteAfterTime : MonoBehaviour {
    [SerializeField] int deathTimer = 3;
    [SerializeField] ParticleSystem onBulletDeathParticle;
    private void Awake() {
        Invoke("DestroyThis", deathTimer);
    }

    private void DestroyThis() {
        Instantiate(onBulletDeathParticle, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
