using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteAfterTime : MonoBehaviour {
    [SerializeField] int deathTimer = 3;
    private void Awake() {
        Invoke("DestroyThis", deathTimer);
    }

    private void DestroyThis() {
        Destroy(gameObject);
    }
}
