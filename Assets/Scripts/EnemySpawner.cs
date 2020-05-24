using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    [SerializeField] GameObject[] enemyTypes;
    [Tooltip("What kind of enemy will this spawner spawn.")]
    [SerializeField] int idOfEnemy;
    [SerializeField] float timeBetweenSpawns = 10f; //In seconds.

    float time = 0;

    private void Start() {
        time = Time.time;
    }

    private void Update() {
        if(Time.time - time > timeBetweenSpawns) {
            spawnEnemy();
            time = Time.time;
        }
    }

    private void spawnEnemy() {
        GameObject enemy = Instantiate(enemyTypes[idOfEnemy], transform.position, transform.rotation);
        enemy.GetComponent<Rigidbody2D>().velocity = new Vector2(UnityEngine.Random.Range(-1, 1), UnityEngine.Random.Range(-1, 1));
    }

}
