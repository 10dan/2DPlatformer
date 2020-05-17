using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {

    private Transform transform;
    float shakeDuration;
    float shakeMagnitude;
    Vector3 initialPosition;
    float timeLeft = 0f;

    void Awake() {
        if (transform == null) {
            transform = GetComponent(typeof(Transform)) as Transform;
        }
    }

    void OnEnable() {
        initialPosition = transform.localPosition;
    }

    void Update() {
        if (timeLeft > 0) {
            transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;

            timeLeft -= Time.deltaTime;
        } else {
            timeLeft = 0f;
            transform.localPosition = initialPosition;
        }
    }

    public void Shake(float duration, float mag) {
        shakeDuration = duration;
        shakeMagnitude = mag;
        timeLeft = shakeDuration;
    }
}
