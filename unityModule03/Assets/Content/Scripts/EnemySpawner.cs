using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float interval = 20;
    private float counter = 0;

    void FixedUpdate() {
        counter += 1;
        if(counter >= interval) {
            counter = 0;
            Instantiate(enemyPrefab, transform.position,transform.rotation);
        }
    }
}