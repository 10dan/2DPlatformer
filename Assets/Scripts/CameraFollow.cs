using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    [SerializeField] GameObject player;
    [SerializeField] float cameraSpeed = 1f;

    private void Update() {
        Vector2 playerPos = player.transform.position;
        Vector3 cameraPos = transform.position;

        float interpolation = cameraSpeed * Time.deltaTime;

        cameraPos.y = Mathf.Lerp(transform.position.y, playerPos.y, interpolation);
        cameraPos.x = Mathf.Lerp(transform.position.x, playerPos.x, interpolation);

        transform.position = cameraPos;
    }
}
