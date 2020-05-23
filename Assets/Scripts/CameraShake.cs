using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {

    private Transform trans;
    float shakeDuration;
    float shakeMagnitude;
    Vector3 initialPosition;
    float timeLeft = 0f;

    void Awake() {
        if (trans == null) {
            trans = GetComponent(typeof(Transform)) as Transform;
        }
    }

    void Update() {
        initialPosition = trans.localPosition;
        if (timeLeft > 0) {
            trans.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;

            timeLeft -= Time.deltaTime;
        } else {
            timeLeft = 0f;
        }
    }

    public void Shake(float duration, float mag) {
        shakeDuration = duration;
        shakeMagnitude = mag;
        timeLeft = shakeDuration;
    }
}
